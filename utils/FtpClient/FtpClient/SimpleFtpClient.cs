using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FtpClient
{
    class SimpleFtpClient
    {
        protected string FtpUser { get; set; }
        protected string FtpPass { get; set; }
        protected string FtpServerUrl { get; set; }
        protected string DirPathToUpload { get; set; }
        protected string BaseDirectory { get; set; }

        public SimpleFtpClient(string ftpuser, string ftppass, string ftpserverurl, string dirpathtoupload)
        {
            this.FtpPass = ftppass;
            this.FtpUser = ftpuser;
            this.FtpServerUrl = ftpserverurl;
            this.DirPathToUpload = dirpathtoupload;
            var spllitedpath = dirpathtoupload.Split('\\').ToArray();
            // Последний индекс должен быть "базовым" каталогом на сервере
            this.BaseDirectory = spllitedpath[spllitedpath.Length - 1];
        }


        public void UploadDirectory()
        {
            // Переименовать старую версию папки (если есть)
            //RenameDir(BaseDirectory);
            // Создать родительскую папку на сервере
            CreateDir(BaseDirectory);
            // Загрузить файлы в самый внешний каталог пути
            UploadAllFolderFiles(DirPathToUpload, BaseDirectory);
            // Перебрать все файлы в подкаталогах



            foreach (string dirPath in Directory.GetDirectories(DirPathToUpload, "*",
            SearchOption.AllDirectories))
            {
                // Создать папку
                CreateDir(dirPath.Substring(dirPath.IndexOf(BaseDirectory), dirPath.Length - dirPath.IndexOf(BaseDirectory)));

                Console.WriteLine(dirPath.Substring(dirPath.IndexOf(BaseDirectory), dirPath.Length - dirPath.IndexOf(BaseDirectory)));
                UploadAllFolderFiles(dirPath, dirPath.Substring(dirPath.IndexOf(BaseDirectory), dirPath.Length - dirPath.IndexOf(BaseDirectory)));
            }
        }

        private void UploadAllFolderFiles(string localpath, string remotepath)
        {
            string[] files = Directory.GetFiles(localpath);
            // Получить только имена файлов и объединить их с удаленным путем
            foreach (string file in files)
            {
                // Полный удаленный путь
                var fullremotepath = remotepath + "\\" + Path.GetFileName(file);
                // Локальный путь
                var fulllocalpath = Path.GetFullPath(file);
                // Загрузить на сервер
                Upload(fulllocalpath, fullremotepath);
            }

        }

        public bool CreateDir(string dirname)
        {
            try
            {
                WebRequest request = WebRequest.Create("ftp://" + FtpServerUrl + "/" + dirname);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Proxy = new WebProxy();
                request.Credentials = new NetworkCredential(FtpUser, FtpPass);
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    if (resp.StatusCode == FtpStatusCode.PathnameCreated)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public void Upload(string filepath, string targetpath)
        {

            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(FtpUser, FtpPass);
                client.Proxy = null;
                var fixedpath = targetpath.Replace(@"\", "/");
                client.UploadFile("ftp://" + FtpServerUrl + "/" + fixedpath, WebRequestMethods.Ftp.UploadFile, filepath);
            }
        }

        public bool RenameDir(string dirname)
        {
            var path = "ftp://" + FtpServerUrl + "/" + dirname;
            string serverUri = path;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                request.Method = WebRequestMethods.Ftp.Rename;
                request.Proxy = null;
                request.Credentials = new NetworkCredential(FtpUser, FtpPass);
                // Изменить название старой папки на старую папку
                request.RenameTo = DateTime.Now.ToString("yyyyMMddHHmmss");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    if (resp.StatusCode == FtpStatusCode.FileActionOK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
