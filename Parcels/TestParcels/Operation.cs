using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestParcels.Models;

namespace TestParcels
{
    internal class Operation
    {
        public static HttpClient _client = new HttpClient();
        public static string UrlWebApi = "http://localhost:5276";
        static string key = "alfa"; 

        public static async Task<Guid> InsertFile(BodyFileParcel bodyFileParcel)
        {
            Guid result = Guid.Empty;
            try
            {
                if (!_client.DefaultRequestHeaders.Contains("XApiKey")) { _client.DefaultRequestHeaders.Add("XApiKey", key); }
                HttpContent content = new StringContent(JsonConvert.SerializeObject(bodyFileParcel), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(string.Format(UrlWebApi + "/api/parcels/post"), content);
                var resultat = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new Exception(resultat);
                    }
                    throw new Exception("Добавление не было произведено по неизвестной причине.");
                }
                result =  Guid.Parse(JsonConvert.DeserializeObject<string>(resultat) ?? Guid.Empty.ToString());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        public static async Task<List<FileParcel>> GetFileParcels(string CTERR, int day)
        {
            try
            {
                if (!_client.DefaultRequestHeaders.Contains("XApiKey")) { _client.DefaultRequestHeaders.Add("XApiKey", key); }
                //Получение данных с помощью сервиса
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/parcels/get?CTERR={0}&day={1}", CTERR, day));
                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var parcels = JsonConvert.DeserializeObject<List<FileParcel>>(obj) ?? new List<FileParcel>();
                return parcels;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<FileParcel>();
            }
        }

        public static async Task<RetFile> GetFileParcel(Guid Id)
        {
            try
            {
                if (!_client.DefaultRequestHeaders.Contains("XApiKey")) { _client.DefaultRequestHeaders.Add("XApiKey", key); }
                //Получение данных с помощью сервиса
                var response = await _client.GetAsync(string.Format(UrlWebApi + "/api/parcels/getfile/{0}", Id));
                var obj = await response.Content.ReadAsStringAsync();
                CheckStatusCode(response.StatusCode, obj);
                var parcel = JsonConvert.DeserializeObject<RetFile>(obj) ?? new RetFile();
                return parcel;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new RetFile();
            }
        }

        public static async Task<Guid> UpdateFileParcel(Guid Id, FileParcel fileParcel)
        {
            Guid result = Guid.Empty;
            try
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(fileParcel), Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(string.Format(UrlWebApi + "/api/parcels/put/{0}", Id), content);
                var resultat = await response.Content.ReadAsStringAsync();
                CheckStatusCode(response.StatusCode, resultat);
                result = Guid.Parse(JsonConvert.DeserializeObject<string>(resultat) ?? Guid.Empty.ToString());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private static void CheckStatusCode(HttpStatusCode statusCode)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Метод не выполнен: {statusCode}.");
            }
        }
        private static void CheckStatusCode(HttpStatusCode statusCode, string resultat)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                if (statusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception($"Данных не найдено. {resultat}");
                }
                if (statusCode == HttpStatusCode.BadRequest)
                {
                    throw new Exception(resultat);
                }
                throw new Exception($"Метод не выполнен: {statusCode}.");
            }
        }
    }
}
