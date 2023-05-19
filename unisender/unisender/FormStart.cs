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
                //0. ������� ������ ��������, ���� �� �������
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
                //1. ������� ������ ��� ��������
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
                    MessageBox.Show("������ ������� ��� �������� �� ������.", "��������", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                    return; 
                }
                //2. ��������� � ������ ���������
                //������ ���������
                List<Contact> contacts = new List<Contact>();
                contacts.Add(new Contact() { Email = "bono39@yandex.ru", Fam = "�������", Im = "�������", Ot = "����������", row = "row-1", mnt = "��������", mo = "��1", tel_mo = "678-67-78" });
                contacts.Add(new Contact() { Email = "evgenij_u@mail.ru", Fam = "������", Im = "������", Ot = "��������", row = "row-2", mnt = "����", mo = "��2", tel_mo = "274-57-30" });
                foreach (Contact contact in contacts)
                {
                    var resImportContact = await Operation.importContacts(contact, list_id);
                    if (resImportContact == null) return;
                    if (!(resImportContact.result.total > 0))
                    {
                        MessageBox.Show($"����� {contact.Email} ��� ������ {contact.Fam} {contact.Im} {contact.Ot} �� �������� � ������", "��������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //3. ������� ��������� ��� ��������
                //3.1. ��������� ������� ������
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
                var res�reateEmailMessage = await Operation.createEmailMessageTemplate("�������", "admin@mco-panacea.ru", "���������� � ���������������", id_template, list_id);
                if (res�reateEmailMessage != null)
                {
                    if (res�reateEmailMessage.result?.message_id > 0) message_id = res�reateEmailMessage.result.message_id;
                    //4. ��������� ��������
                    var res�reateCampaign = await Operation.createCampaign(message_id.ToString());
                    if (res�reateCampaign != null)
                    {
                        if (res�reateCampaign.result?.campaign_id > 0) campaign_id = res�reateCampaign.result.campaign_id;
                    }
                    else
                    {
                        MessageBox.Show($"�������� �� ��������.", "��������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"�������� �� �������.", "��������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //���������� �������������� ��������
                var txt = new TxtFile();
                txt.SaveId(campaign_id.ToString(), Application.StartupPath);
                MessageBox.Show($"�������� ��������.", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonGetResult_Click(object sender, EventArgs e)
        {
            try
            {
                //0. ��������� ��������������� ����
                string fields_ids = string.Empty;
                var resGetFields = await Operation.getFields();
                if (resGetFields != null)
                {
                    if (resGetFields.result.Where(x => x.name.Equals("row")).Count() > 0)
                    {
                        fields_ids = resGetFields.result.Where(x => x.name.Equals("row"))?.FirstOrDefault()?.id.ToString();
                    }
                }
                //1. ������� ����� ����������
                var resStat = await Operation.getCampaignCommonStats(campaign_id.ToString());
                if (resStat != null)
                {
                    MessageBox.Show($"����� ���������: {resStat.result.total}, ����������: {resStat.result.sent}, ����������: {resStat.result.delivered}.", "��������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("�� �������� �������� ����� ����������.", "��������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //2. �������� ������ ��������
                var resStatus = await Operation.getCampaignDeliveryStats(campaign_id.ToString(), fields_ids);
                if (resStatus == null)
                {
                    return;
                }
                MessageBox.Show(resStatus.result.status, "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(resStatus.result.task_uuid)) 
                {
                    MessageBox.Show("������������� ������ ��� �� �������.", "��������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //3. �������� ���� ������
                var resTask = await Operation.getTaskResult(resStatus.result.task_uuid);
                if (resTask == null)
                {
                    return;
                }
                if (resTask.result.file_to_download != null)
                {
                    if (resTask.result.file_to_download.Length > 0)
                    {
                        MessageBox.Show($"���� ��� ����������: {resTask.result.file_to_download}", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //4. ������� ��������� ������ (�� �����������)
                        //var resDeleteList = await Operation.deleteList(list_id);
                        //MessageBox.Show("������ ������� ��� �������� ������.", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //5. ��������� ���� � �������
                        var webClient = new HttpClient();
                        var zipDir = System.IO.Path.Combine(Application.StartupPath, "Report");
                        if (!Directory.Exists(zipDir)) { Directory.CreateDirectory(zipDir); }
                        var csvFile = System.IO.Path.Combine(zipDir, "report.csv");
                        if (System.IO.File.Exists(csvFile)) System.IO.File.Delete(csvFile);
                        await webClient.DownloadFileTaskAsync(new Uri(resTask.result.file_to_download), csvFile);
                        MessageBox.Show($"���� ������ ������ � {zipDir}", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //6. ����������� � XML
                        //TxtFile t = new TxtFile();
                        //var xmlFile = System.IO.Path.Combine(zipDir, "report.xml");
                        //if (System.IO.File.Exists(xmlFile)) System.IO.File.Delete(xmlFile);
                        //t.CsvToXml(csvFile, xmlFile);

                        //6. ����������� � DataSet
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
                    MessageBox.Show("������ �� ����� ��� �� ��������.", "��������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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