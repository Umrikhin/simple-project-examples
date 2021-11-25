using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ufanet_recognizer.Models;

namespace ufanet_recognizer.Infrastructure
{
    public class Utils
    {
        static string Host = "passrec.ufanet.ru/api/v0";

        //Пути для служб
        static string PathService = "/passports/recognition/";
        static string PathServiceToken = "/token/";

        public static async Task<string> GetResponseAccessToken(string login, string password)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new Models.CredentialForToken() { login = login, password = password };

                using (var response = await client.PostAsync("https://" + Host + PathServiceToken, new StringContent(JsonConvert.SerializeObject(content),
                                    Encoding.UTF8,
                                    "application/json")))
                {
                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<BodyResultToken200>(await response.Content.ReadAsStringAsync()).detail.access;
                }
            }
        }

        public static async Task<Passport> GetResponseResultat(string token, string image_base64)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "JWT " + token);

                var content = new Models.BodyRequest() { file = image_base64, doc_type = "passport" };

                using (var response = await client.PostAsync("https://" + Host + PathService, new StringContent(JsonConvert.SerializeObject(content),
                                    Encoding.UTF8,
                                    "application/json")))
                {
                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<PassportRecognition>(await response.Content.ReadAsStringAsync()).detail;
                }
            }
        }

        public static List<ViewResultRec> GetResult(Passport passport)
        {
            List<ViewResultRec> resultat = new List<ViewResultRec>();
            resultat.Add(new ViewResultRec() { Field = "Серия и номер", RecValue = passport.series_number });
            resultat.Add(new ViewResultRec() { Field = "Кем выдан", RecValue = passport.authority });
            resultat.Add(new ViewResultRec() { Field = "Дата выдачи", RecValue = passport.issue_date });
            resultat.Add(new ViewResultRec() { Field = "Код подразделения", RecValue = passport.authority_code });
            resultat.Add(new ViewResultRec() { Field = "Фамилия", RecValue = passport.surname });
            resultat.Add(new ViewResultRec() { Field = "Имя", RecValue = passport.name });
            resultat.Add(new ViewResultRec() { Field = "Отчество", RecValue = passport.patronymic });
            resultat.Add(new ViewResultRec() { Field = "Пол", RecValue = passport.gender });
            resultat.Add(new ViewResultRec() { Field = "Дата рождения", RecValue = passport.birthday });
            resultat.Add(new ViewResultRec() { Field = "Место рождения", RecValue = passport.birthplace });
            return resultat;
        }

    }

}
