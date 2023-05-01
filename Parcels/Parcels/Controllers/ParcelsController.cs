using Parcels.Models;
using Parcels.Services;
using Microsoft.AspNetCore.Mvc;
using Parcels.Utils;

namespace Parcels.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ParcelsController : Controller
    {
        private readonly Repository _rep;
        private IWebHostEnvironment _Environment;
        IConfiguration _config;

        public ParcelsController(IWebHostEnvironment environment, IConfiguration config)
        {
            _Environment = environment;
            _config = config;
            int timeout = Convert.ToInt32(config.GetSection("MaxTimeOut").Value);
            string connectionString = config.GetSection("ConnectionStrings")["DefaultConnection"];
            _rep = new Repository(connectionString, timeout);
        }

        [HttpGet]
        public IActionResult Get(string CTERR = "", int day = 1)
        {
            try
            {
                return new ObjectResult(_rep.GetTop(CTERR, day));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public IActionResult Get(Guid id)
        {
            FileParcel parcel;
            try
            {
                parcel = _rep.Get(id);
                if (parcel == null) return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return new ObjectResult(parcel);
        }

        [HttpGet("{Id}")]
        public IActionResult GetFile(Guid Id)
        {
            FileParcel parcel;
            try
            {
                parcel = _rep.Get(Id);
                if (parcel == null) return NotFound("Запись о запрашиваемо файле не надена в базе данных.");
                //выполняем получение файла из ТФОМС, архивирование и передачу его в общий архив и клиенту
                //******************************************************************************************

                //Проверка и создание каталога
                string md = Path.Combine(_Environment.ContentRootPath, "WorkFiles");
                if (!(Directory.Exists(md))) Directory.CreateDirectory(md);
                //Очистка рабочего каталога
                foreach (var file in new DirectoryInfo(md).GetFiles().Where(x => x.LastWriteTime < DateTime.Now.AddDays(-10)))
                {
                    file.Delete();
                }
                //Копируем разархивированный файл в папку для отправки
                string TfomsDir = _config.GetSection("TfomsDir").Value;
                if (!Directory.Exists(TfomsDir)) return BadRequest("Каталога с ответами из ТФОМС не найдено.");
                string pFile = Path.Combine(TfomsDir, parcel.RetFile + ".xml");
                if (!System.IO.File.Exists(pFile)) return NotFound("Ответа из ТФОМС не найдено.");
                //Создаем вспомогательный каталог для копирования p-файла
                string dir = Path.Combine(md, parcel.RetFile);
                if (!(Directory.Exists(dir))) Directory.CreateDirectory(dir);
                string targetFile = Path.Combine(dir, new FileInfo(pFile).Name);
                string zipFile = Path.Combine(md, parcel.RetFile + ".zip");
                System.IO.File.Copy(pFile, targetFile, true);
                //Архивирем файл в рабочем каталоге
                if (System.IO.File.Exists(zipFile)) System.IO.File.Delete(zipFile);
                Helpers.CreateZipFile(dir, zipFile);
                //Удалеям вспомогательный каталог, где был исходный p-файл
                if (Directory.Exists(dir)) Directory.Delete(dir, true);

                //Копируем исходный файл в общий архив
                string ArhDir = _config.GetSection("ArhDir").Value;
                if (!Directory.Exists(ArhDir)) return BadRequest("Каталога для архивов не найдено.");
                //Создаем подкаталоги если их нет
                string yyyy = new FileInfo(zipFile).LastWriteTime.ToString("yyyy");
                string MM = new FileInfo(zipFile).LastWriteTime.ToString("MM");
                if (!(Directory.Exists(Path.Combine(ArhDir, "pFiles")))) Directory.CreateDirectory(Path.Combine(ArhDir, "pFiles"));
                if (!(Directory.Exists(Path.Combine(ArhDir, "pFiles", yyyy)))) Directory.CreateDirectory(Path.Combine(ArhDir, "pFiles", yyyy));
                if (!(Directory.Exists(Path.Combine(ArhDir, "pFiles", yyyy, MM)))) Directory.CreateDirectory(Path.Combine(ArhDir, "pFiles", yyyy, MM));
                System.IO.File.Copy(zipFile, Path.Combine(ArhDir, "pFiles", yyyy, MM, new FileInfo(zipFile).Name), true);

                //Передаем архив клиенту
                byte[]? mas = null;
                //Связываем файловый поток с открываемым файлом
                FileStream fileStream = new FileStream(zipFile, FileMode.Open);
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
                mas = Helpers.Compress(mas);

                RetFile retFile = new RetFile() { NameFile = new FileInfo(zipFile).Name, BodyRetFile = mas };
                return Ok(retFile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] BodyFileParcel parcel)
        {
            // если есть ошибки - возвращаем ошибку 400
            if (!ModelState.IsValid)
            {
                string validationErrors = string.Join(". ",
                    ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());
                return BadRequest(validationErrors);
            }
            // если ошибок нет, сохраняем элемент
            Guid newId = Guid.Empty;
            try
            {
                if (_rep.GetRow(parcel.StartFile).Id != Guid.Empty)
                {
                    return BadRequest("Выбранный файл уже загружался.");
                }
                //выполняем загрузку файла от клиента, разархивирование и передачу его общий архив и в папку для отправки
                //******************************************************************************************
                if (parcel.BodyStartFile != null)
                {
                    byte[]? mas = Utils.Helpers.Decompress(parcel.BodyStartFile);

                    //Проверка и создание каталога
                    string md = Path.Combine(_Environment.ContentRootPath, "WorkFiles");
                    if (!(Directory.Exists(md))) Directory.CreateDirectory(md);
                    //Очистка рабочего каталога
                    foreach (var file in new DirectoryInfo(md).GetFiles().Where(x => x.LastWriteTime < DateTime.Now.AddDays(-10)))
                    {
                        file.Delete();
                    }
                    //Сохраняем файл
                    string f = Path.Combine(md, parcel.StartFile + ".zip");
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

                    //Разархивируем файл
                    string iFile = Helpers.DearhivZipFile(f, md);

                    //Копируем исходный файл в общий архив
                    string ArhDir = _config.GetSection("ArhDir").Value;
                    if (!Directory.Exists(ArhDir)) return BadRequest("Каталога для архивов не найдено.");
                    //Создаем подкаталоги если их нет
                    string yyyy = new FileInfo(f).LastWriteTime.ToString("yyyy");
                    string MM = new FileInfo(f).LastWriteTime.ToString("MM");
                    if (!(Directory.Exists(Path.Combine(ArhDir, "iFiles")))) Directory.CreateDirectory(Path.Combine(ArhDir, "iFiles"));
                    if (!(Directory.Exists(Path.Combine(ArhDir, "iFiles", yyyy)))) Directory.CreateDirectory(Path.Combine(ArhDir, "iFiles", yyyy));
                    if (!(Directory.Exists(Path.Combine(ArhDir, "iFiles", yyyy, MM)))) Directory.CreateDirectory(Path.Combine(ArhDir, "iFiles", yyyy, MM));
                    System.IO.File.Copy(f, Path.Combine(ArhDir, "iFiles", yyyy, MM, new FileInfo(f).Name), true);

                    //Копируем разархивированный файл в папку для отправки
                    string SendDir = _config.GetSection("SendDir").Value;
                    if (!Directory.Exists(SendDir)) return BadRequest("Каталога для отправки не найдено.");
                    System.IO.File.Copy(iFile, Path.Combine(SendDir, new FileInfo(iFile).Name), true);
                    //Удаляем все файлы
                    System.IO.File.Delete(f);
                    System.IO.File.Delete(iFile);
                }
                else
                {
                    return BadRequest("Файл не прикреплен для отправки.");
                }

                //добавление в базу
                var newParcel = new FileParcel() {
                    Id = newId,
                    CTERR = parcel.CTERR,
                    StartFile = parcel.StartFile,
                    DateStart = DateTime.Now,
                    RetFile = parcel.RetFile,
                    DateRet = null 
                };
                newId = _rep.AddFileParcel(newParcel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(newId);
        }

        [HttpPut("{Id}")]
        public IActionResult Put(Guid Id, [FromBody] FileParcel model)
        {
            // если есть ошибки - возвращаем ошибку 400
            if (!ModelState.IsValid)
            {
                string validationErrors = string.Join(". ",
                    ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());
                return BadRequest(validationErrors);
            }
            // если ошибок нет, обновляем элемент и удаляем отосланный файл
            try
            {
                var parcel = _rep.Get(Id);

                //Удаляем файл архива p-файла
                //Проверка и создание каталога
                string md = Path.Combine(_Environment.ContentRootPath, "WorkFiles");
                if (!(Directory.Exists(md))) Directory.CreateDirectory(md);
                //Очистка рабочего каталога
                foreach (var file in new DirectoryInfo(md).GetFiles().Where(x => x.LastWriteTime < DateTime.Now.AddDays(-10)))
                {
                    file.Delete();
                }
                string fZip = Path.Combine(md, parcel.RetFile + ".zip");
                if (System.IO.File.Exists(fZip)) System.IO.File.Delete(fZip);

                //Обновляем запись
                parcel.DateRet = model.DateRet ?? DateTime.Now;
                _rep.UpdateFileParcel(parcel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(Id);
        }

    }
}
