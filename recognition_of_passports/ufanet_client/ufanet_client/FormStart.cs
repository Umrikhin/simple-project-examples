using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace ufanet_client
{
    public partial class FormStart : Form
    {
        string Host = "passrec.ufanet.ru/api/v0";

        //Пути для служб
        string PathService = "/passports/recognition/";
        string PathServiceToken = "/token/";

        //Параметры доступа
        string login = "demo_panaceya";
        string password = "We0t2GgcZIMR";

        public FormStart()
        {
            InitializeComponent();
            ReadSettings();
        }

        private void ReadSettings()
        {
            if (!(System.IO.File.Exists(Application.StartupPath + "\\ufanet_client.ini")))
            {
                return;
            }
            IniFile clsIni = new IniFile(Application.StartupPath + "\\ufanet_client.ini");
            login = clsIni.IniReadValue("Settings", "login");
            password = clsIni.IniReadValue("Settings", "password");
        }
        private void buttonBrowsePhoto_Click(object sender, EventArgs e)
        {
            if (openFileDialogPhoto.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(openFileDialogPhoto.FileName);

                pictureBoxPhoto.Image = Image.FromFile(openFileDialogPhoto.FileName);
                labelRez.Text = "Результат:";
            }
        }

        private void buttonClearPhoto_Click(object sender, EventArgs e)
        {
            pictureBoxPhoto.Image = null;
            labelRez.Text = "Результат:";
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (pictureBoxPhoto.Image == null)
            {
                labelRez.Text = "Результат:\r\nПрикрепите файл изображения.";
                return;
            }
            HttpStatusCode code = HttpStatusCode.NotFound;
            var output = SendData(ImageToBase64(pictureBoxPhoto.Image, System.Drawing.Imaging.ImageFormat.Jpeg), out code);
            if (code == HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<Models.PassportRecognition>(output).detail;
                labelRez.Text = "Результат:\r\n" + "Серия и номер: " + result.series_number;
                labelRez.Text += "\r\n" + "Кем выдан: " + result.authority;
                labelRez.Text += "\r\n" + "Дата выдачи: " + result.issue_date;
                labelRez.Text += "\r\n" + "Код подразделения: " + result.authority_code;
                labelRez.Text += "\r\n" + "Фамилия: " + result.surname;
                labelRez.Text += "\r\n" + "Имя: " + result.name;
                labelRez.Text += "\r\n" + "Отчество: " + result.patronymic;
                labelRez.Text += "\r\n" + "Пол: " + result.gender;
                labelRez.Text += "\r\n" + "Дата рождения: " + result.birthday;
                labelRez.Text += "\r\n" + "Место рождения: " + result.birthplace;
            }
            else if (code == HttpStatusCode.BadRequest)
            {
                var result = JsonConvert.DeserializeObject<Models.BodyResultRec400>(output);
                labelRez.Text = "Результат:\r\n" + result.detail;
                labelRez.Text += "\r\n" + result.error_detail;
                labelRez.Text += "\r\n" + result.status;
                labelRez.Text += "\r\n" + result.status_id;
                labelRez.Text += "\r\n" + result.error_message;
            }
            else if (code == HttpStatusCode.Forbidden)
            {
                var result = JsonConvert.DeserializeObject<Models.BodyResultRec403>(output);
                labelRez.Text = "Результат:\r\n" + result.detail;
                labelRez.Text += "\r\n" + result.error_detail;
                labelRez.Text += "\r\n" + result.status;
                labelRez.Text += "\r\n" + result.status_id;
                labelRez.Text += "\r\n" + result.error_message;
            }
            else
            {
                labelRez.Text = "Результат:\r\nРезультат не получен. " + output;
            }
        }

        public string SendData(string image_base64, out HttpStatusCode statusCode)
        {
            statusCode = HttpStatusCode.NotFound;

            String ResHTML = "";

            HttpWebRequest Req;
            HttpWebResponse resp;
            Stream receiveStream;
            StreamReader readStream;

            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
delegate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
{
    return true; // **** Always accept
};

                Req = (HttpWebRequest)WebRequest.Create("https://" + Host + PathService);
                Req.Method = "POST";
                Req.AllowAutoRedirect = true;
                Req.ContentType = " application/json";

                bool isGetToken = false;
                var token = GetAccessToken(out isGetToken);
                if (!isGetToken) throw new Exception("Токен доступа не получен: " + token);
                Req.Headers.Add("Authorization", "JWT " + token);

                Models.BodyRequest body = new Models.BodyRequest() { file = image_base64, doc_type = "passport" };
                byte[] sentData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
                Req.ContentLength = sentData.Length;
                Req.GetRequestStream().Write(sentData, 0, sentData.Length);
                Req.GetRequestStream().Close();
                resp = (HttpWebResponse)Req.GetResponse();
                statusCode = resp.StatusCode;
                receiveStream = resp.GetResponseStream();
                readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8);
                ResHTML = readStream.ReadToEnd();
                resp.Close();
                readStream.Close();

            }
            catch (Exception exc)
            { ResHTML = exc.Message; }
            return ResHTML;
        }

        string GetAccessToken(out bool isGetToken)
        {
            HttpStatusCode statusCodeResult = HttpStatusCode.NotFound;
            isGetToken = false;

            String ResHTML = "";

            HttpWebRequest Req;
            HttpWebResponse resp;
            Stream receiveStream;
            StreamReader readStream;

            try
            {
                Req = (HttpWebRequest)WebRequest.Create("https://" + Host + PathServiceToken);
                Req.Method = "POST";
                Req.AllowAutoRedirect = true;
                Req.ContentType = " application/json";
                Models.CredentialForToken body = new Models.CredentialForToken() { login = login, password = password };
                byte[] sentData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
                Req.ContentLength = sentData.Length;
                Req.GetRequestStream().Write(sentData, 0, sentData.Length);
                Req.GetRequestStream().Close();
                resp = (HttpWebResponse)Req.GetResponse();
                statusCodeResult = resp.StatusCode;
                receiveStream = resp.GetResponseStream();
                readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8);
                ResHTML = readStream.ReadToEnd();
                resp.Close();
                readStream.Close();

            }
            catch (Exception exc)
            { ResHTML = exc.Message; }

            if (statusCodeResult == HttpStatusCode.OK)
            {
                var access = JsonConvert.DeserializeObject<Models.BodyResultToken200>(ResHTML).detail.access;
                isGetToken = true;
                return access;
            }

            return ResHTML;
        }

        public string ImageToBase64(Image image,
            System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        
    }
}
