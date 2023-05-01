using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using TestParcels.Models;

namespace TestParcels
{
    public class OperationRest
    {
        string _UrlWebApi = "http://localhost:5276";
        string _key = "alfa";
        int _timeout = 300000;

        public OperationRest(string UrlWebApi, string key, int timeout)
        {
            _UrlWebApi = UrlWebApi;
            _key = key;
            _timeout = timeout;
        }

        public Guid InsertFile(BodyFileParcel bodyFileParcel)
        {
            Guid result = Guid.Empty;
            HttpWebRequest Req;
            HttpWebResponse resp;
            Stream receiveStream;
            StreamReader readStream;
            try
            {
                Req = (HttpWebRequest)WebRequest.Create(string.Format(_UrlWebApi + "/api/parcels/post"));
                Req.Timeout = _timeout;
                Req.Method = "POST";
                Req.AllowAutoRedirect = true;
                if (string.IsNullOrEmpty(Req.Headers.Get("XApiKey"))) Req.Headers.Add("XApiKey", _key);
                Req.ContentType = "application/json";
                byte[] sentData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bodyFileParcel));
                Req.ContentLength = sentData.Length;
                Req.GetRequestStream().Write(sentData, 0, sentData.Length);
                Req.GetRequestStream().Close();
                resp = (HttpWebResponse)Req.GetResponse();
                var statusCode = resp.StatusCode;
                receiveStream = resp.GetResponseStream();
                readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8);
                var resultat = readStream.ReadToEnd();
                resp.Close();
                readStream.Close();
                if (statusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Добавление не было произведено по неизвестной причине: {statusCode}.");
                }
                result = Guid.Parse(JsonConvert.DeserializeObject<string>(resultat));
            }
            catch (WebException we)
            {
                var wResp = (HttpWebResponse)we.Response;
                var wRespStatusCode = wResp.StatusCode;
                var wStream = wResp.GetResponseStream();
                var rStream = new StreamReader(wStream, System.Text.Encoding.UTF8);
                var resultat = rStream.ReadToEnd();
                if (wRespStatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show($"Данных не найдено. {resultat}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (wRespStatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show(resultat, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Метод не выполнен: {wRespStatusCode}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        public List<FileParcel> GetFileParcels(string CTERR, int day)
        {
            HttpWebRequest Req;
            HttpWebResponse resp;
            Stream receiveStream;
            StreamReader readStream;
            try
            {
                Req = (HttpWebRequest)WebRequest.Create(_UrlWebApi + string.Format("/api/parcels/get?CTERR={0}&day={1}", CTERR, day));
                Req.Timeout = _timeout;
                Req.Method = "GET";
                Req.AllowAutoRedirect = true;
                if (string.IsNullOrEmpty(Req.Headers.Get("XApiKey"))) Req.Headers.Add("XApiKey", _key);
                Req.ContentType = "application/json";
                resp = (HttpWebResponse)Req.GetResponse();
                var statusCode = resp.StatusCode;
                receiveStream = resp.GetResponseStream();
                readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8);
                var resultat = readStream.ReadToEnd();
                resp.Close();
                readStream.Close();
                if (statusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Метод не выполнен: {statusCode}.");
                }
                var parcels = JsonConvert.DeserializeObject<List<FileParcel>>(resultat);
                return parcels;
            }
            catch (WebException we)
            {
                var wResp = (HttpWebResponse)we.Response;
                var wRespStatusCode = wResp.StatusCode;
                var wStream = wResp.GetResponseStream();
                var rStream = new StreamReader(wStream, System.Text.Encoding.UTF8);
                var resultat = rStream.ReadToEnd();
                if (wRespStatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show($"Данных не найдено. {resultat}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (wRespStatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show(resultat, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Метод не выполнен: {wRespStatusCode}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return new List<FileParcel>();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<FileParcel>();
            }
        }

        public RetFile GetFileParcel(Guid Id)
        {
            HttpWebRequest Req;
            HttpWebResponse resp = null;
            Stream receiveStream;
            StreamReader readStream;
            try
            {
                Req = (HttpWebRequest)WebRequest.Create(string.Format(_UrlWebApi + "/api/parcels/getfile/{0}", Id));
                Req.Timeout = _timeout;
                Req.Method = "GET";
                Req.AllowAutoRedirect = true;
                if (string.IsNullOrEmpty(Req.Headers.Get("XApiKey"))) Req.Headers.Add("XApiKey", _key);
                Req.ContentType = "application/json";
                resp = (HttpWebResponse)Req.GetResponse();
                var statusCode = resp.StatusCode;
                receiveStream = resp.GetResponseStream();
                readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8);
                var resultat = readStream.ReadToEnd();
                resp.Close();
                readStream.Close();
                if (statusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Метод не выполнен: {statusCode}.");
                }
                var parcel = JsonConvert.DeserializeObject<RetFile>(resultat);
                return parcel;
            }
            catch (WebException we)
            {
                var wResp = (HttpWebResponse)we.Response;
                var wRespStatusCode = wResp.StatusCode;
                var wStream = wResp.GetResponseStream();
                var rStream = new StreamReader(wStream, System.Text.Encoding.UTF8);
                var resultat = rStream.ReadToEnd();
                if (wRespStatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show($"Данных не найдено. {resultat}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (wRespStatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show(resultat, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Метод не выполнен: {wRespStatusCode}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return new RetFile();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new RetFile();
            }
        }

        public Guid UpdateFileParcel(Guid Id, FileParcel fileParcel)
        {
            Guid result = Guid.Empty;
            HttpWebRequest Req;
            HttpWebResponse resp;
            Stream receiveStream;
            StreamReader readStream;
            try
            {
                Req = (HttpWebRequest)WebRequest.Create(string.Format(_UrlWebApi + "/api/parcels/put/{0}", Id));
                Req.Timeout = _timeout;
                Req.Method = "PUT";
                Req.AllowAutoRedirect = true;
                if (string.IsNullOrEmpty(Req.Headers.Get("XApiKey"))) Req.Headers.Add("XApiKey", _key);
                Req.ContentType = "application/json";
                byte[] sentData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(fileParcel));
                Req.ContentLength = sentData.Length;
                Req.GetRequestStream().Write(sentData, 0, sentData.Length);
                Req.GetRequestStream().Close();
                resp = (HttpWebResponse)Req.GetResponse();
                var statusCode = resp.StatusCode;
                receiveStream = resp.GetResponseStream();
                readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8);
                var resultat = readStream.ReadToEnd();
                resp.Close();
                readStream.Close();
                if (statusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Метод не выполнен: {statusCode}.");
                }
                result = Guid.Parse(JsonConvert.DeserializeObject<string>(resultat));
            }
            catch (WebException we)
            {
                var wResp = (HttpWebResponse)we.Response;
                var wRespStatusCode = wResp.StatusCode;
                var wStream = wResp.GetResponseStream();
                var rStream = new StreamReader(wStream, System.Text.Encoding.UTF8);
                var resultat = rStream.ReadToEnd();
                if (wRespStatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show($"Данных не найдено. {resultat}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (wRespStatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show(resultat, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Метод не выполнен: {wRespStatusCode}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
    }
}
