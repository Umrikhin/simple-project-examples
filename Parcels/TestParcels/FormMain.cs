using Microsoft.VisualBasic.Devices;
using System;
using System.IO.Compression;
using System.Windows.Forms;
using TestParcels.Models;

namespace TestParcels
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private async void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog sd = new OpenFileDialog();
            sd.Title = "Открыть файл посылки...";
            sd.FileName = "i61001_10_032320165.zip";
            sd.Filter = "Zip file (*.zip)|*.zip";	// zip file format
            if (sd.ShowDialog() != DialogResult.OK)
                return;

            labelFile.Text = sd.FileName;

            //Отправка файла на сервер
            FileInfo file = new FileInfo(labelFile.Text);
            if (file.Extension.IndexOf("zip") == -1)
            {
                MessageBox.Show("Отправлять можно только zip-архивы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (file.Name.IndexOf("i") != 0)
            {
                MessageBox.Show("Имя zip-архива должно начинаться с буквы i.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBoxUrl.Text.Trim().Length > 0)
            {
                Operation.UrlWebApi = textBoxUrl.Text.Trim();
            }

            string iFile = Path.GetFileNameWithoutExtension(file.Name);
            string pFile = "p" + iFile.Substring(1);

            var bodyFileParcel = new BodyFileParcel() { Id = Guid.Empty, CTERR = "401", StartFile = iFile, DateStart = DateTime.Now, RetFile = pFile };

            byte[]? mas = null;
            //Связываем файловый поток с открываемым файлом
            FileStream fileStream = new FileStream(sd.FileName, FileMode.Open);
            //Создаем объект BinaryReader для чтения двоичных данных
            BinaryReader binaryReader = new BinaryReader(fileStream);
            //указ. длинну массива
            mas = new byte[fileStream.Length];
            //в цикле считали побойтово файл
            for (int i = 0; i < fileStream.Length; i++)
                mas[i] = binaryReader.ReadByte();
            //закрыли поток
            binaryReader.Close();
            fileStream.Close();
            //Упаковка
            mas = Compress(mas);

            bodyFileParcel.BodyStartFile = mas;

            var newId = await Operation.InsertFile(bodyFileParcel);
            if (newId != Guid.Empty)
            {
                MessageBox.Show($"Файл {newId} загружен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static byte[] Compress(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzStream = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    gzStream.Write(data, 0, data.Length);
                }
                return ms.ToArray();
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            using (GZipStream gzStream = new GZipStream(new MemoryStream(data), CompressionMode.Decompress, true))
            {
                const int bufferSize = 4096;
                int bytesRead = 0;

                byte[] buffer = new byte[bufferSize];

                using (MemoryStream ms = new MemoryStream())
                {
                    while ((bytesRead = gzStream.Read(buffer, 0, bufferSize)) > 0)
                    {
                        ms.Write(buffer, 0, bytesRead);
                    }
                    return ms.ToArray();
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            textBoxUrl.Text = Operation.UrlWebApi;
            Operation._client.Timeout = new TimeSpan(0, 5, 0);
        }

        private async void buttonGetData_Click(object sender, EventArgs e)
        {
            if (textBoxUrl.Text.Trim().Length > 0)
            {
                Operation.UrlWebApi = textBoxUrl.Text.Trim();
            }

            //Выбор списка для получения файлов для 401 территроии за прошедший 1 день
            var data = await Operation.GetFileParcels("401", 1);
            var list = data.Where(x => x.DateRet == null).ToList();
            if (list.Count == 0)
            {
                MessageBox.Show("Данных не найдено.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            listBoxIDs.Items.Clear();
            foreach (var row in list)
            {
                listBoxIDs.Items.Add(row.Id.ToString());
            }
            //Получение файлов и обновление данных в базе
            List<FileForUpdate> listUpdate = new List<FileForUpdate>();
            foreach (var row in list)
            {
                FileForUpdate fileForUpdate = new FileForUpdate() { Parcel = row, IsUpdate = false };
                var file = await Operation.GetFileParcel(row.Id);
                if (file.BodyRetFile != null)
                {
                    byte[]? mas = Decompress(file.BodyRetFile);

                    //Проверка и создание каталога
                    string md = Path.Combine(Application.StartupPath, "pFiles");
                    if (!(Directory.Exists(md))) Directory.CreateDirectory(md);
                    //Очистка рабочего каталога
                    foreach (var zipFile in new DirectoryInfo(md).GetFiles().Where(x => x.LastWriteTime < DateTime.Now.AddDays(-100)))
                    {
                        zipFile.Delete();
                    }
                    //Сохраняем файл
                    string f = Path.Combine(md, file.NameFile);
                    if (System.IO.File.Exists(f)) System.IO.File.Delete(f);

                    //создали файл в целевой папке
                    FileStream fs = new FileStream(f, FileMode.Create);
                    //Создали объект BinaryWriter для записи в файл
                    BinaryWriter bw = new BinaryWriter(fs);
                    //записали данные
                    bw.Write(mas, 0, mas.Length);
                    //закрыли потоки
                    bw.Close();
                    fs.Close();
                    mas = null;

                    //Обновляем запись
                    var updateID = await Operation.UpdateFileParcel(row.Id, row);
                    fileForUpdate.IsUpdate = true;
                    listUpdate.Add(fileForUpdate);
                }
            }
            var cUpdate = listUpdate.Where(x=>x.IsUpdate).Count();
            MessageBox.Show($"Проверено {list.Count} файлов ответов. Получено {cUpdate} файлов.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}