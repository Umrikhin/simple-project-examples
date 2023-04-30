using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace Parcels.Utils
{
    public class Helpers
    {
        //Возвращаем хеш строки
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                string CodeMD5 = string.Empty;
                for (int i = 0; i < bytes.Length; i++)
                {
                    CodeMD5 += bytes[i].ToString("X2");
                }
                return CodeMD5;
            }
        }

        //Архивирование файлов
        public static void CreateZipFile(string startPath, string zipPath)
        {
            //startPath - путь каталога для архивации
            //zipPath - путь архива

            //не работает если архив и файлы для архивации в одном каталоге

            //Поэтому создаем еще одну папку в папке
            var subDir = Path.Combine(startPath, "docs");
            if (!Directory.Exists(subDir)) Directory.CreateDirectory(subDir);
            //Перемещаем в нее содержимое исходной папки
            foreach(var file in new DirectoryInfo(startPath).GetFiles())
            {
                file.MoveTo(Path.Combine(subDir, file.Name), true);
            }
            //Создание архива
            ZipFile.CreateFromDirectory(subDir, zipPath);
        }

        //Разархивирование файлов в текущую директорию
        public static string DearhivZipFile(string zipPath, string workDirDefault)
        {
            //workDirDefault - каталог для распаковки по умолчанию
            string destinationFile = string.Empty;
            FileInfo fZip = new FileInfo(zipPath);
            string dir = fZip.DirectoryName ?? workDirDefault;
            destinationFile = Path.Combine(dir, Path.GetFileNameWithoutExtension(fZip.Name) + ".xml");
            ZipFile.ExtractToDirectory(zipPath, dir, true);
            return destinationFile;
        }

        //Сжатие для отправки
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

        //Декомпрессия для приема
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

    }
}
