using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;
using unisender.Models;

namespace unisender
{
    public partial class FormStart : Form
    {
        string list_id = string.Empty;
        int message_id = 0;
        int campaign_id = 0;
        string status = string.Empty;

        public FormStart()
        {
            InitializeComponent();
        }

        private async void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                //0. Удаляем список рассылки, если он имеется
                var resGetList = await Operation.getLists();
                if (resGetList != null)
                {
                    if (resGetList.result.Where(x => x.title.Equals("PanaceaForSendingApi")).Count() > 0)
                    {
                        var del_id_list = resGetList.result.Where(x => x.title.Equals("PanaceaForSendingApi"))?.FirstOrDefault()?.id.ToString();
                        if (del_id_list != null)
                        {
                            var resDeleteList = await Operation.deleteList(del_id_list);
                            if (resDeleteList == null) return;
                        }
                    }
                }
                else
                {
                    return;
                }
                //1. Создаем список для рассылки
                var resCreateList = await Operation.createList();
                if (resCreateList == null)
                {
                    return;
                }
                if (resCreateList.result.id > 0)
                {
                    list_id = resCreateList.result.id.ToString();
                }
                else 
                { 
                    MessageBox.Show("Список адресов для рассылки не создан.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                    return; 
                }
                //2. Добавляем в список адресатов
                //Список адресатов
                List<Contact> contacts = new List<Contact>();
                contacts.Add(new Contact() { Email = "bono39@yandex.ru", Fam = "Умрихин", Im = "Евгений", Ot = "Дмитриевич", row = "row-1", mnt = "сентябрь", mo = "ГБ1", tel_mo = "678-67-78" });
                contacts.Add(new Contact() { Email = "evgenij_u@mail.ru", Fam = "Иванов", Im = "Сергей", Ot = "Петрович", row = "row-2", mnt = "март", mo = "ГБ2", tel_mo = "274-57-30" });
                foreach (Contact contact in contacts)
                {
                    var resImportContact = await Operation.importContacts(contact, list_id);
                    if (resImportContact == null) return;
                    if (!(resImportContact.result.total > 0))
                    {
                        MessageBox.Show($"Адрес {contact.Email} для записи {contact.Fam} {contact.Im} {contact.Ot} не добавлен в список", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //3. Создаем сообщения для отправки
                //3.1. Получение шаблона письма
                string id_template = string.Empty;
                var resGetTemplates = await Operation.getTemplates();
                if (resGetTemplates != null)
                {
                    if (resGetTemplates.result.Where(x => x.title.Equals("Disp")).Count() > 0)
                    {
                        id_template = resGetTemplates.result.Where(x => x.title.Equals("Disp"))?.FirstOrDefault()?.id.ToString();
                    }
                }
                else
                {
                    return;
                }
                var resСreateEmailMessage = await Operation.createEmailMessageTemplate("Панацея", "admin@mco-panacea.ru", "Оповещение о диспансеризации", id_template, list_id);
                if (resСreateEmailMessage != null)
                {
                    if (resСreateEmailMessage.result?.message_id > 0) message_id = resСreateEmailMessage.result.message_id;
                    //4. Запускаем рассылку
                    var resСreateCampaign = await Operation.createCampaign(message_id.ToString());
                    if (resСreateCampaign != null)
                    {
                        if (resСreateCampaign.result?.campaign_id > 0) campaign_id = resСreateCampaign.result.campaign_id;
                    }
                    else
                    {
                        MessageBox.Show($"Рассылка не запущена.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"Рассылка не создана.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Сохранение идентификатора рассылки
                var txt = new TxtFile();
                txt.SaveId(campaign_id.ToString(), Application.StartupPath);
                MessageBox.Show($"Рассылка запущена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonGetResult_Click(object sender, EventArgs e)
        {
            try
            {
                //0. Получение дополнительного поля
                string fields_ids = string.Empty;
                var resGetFields = await Operation.getFields();
                if (resGetFields != null)
                {
                    if (resGetFields.result.Where(x => x.name.Equals("row")).Count() > 0)
                    {
                        fields_ids = resGetFields.result.Where(x => x.name.Equals("row"))?.FirstOrDefault()?.id.ToString();
                    }
                }
                //1. Получем общую статистику
                var resStat = await Operation.getCampaignCommonStats(campaign_id.ToString());
                if (resStat != null)
                {
                    MessageBox.Show($"Всего контактов: {resStat.result.total}, Отправлено: {resStat.result.sent}, доставлено: {resStat.result.delivered}.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Не возможно получить общую статистику.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //2. Получаем статус рассылки
                var resStatus = await Operation.getCampaignDeliveryStats(campaign_id.ToString(), fields_ids);
                if (resStatus == null)
                {
                    return;
                }
                MessageBox.Show(resStatus.result.status, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(resStatus.result.task_uuid)) 
                {
                    MessageBox.Show("Идентификатор задачи еще не получен.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //3. Получаем файл отчета
                var resTask = await Operation.getTaskResult(resStatus.result.task_uuid);
                if (resTask == null)
                {
                    return;
                }
                if (resTask.result.file_to_download != null)
                {
                    if (resTask.result.file_to_download.Length > 0)
                    {
                        MessageBox.Show($"Файл для скачивания: {resTask.result.file_to_download}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //4. Удаляем созданный список (Не обязательно)
                        //var resDeleteList = await Operation.deleteList(list_id);
                        //MessageBox.Show("Список адресов для рассылки удален.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //5. Скачиваем файл с отчетом
                        var webClient = new HttpClient();
                        var zipDir = System.IO.Path.Combine(Application.StartupPath, "Report");
                        if (!Directory.Exists(zipDir)) { Directory.CreateDirectory(zipDir); }
                        var csvFile = System.IO.Path.Combine(zipDir, "report.csv");
                        if (System.IO.File.Exists(csvFile)) System.IO.File.Delete(csvFile);
                        await webClient.DownloadFileTaskAsync(new Uri(resTask.result.file_to_download), csvFile);
                        MessageBox.Show($"Файл отчета скачан в {zipDir}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //6. Конвертация в XML
                        //TxtFile t = new TxtFile();
                        //var xmlFile = System.IO.Path.Combine(zipDir, "report.xml");
                        //if (System.IO.File.Exists(xmlFile)) System.IO.File.Delete(xmlFile);
                        //t.CsvToXml(csvFile, xmlFile);

                        //6. Конвертация в DataSet
                        DataSet ds = new DataSet("uniSend");
                        var dt = TxtFile.ConvertCSVtoDataTable(csvFile);
                        var xmlFile = System.IO.Path.Combine(zipDir, "report.xml");
                        if (System.IO.File.Exists(xmlFile)) System.IO.File.Delete(xmlFile);
                        ds.Tables.Add(dt);  
                        ds.WriteXml(xmlFile);
                    }
                }
                else
                {
                    MessageBox.Show("Ссылка на отчет еще не доступна.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FormStart_Load(object sender, EventArgs e)
        {
            var txt = new TxtFile();
            var id = await txt.GetId(Application.StartupPath);
            if (!string.IsNullOrEmpty(id)) { campaign_id = Convert.ToInt32(id); }
        }
    }
}