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
            sd.Title = "������� ���� �������...";
            sd.FileName = "i61001_10_032320165.zip";
            sd.Filter = "Zip file (*.zip)|*.zip";	// zip file format
            if (sd.ShowDialog() != DialogResult.OK)
                return;

            labelFile.Text = sd.FileName;

            //�������� ����� �� ������
            FileInfo file = new FileInfo(labelFile.Text);
            if (file.Extension.IndexOf("zip") == -1)
            {
                MessageBox.Show("���������� ����� ������ zip-������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (file.Name.IndexOf("i") != 0)
            {
                MessageBox.Show("��� zip-������ ������ ���������� � ����� i.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //��������� �������� ����� � ����������� ������
            FileStream fileStream = new FileStream(sd.FileName, FileMode.Open);
            //������� ������ BinaryReader ��� ������ �������� ������
            BinaryReader binaryReader = new BinaryReader(fileStream);
            //����. ������ �������
            mas = new byte[fileStream.Length];
            //� ����� ������� ��������� ����
            for (int i = 0; i < fileStream.Length; i++)
                mas[i] = binaryReader.ReadByte();
            //������� �����
            binaryReader.Close();
            fileStream.Close();
            //��������
            mas = Compress(mas);

            bodyFileParcel.BodyStartFile = mas;

            var newId = await Operation.InsertFile(bodyFileParcel);
            if (newId != Guid.Empty)
            {
                MessageBox.Show($"���� {newId} ��������.", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            //����� ������ ��� ��������� ������ ��� 401 ���������� �� ��������� 1 ����
            var data = await Operation.GetFileParcels("401", 1);
            var list = data.Where(x => x.DateRet == null).ToList();
            if (list.Count == 0)
            {
                MessageBox.Show("������ �� �������.", "��������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            listBoxIDs.Items.Clear();
            foreach (var row in list)
            {
                listBoxIDs.Items.Add(row.Id.ToString());
            }
            //��������� ������ � ���������� ������ � ����
            List<FileForUpdate> listUpdate = new List<FileForUpdate>();
            foreach (var row in list)
            {
                FileForUpdate fileForUpdate = new FileForUpdate() { Parcel = row, IsUpdate = false };
                var file = await Operation.GetFileParcel(row.Id);
                if (file.BodyRetFile != null)
                {
                    byte[]? mas = Decompress(file.BodyRetFile);

                    //�������� � �������� ��������
                    string md = Path.Combine(Application.StartupPath, "pFiles");
                    if (!(Directory.Exists(md))) Directory.CreateDirectory(md);
                    //������� �������� ��������
                    foreach (var zipFile in new DirectoryInfo(md).GetFiles().Where(x => x.LastWriteTime < DateTime.Now.AddDays(-100)))
                    {
                        zipFile.Delete();
                    }
                    //��������� ����
                    string f = Path.Combine(md, file.NameFile);
                    if (System.IO.File.Exists(f)) System.IO.File.Delete(f);

                    //������� ���� � ������� �����
                    FileStream fs = new FileStream(f, FileMode.Create);
                    //������� ������ BinaryWriter ��� ������ � ����
                    BinaryWriter bw = new BinaryWriter(fs);
                    //�������� ������
                    bw.Write(mas, 0, mas.Length);
                    //������� ������
                    bw.Close();
                    fs.Close();
                    mas = null;

                    //��������� ������
                    var updateID = await Operation.UpdateFileParcel(row.Id, row);
                    fileForUpdate.IsUpdate = true;
                    listUpdate.Add(fileForUpdate);
                }
            }
            var cUpdate = listUpdate.Where(x=>x.IsUpdate).Count();
            MessageBox.Show($"��������� {list.Count} ������ �������. �������� {cUpdate} ������.", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}