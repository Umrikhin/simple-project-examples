using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Spire.Xls;
using System.IO;

namespace LibGenerateZaFoms.Utils
{
    public class ActionZaVkl
    {
        static void SetDoc(ref Worksheet sh, LibGenerateZaFoms.Models.Doc doc)
        {
            if (doc == null) return;
            //Тип документа
            sh.Range["H29"].Text = doc.TypeDoc;
            //Серия
            sh.Range["D30"].Text = doc.SerDoc;
            //Номер
            sh.Range["I30"].Text = doc.NumDoc;
            //Дата выдачи
            sh.Range["R30"].Text = doc.DateDoc;
            //Кем выдан
            sh.Range["D31"].Text = doc.NpDoc;
        }

        static void SetDoc2(ref Worksheet sh, LibGenerateZaFoms.Models.Doc doc)
        {
            if (doc == null) return;
            //Тип документа
            sh.Range["E51"].Text = doc.TypeDoc;
            //Серия
            sh.Range["D52"].Text = doc.SerDoc;
            //Номер
            sh.Range["M52"].Text = doc.NumDoc;
            //Кем и когда выдан
            sh.Range["F53"].Text = doc.NpDoc + ", " + doc.DateDoc;
        }

        static void SetRegAddress(ref Worksheet sh, LibGenerateZaFoms.Models.RegAddress address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(35, 5, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P35"].Text = address.Subj;
            //Район
            sh.Range["D37"].Text = address.Rayon;
            //Город
            sh.Range["P37"].Text = address.Town;
            //Населенный пункт
            sh.Range["F38"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P38"].Text = address.Street;
            //Дом
            sh.Range["F40"].Text = address.House;
            //Корпус
            sh.Range["N40"].Text = address.Korp;
            //Квартира
            sh.Range["T40"].Text = address.Kv;
            //Дата регистрации
            sh.Range["I41"].Text = address.DateReg;
        }

        static void SetFactAddress(ref Worksheet sh, LibGenerateZaFoms.Models.Address address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(44, 5, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P44"].Text = address.Subj;
            //Район
            sh.Range["D46"].Text = address.Rayon;
            //Город
            sh.Range["P46"].Text = address.Town;
            //Населенный пункт
            sh.Range["F47"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P47"].Text = address.Street;
            //Дом
            sh.Range["F49"].Text = address.House;
            //Корпус
            sh.Range["N49"].Text = address.Korp;
            //Квартира
            sh.Range["T49"].Text = address.Kv;
        }

        static void SetNotif(ref Worksheet sh, LibGenerateZaFoms.Models.Notif notif)
        {
            if (notif == null) return;
            //Флажки для способов оповещения
            Spire.Xls.Core.ICheckBox checkboxSms = sh.CheckBoxes.AddCheckBox(75, 1, 20, 20);
            checkboxSms.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxMail = sh.CheckBoxes.AddCheckBox(75, 14, 20, 20);
            checkboxMail.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxEmail = sh.CheckBoxes.AddCheckBox(76, 1, 20, 20);
            checkboxEmail.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxCall = sh.CheckBoxes.AddCheckBox(76, 14, 20, 20);
            checkboxCall.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxMsg = sh.CheckBoxes.AddCheckBox(77, 1, 20, 20);
            checkboxMsg.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxOther = sh.CheckBoxes.AddCheckBox(77, 14, 20, 20);
            checkboxOther.CheckState = Spire.Xls.CheckState.Unchecked;
            switch (notif.info)
            {
                case Utils.Inform.sms:
                    checkboxSms.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Inform.mail:
                    checkboxMail.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Inform.email:
                    checkboxEmail.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Inform.call:
                    checkboxCall.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Inform.msg:
                    checkboxMsg.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Inform.other:
                    checkboxOther.CheckState = Spire.Xls.CheckState.Checked;
                    break;
            }
            if (notif.info == Utils.Inform.other)
            {
                sh.Range["O78"].Text = notif.DespriptionOther;
            }
        }

        static void SetAgent(ref Worksheet sh, LibGenerateZaFoms.Models.Agent agent)
        {
            if (agent == null) return;
            //Фамилия
            sh.Range["D80"].Text = agent.Famip;
            //Имя
            sh.Range["L80"].Text = agent.Namep;
            //Отчество
            sh.Range["F82"].Text = agent.Otchp;
            //Пол
            Spire.Xls.Core.ICheckBox checkboxSexAM = sh.CheckBoxes.AddCheckBox(84, 4, 20, 20);
            checkboxSexAM.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxSexAF = sh.CheckBoxes.AddCheckBox(84, 6, 20, 20);
            checkboxSexAF.CheckState = Spire.Xls.CheckState.Unchecked;
            if (agent.Sex.ToLower().Equals("м"))
            {
                checkboxSexAM.CheckState = Spire.Xls.CheckState.Checked;
            }
            else
            {
                checkboxSexAF.CheckState = Spire.Xls.CheckState.Checked;
            }
            //Дата роджения
            sh.Range["N84"].Text = agent.DR;
            //Гражданство
            sh.Range["E86"].Text = agent.Land;
            //Статус представителя
            Spire.Xls.Core.ICheckBox checkboxMother = sh.CheckBoxes.AddCheckBox(88, 13, 20, 20);
            checkboxMother.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxFather = sh.CheckBoxes.AddCheckBox(89, 13, 20, 20);
            checkboxFather.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxOpekun = sh.CheckBoxes.AddCheckBox(88, 17, 20, 20);
            checkboxOpekun.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxPopechitel = sh.CheckBoxes.AddCheckBox(89, 17, 20, 20);
            checkboxPopechitel.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxUsinovitel = sh.CheckBoxes.AddCheckBox(88, 21, 20, 20);
            checkboxUsinovitel.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxByProxy = sh.CheckBoxes.AddCheckBox(89, 21, 20, 20);
            checkboxByProxy.CheckState = Spire.Xls.CheckState.Unchecked;
            switch (agent.status)
            {
                case Utils.AgentStatus.Mother:
                    checkboxMother.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.AgentStatus.Father:
                    checkboxFather.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.AgentStatus.Opekun:
                    checkboxOpekun.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.AgentStatus.Popechitel:
                    checkboxPopechitel.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.AgentStatus.Usinovitel:
                    checkboxUsinovitel.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.AgentStatus.ByProxy:
                    checkboxByProxy.CheckState = Spire.Xls.CheckState.Checked;
                    break;
            }

            //Документ
            //Тип документа
            sh.Range["S90"].Text = agent.doc.TypeDoc;
            //Серия
            sh.Range["D91"].Text = agent.doc.SerDoc;
            //Номер
            sh.Range["I91"].Text = agent.doc.NumDoc;
            //Дата выдачи
            sh.Range["R91"].Text = agent.doc.DateDoc;
            //Кем выдан
            sh.Range["D92"].Text = agent.doc.NpDoc;

            //Документ статуса
            //Серия
            sh.Range["D94"].Text = agent.docStatusSer;
            //Номер
            sh.Range["I94"].Text = agent.docStatusNum;
            //Дата выдачи
            sh.Range["Q94"].Text = agent.docStatusDbeg;

            //СНИЛС
            sh.Range["O95"].Text = agent.Snils;

            //ЕНП
            sh.Range["M96"].Text = agent.ENP;

            //Адрес регистрации
            SetRegAgentAddress(ref sh, agent.regAddress);
            //Бомж
            Spire.Xls.Core.ICheckBox checkboxABomg = sh.CheckBoxes.AddCheckBox(105, 2, 20, 20);
            checkboxABomg.CheckState = Spire.Xls.CheckState.Unchecked;
            if (agent.Bomg > 0) checkboxABomg.CheckState = Spire.Xls.CheckState.Checked;
            //Адрес пребывания
            SetFactAgentAddress(ref sh, agent.factAddress);

            //Контакты
            sh.Range["G113"].Text = agent.PhoneMob;
            sh.Range["M113"].Text = agent.PhoneHome;
            sh.Range["S113"].Text = agent.PhoneWork;
            sh.Range["G114"].Text = agent.Email;
        }

        static void SetRegAgentAddress(ref Worksheet sh, LibGenerateZaFoms.Models.RegAddress address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(98, 5, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P98"].Text = address.Subj;
            //Район
            sh.Range["D100"].Text = address.Rayon;
            //Город
            sh.Range["P100"].Text = address.Town;
            //Населенный пункт
            sh.Range["F101"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P101"].Text = address.Street;
            //Дом
            sh.Range["F103"].Text = address.House;
            //Корпус
            sh.Range["N103"].Text = address.Korp;
            //Квартира
            sh.Range["T103"].Text = address.Kv;
            //Дата регистрации
            sh.Range["I104"].Text = address.DateReg;
        }

        static void SetFactAgentAddress(ref Worksheet sh, LibGenerateZaFoms.Models.Address address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(107, 5, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P107"].Text = address.Subj;
            //Район
            sh.Range["D109"].Text = address.Rayon;
            //Город
            sh.Range["P109"].Text = address.Town;
            //Населенный пункт
            sh.Range["F110"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P110"].Text = address.Street;
            //Дом
            sh.Range["F112"].Text = address.House;
            //Корпус
            sh.Range["N112"].Text = address.Korp;
            //Квартира
            sh.Range["T112"].Text = address.Kv;
        }

        public static void CreatePDF(LibGenerateZaFoms.Models.ZaVkl z, bool showFile = true)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(z.PathTemplate);
            Worksheet sheet = workbook.Worksheets[0];

            //Шапка заявления
            sheet.Range["K1"].Text = z.SmoName;
            sheet.Range["K3"].Text = (z.Famip + " " + z.Namep + " " + z.Otchp).Replace("  ", " ").Trim();

            Spire.Xls.Core.ICheckBox checkboxVipiska = sheet.CheckBoxes.AddCheckBox(8, 4, 20, 20);
            checkboxVipiska.CheckState = Spire.Xls.CheckState.Checked;

            //ФИО
            sheet.Range["D11"].Text = z.Famip;
            sheet.Range["L11"].Text = z.Namep;
            sheet.Range["F13"].Text = z.Otchp;

            //Пол
            Spire.Xls.Core.ICheckBox checkboxSexM = sheet.CheckBoxes.AddCheckBox(13, 17, 20, 20);
            checkboxSexM.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxSexF = sheet.CheckBoxes.AddCheckBox(13, 19, 20, 20);
            checkboxSexF.CheckState = Spire.Xls.CheckState.Unchecked;
            if (z.Sex.ToLower().Equals("м"))
            {
                checkboxSexM.CheckState = Spire.Xls.CheckState.Checked;
            }
            else
            {
                checkboxSexF.CheckState = Spire.Xls.CheckState.Checked;
            }

            //Флажки для категории ЗЛ
            Spire.Xls.Core.ICheckBox checkboxCat1 = sheet.CheckBoxes.AddCheckBox(16, 1, 20, 20);
            checkboxCat1.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat2 = sheet.CheckBoxes.AddCheckBox(17, 1, 20, 20);
            checkboxCat2.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat3 = sheet.CheckBoxes.AddCheckBox(18, 1, 20, 20);
            checkboxCat3.Top = checkboxCat3.Top + 20;
            checkboxCat3.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat4 = sheet.CheckBoxes.AddCheckBox(19, 1, 20, 20);
            checkboxCat4.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat5 = sheet.CheckBoxes.AddCheckBox(20, 1, 20, 20);
            checkboxCat5.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat6 = sheet.CheckBoxes.AddCheckBox(21, 1, 20, 20);
            checkboxCat6.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat7 = sheet.CheckBoxes.AddCheckBox(22, 1, 20, 20);
            checkboxCat7.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat8 = sheet.CheckBoxes.AddCheckBox(23, 1, 20, 20);
            checkboxCat8.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat9 = sheet.CheckBoxes.AddCheckBox(16, 13, 20, 20);
            checkboxCat9.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat10 = sheet.CheckBoxes.AddCheckBox(17, 13, 20, 20);
            checkboxCat10.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat11 = sheet.CheckBoxes.AddCheckBox(18, 13, 20, 20);
            checkboxCat11.Top = checkboxCat11.Top + 20;
            checkboxCat1.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat12 = sheet.CheckBoxes.AddCheckBox(19, 13, 20, 20);
            checkboxCat12.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat13 = sheet.CheckBoxes.AddCheckBox(20, 13, 20, 20);
            checkboxCat13.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat14 = sheet.CheckBoxes.AddCheckBox(21, 13, 20, 20);
            checkboxCat14.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat15 = sheet.CheckBoxes.AddCheckBox(22, 13, 20, 20);
            checkboxCat15.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat16 = sheet.CheckBoxes.AddCheckBox(23, 13, 20, 20);
            checkboxCat16.CheckState = Spire.Xls.CheckState.Unchecked;

            switch (z.cat)
            {
                case Utils.Cat.cat1:
                    checkboxCat1.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat2:
                    checkboxCat2.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat3:
                    checkboxCat3.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat4:
                    checkboxCat4.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat5:
                    checkboxCat5.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat6:
                    checkboxCat6.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat7:
                    checkboxCat7.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat8:
                    checkboxCat8.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat9:
                    checkboxCat9.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat10:
                    checkboxCat10.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat11:
                    checkboxCat11.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat12:
                    checkboxCat12.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat13:
                    checkboxCat13.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat14:
                    checkboxCat14.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat15:
                    checkboxCat15.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.Cat.cat16:
                    checkboxCat16.CheckState = Spire.Xls.CheckState.Checked;
                    break;
            }

            //Окончание флажков для категории ЗЛ

            //Дата рождения
            sheet.Range["E26"].Text = z.DR;
            //Место рождения
            sheet.Range["L26"].Text = z.BirthPlac;
            //ДУЛ
            LibGenerateZaFoms.Utils.ActionZaVkl.SetDoc(ref sheet, z.doc);
            //Гражданство
            sheet.Range["E32"].Text = z.Land;
            //Адрес регистрации
            LibGenerateZaFoms.Utils.ActionZaVkl.SetRegAddress(ref sheet, z.regAddress);
            //Бомж
            Spire.Xls.Core.ICheckBox checkboxBomg = sheet.CheckBoxes.AddCheckBox(42, 2, 20, 20);
            checkboxBomg.CheckState = Spire.Xls.CheckState.Unchecked;
            if (z.Bomg > 0) checkboxBomg.CheckState = Spire.Xls.CheckState.Checked;
            //Адрес пребывания
            LibGenerateZaFoms.Utils.ActionZaVkl.SetFactAddress(ref sheet, z.factAddress);
            //Сведения о втором документе
            LibGenerateZaFoms.Utils.ActionZaVkl.SetDoc2(ref sheet, z.doc2);

            //Начало вида на жительство
            sheet.Range["E56"].Text = z.VidStart;
            //Окончание вида на жительство
            sheet.Range["M56"].Text = z.VidEnd;

            //Трудовой договор
            sheet.Range["C59"].Text = z.TrDogNum;
            sheet.Range["K59"].Text = z.TrDogSign;
            sheet.Range["P59"].Text = z.TrDogBeg;
            sheet.Range["T59"].Text = z.TrDogEnd;
            sheet.Range["H60"].Text = z.TrDogLocation;

            //ЕАЭС
            sheet.Range["D62"].Text = z.EaesSer;
            sheet.Range["M62"].Text = z.EaesNum;
            sheet.Range["B66"].Text = z.EaesCat;

            //Место пребывания
            sheet.Range["B68"].Text = z.PlaceOfStay;
            sheet.Range["P69"].Text = z.DbegOfStay;
            sheet.Range["T69"].Text = z.DendOfStay;

            //СНИЛС
            sheet.Range["O70"].Text = z.Snils;

            //Контакты
            sheet.Range["G72"].Text = z.PhoneMob;
            sheet.Range["M72"].Text = z.PhoneHome;
            sheet.Range["S72"].Text = z.PhoneWork;
            sheet.Range["G73"].Text = z.Email;

            //Оповещения
            LibGenerateZaFoms.Utils.ActionZaVkl.SetNotif(ref sheet, z.notif);

            //Представитель
            if (z.agent != null) LibGenerateZaFoms.Utils.ActionZaVkl.SetAgent(ref sheet, z.agent);

            //Расшифровка подписи
            string full_name = z.Famip;
            if (z.Namep.Length > 0) full_name = full_name + " " + z.Namep.Substring(0, 1) + ".";
            if (z.Otchp.Length > 0) full_name = full_name + " " + z.Otchp.Substring(0, 1) + ".";
            if (z.agent != null)
            {
                if (z.agent.Famip.Length > 0) full_name = z.agent.Famip;
                if (z.agent.Namep.Length > 0) full_name = full_name + " " + z.agent.Namep.Substring(0, 1) + ".";
                if (z.agent.Otchp.Length > 0) full_name = full_name + " " + z.agent.Otchp.Substring(0, 1) + ".";
            }
            sheet.Range["H116"].Text = full_name;
            sheet.Range["I122"].Text = full_name;
            sheet.Range["I125"].Text = full_name;

            //Дата заявления
            sheet.Range["Q116"].Text = z.DZ;
            //Менеджер
            sheet.Range["N118"].Text = z.Manager;

            //Согласие
            Spire.Xls.Core.ICheckBox checkboxSogl1 = sheet.CheckBoxes.AddCheckBox(121, 1, 20, 20);
            checkboxSogl1.Top = checkboxSogl1.Top + 20;
            checkboxSogl1.CheckState = Spire.Xls.CheckState.Checked;
            Spire.Xls.Core.ICheckBox checkboxSogl2 = sheet.CheckBoxes.AddCheckBox(124, 1, 20, 20);
            checkboxSogl2.CheckState = Spire.Xls.CheckState.Checked;

            sheet.PageSetup.TopMargin = 0.10;
            sheet.PageSetup.BottomMargin = 0.10;
            sheet.PageSetup.LeftMargin = 0.45;

            DateTime dR = DateTime.MinValue;
            DateTime.TryParse(z.DR, out dR);
            string filePdf = z.Famip + z.Namep + z.Otchp + dR.ToString("ddMMyyyy") + ".pdf";

            if (!Directory.Exists(z.PathOut)) Directory.CreateDirectory(z.PathOut);

            //Очистка от старых файлов
            foreach (var file in new DirectoryInfo(z.PathOut).GetFiles().Where(x => x.LastWriteTime < DateTime.Now.Date))
            {
                file.Delete();
            }

            workbook.SaveToFile(Path.Combine(z.PathOut, filePdf), FileFormat.PDF);
            if (showFile) System.Diagnostics.Process.Start(Path.Combine(z.PathOut, filePdf));
        }

        public static void CreateListPDF(List<LibGenerateZaFoms.Models.ZaVkl> lz, bool showFile = true)
        {
            foreach (var item in lz)
            {
                CreatePDF(item, false);
            }
            if (showFile)
            {
                if (lz.Count == 1)
                {
                    string filePdf = lz[0].Famip + lz[0].Namep + lz[0].Otchp + DateTime.Parse(lz[0].DR).ToString("ddMMyyyy") + ".pdf";
                    System.Diagnostics.Process.Start(Path.Combine(lz[0].PathOut, filePdf));
                }
                if (lz.Count > 1)
                {
                    System.Diagnostics.Process.Start(lz[0].PathOut);
                }
            }
        }
    }
}
