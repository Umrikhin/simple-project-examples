using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using unisender.Models;

namespace unisender
{
    public class Operation
    {
        public static HttpClient _client = new HttpClient();
        public static string UrlWebApi = "https://api.unisender.com/ru";
        static string key = "ваш_ключ";

        //1. Создает рассылку
        public static async Task<RootCreateEmailMessage> createEmailMessage(string senderName, string sender_email, string subject, string HTMLBODY, string list_id)
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/createEmailMessage?format=json&api_key={0}&sender_name={1}&sender_email={2}&subject={3}&body={4}&list_id={5}&generate_text=1", key, senderName, sender_email, subject, HTMLBODY, list_id));

                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootCreateEmailMessage>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //2. Создает рассылку с использованием шаблона
        public static async Task<RootCreateEmailMessage> createEmailMessageTemplate(string senderName, string sender_email, string subject, string template_id, string list_id)
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/createEmailMessage?format=json&api_key={0}&sender_name={1}&sender_email={2}&subject={3}&template_id={4}&list_id={5}&generate_text=1", key, senderName, sender_email, subject, template_id, list_id));

                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootCreateEmailMessage>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //3. Запускает рассылку
        public static async Task<RootCreateCampaing> createCampaign(string message_id)
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/createCampaign?api_key={0}&message_id={1}", key, message_id));
                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootCreateCampaing>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //4. Получает статистику рассылки, с учетом пользовательских полей
        public static async Task<RootCampaignStatus> getCampaignDeliveryStats(string campaign_id, string field_ids)
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/async/getCampaignDeliveryStats?api_key={0}&campaign_id={1}&field_ids[]={2}", key, campaign_id, field_ids));
                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootCampaignStatus>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //5. Получет общую статистику рассылки
        public static async Task<RootStat> getCampaignCommonStats(string campaign_id)
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/getCampaignCommonStats?format=json&api_key={0}&campaign_id={1}", key, campaign_id));
                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootStat>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //6. Получет итоговый файл по рассылке
        public static async Task<RootTask> getTaskResult(string task_uuid)
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/async/getTaskResult?api_key={0}&task_uuid={1}", key, task_uuid));
                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootTask>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //7. Создает список контактов
        public static async Task<RootCreateList> createList()
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/createList?format=json&api_key={0}&title=PanaceaForSendingApi", key));
                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootCreateList>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //8. Получает список контактов
        public static async Task<RootGetList> getLists()
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/getLists?format=json&api_key={0}", key));
                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootGetList>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //9. Добавлет контакт в список контактов
        public static async Task<RootImportContact> importContacts(Contact contact, string list_id)
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/importContacts?format=json&api_key={0}&field_names[0]=email&field_names[1]=surname&field_names[2]=Name&field_names[3]=patronymic&field_names[4]=row&field_names[5]=mnt&field_names[6]=mo&field_names[7]=tel_mo&field_names[8]=email_list_ids&data[0][0]={1}&data[0][1]={2}&data[0][2]={3}&data[0][3]={4}&data[0][4]={5}&data[0][5]={6}&data[0][6]={7}&data[0][7]={8}&data[0][8]={9}", key, contact.Email, contact.Fam, contact.Im, contact.Ot, contact.row, contact.mnt, contact.mo, contact.tel_mo, list_id));
                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootImportContact>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //10. Удаляет список контактов
        public static async Task<RootDeleteList> deleteList(string list_id)
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/deleteList?format=json&api_key={0}&list_id={1}", key, list_id));
                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootDeleteList>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //11. Получет список шаблонов писем для рассылки
        public static async Task<RootResultTemplate> getTemplates()
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/getTemplates?format=json&api_key={0}", key));
                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootResultTemplate>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //12. Получет список пользовательских полей
        public static async Task<RootFields> getFields()
        {
            try
            {
                var response = await _client.GetAsync(UrlWebApi + string.Format("/api/getFields?format=json&api_key={0}", key));
                CheckStatusCode(response.StatusCode);
                var obj = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootFields>(obj);
                if (result.result != null) return result;
                var resultatEx = JsonConvert.DeserializeObject<ErrorEmailMessage>(obj);
                if (!string.IsNullOrEmpty(resultatEx.error))
                {
                    throw new Exception(resultatEx.error);
                }
                return result;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        private static void CheckStatusCode(HttpStatusCode statusCode)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Метод не выполнен: {statusCode}.");
            }
        }
    }
}
