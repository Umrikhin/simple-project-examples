using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Spire.Xls;
using System.IO;

namespace LibGenerateZaFoms.Utils
{
    public class ActionZaPolis
    {
        static void SetDoc(ref Worksheet sh, LibGenerateZaFoms.Models.Doc doc)
        {
            if (doc == null) return;
            //Тип документа
            sh.Range["I33"].Text = doc.TypeDoc;
            //Серия
            sh.Range["D34"].Text = doc.SerDoc;
            //Номер
            sh.Range["I34"].Text = doc.NumDoc;
            //Дата выдачи
            sh.Range["R34"].Text = doc.DateDoc;
            //Кем выдан
            sh.Range["D35"].Text = doc.NpDoc;
        }

        static void SetDoc2(ref Worksheet sh, LibGenerateZaFoms.Models.Doc doc)
        {
            if (doc == null) return;
            //Тип документа
            sh.Range["E55"].Text = doc.TypeDoc;
            //Серия
            sh.Range["D56"].Text = doc.SerDoc;
            //Номер
            sh.Range["M56"].Text = doc.NumDoc;
            //Кем и когда выдан
            sh.Range["F57"].Text = doc.NpDoc + ", " + doc.DateDoc;
        }

        static void SetRegAddress(ref Worksheet sh, LibGenerateZaFoms.Models.RegAddress address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(39, 5, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P39"].Text = address.Subj;
            //Район
            sh.Range["D41"].Text = address.Rayon;
            //Город
            sh.Range["P41"].Text = address.Town;
            //Населенный пункт
            sh.Range["F42"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P42"].Text = address.Street;
            //Дом
            sh.Range["F44"].Text = address.House;
            //Корпус
            sh.Range["N44"].Text = address.Korp;
            //Квартира
            sh.Range["T44"].Text = address.Kv;
            //Дата регистрации
            sh.Range["I45"].Text = address.DateReg;
        }

        static void SetFactAddress(ref Worksheet sh, LibGenerateZaFoms.Models.Address address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(48, 5, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P48"].Text = address.Subj;
            //Район
            sh.Range["D50"].Text = address.Rayon;
            //Город
            sh.Range["P50"].Text = address.Town;
            //Населенный пункт
            sh.Range["F51"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P51"].Text = address.Street;
            //Дом
            sh.Range["F53"].Text = address.House;
            //Корпус
            sh.Range["N53"].Text = address.Korp;
            //Квартира
            sh.Range["T53"].Text = address.Kv;
        }

        static void SetNotif(ref Worksheet sh, LibGenerateZaFoms.Models.Notif notif)
        {
            if (notif == null) return;
            //Флажки для способов оповещения
            Spire.Xls.Core.ICheckBox checkboxSms = sh.CheckBoxes.AddCheckBox(80, 1, 20, 20);
            checkboxSms.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxMail = sh.CheckBoxes.AddCheckBox(80, 14, 20, 20);
            checkboxMail.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxEmail = sh.CheckBoxes.AddCheckBox(81, 1, 20, 20);
            checkboxEmail.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxCall = sh.CheckBoxes.AddCheckBox(81, 14, 20, 20);
            checkboxCall.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxMsg = sh.CheckBoxes.AddCheckBox(82, 1, 20, 20);
            checkboxMsg.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxOther = sh.CheckBoxes.AddCheckBox(82, 14, 20, 20);
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
                sh.Range["O83"].Text = notif.DespriptionOther;
            }
        }

        static void SetAgent(ref Worksheet sh, LibGenerateZaFoms.Models.Agent agent)
        {
            if (agent == null) return;
            //Фамилия
            sh.Range["D92"].Text = agent.Famip;
            //Имя
            sh.Range["L92"].Text = agent.Namep;
            //Отчество
            sh.Range["F94"].Text = agent.Otchp;
            //Пол
            Spire.Xls.Core.ICheckBox checkboxSexAM = sh.CheckBoxes.AddCheckBox(96, 4, 20, 20);
            checkboxSexAM.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxSexAF = sh.CheckBoxes.AddCheckBox(96, 6, 20, 20);
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
            sh.Range["N96"].Text = agent.DR;
            //Гражданство
            sh.Range["E98"].Text = agent.Land;
            //Статус представителя
            Spire.Xls.Core.ICheckBox checkboxMother = sh.CheckBoxes.AddCheckBox(100, 13, 20, 20);
            checkboxMother.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxFather = sh.CheckBoxes.AddCheckBox(101, 13, 20, 20);
            checkboxFather.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxOpekun = sh.CheckBoxes.AddCheckBox(100, 17, 20, 20);
            checkboxOpekun.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxPopechitel = sh.CheckBoxes.AddCheckBox(101, 17, 20, 20);
            checkboxPopechitel.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxUsinovitel = sh.CheckBoxes.AddCheckBox(100, 21, 20, 20);
            checkboxUsinovitel.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxByProxy = sh.CheckBoxes.AddCheckBox(101, 21, 20, 20);
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
            sh.Range["S102"].Text = agent.doc.TypeDoc;
            //Серия
            sh.Range["D103"].Text = agent.doc.SerDoc;
            //Номер
            sh.Range["I103"].Text = agent.doc.NumDoc;
            //Дата выдачи
            sh.Range["R103"].Text = agent.doc.DateDoc;
            //Кем выдан
            sh.Range["D104"].Text = agent.doc.NpDoc;

            //Документ статуса
            //Серия
            sh.Range["D106"].Text = agent.docStatusSer;
            //Номер
            sh.Range["I106"].Text = agent.docStatusNum;
            //Дата выдачи
            sh.Range["Q106"].Text = agent.docStatusDbeg;

            //СНИЛС
            sh.Range["O107"].Text = agent.Snils;

            //ЕНП
            sh.Range["M108"].Text = agent.ENP;

            //Адрес регистрации
            SetRegAgentAddress(ref sh, agent.regAddress);
            //Бомж
            Spire.Xls.Core.ICheckBox checkboxABomg = sh.CheckBoxes.AddCheckBox(117, 2, 20, 20);
            checkboxABomg.CheckState = Spire.Xls.CheckState.Unchecked;
            if (agent.Bomg > 0) checkboxABomg.CheckState = Spire.Xls.CheckState.Checked;
            //Адрес пребывания
            SetFactAgentAddress(ref sh, agent.factAddress);

            //Контакты
            sh.Range["G125"].Text = agent.PhoneMob;
            sh.Range["M125"].Text = agent.PhoneHome;
            sh.Range["S125"].Text = agent.PhoneWork;
            sh.Range["G126"].Text = agent.Email;
        }

        static void SetRegAgentAddress(ref Worksheet sh, LibGenerateZaFoms.Models.RegAddress address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(110, 5, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P110"].Text = address.Subj;
            //Район
            sh.Range["D112"].Text = address.Rayon;
            //Город
            sh.Range["P112"].Text = address.Town;
            //Населенный пункт
            sh.Range["F113"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P113"].Text = address.Street;
            //Дом
            sh.Range["F115"].Text = address.House;
            //Корпус
            sh.Range["N115"].Text = address.Korp;
            //Квартира
            sh.Range["T115"].Text = address.Kv;
            //Дата регистрации
            sh.Range["I116"].Text = address.DateReg;
        }

        static void SetFactAgentAddress(ref Worksheet sh, LibGenerateZaFoms.Models.Address address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(119, 5, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P119"].Text = address.Subj;
            //Район
            sh.Range["D121"].Text = address.Rayon;
            //Город
            sh.Range["P121"].Text = address.Town;
            //Населенный пункт
            sh.Range["F122"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P122"].Text = address.Street;
            //Дом
            sh.Range["F124"].Text = address.House;
            //Корпус
            sh.Range["N124"].Text = address.Korp;
            //Квартира
            sh.Range["T124"].Text = address.Kv;
        }

        public static void CreatePDF(LibGenerateZaFoms.Models.ZaPolis z, bool showFile = true)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(z.PathTemplate);
            Worksheet sheet = workbook.Worksheets[0];

            //Шапка заявления
            sheet.Range["K1"].Text = z.SmoName;
            sheet.Range["K3"].Text = (z.Famip + " " + z.Namep + " " + z.Otchp).Replace("  ", " ").Trim();

            Spire.Xls.Core.ICheckBox checkboxVipiska = sheet.CheckBoxes.AddCheckBox(8, 4, 20, 20);
            checkboxVipiska.CheckState = Spire.Xls.CheckState.Checked;

            //Причина переоформления
            Spire.Xls.Core.ICheckBox checkboxIzmFam = sheet.CheckBoxes.AddCheckBox(10, 1, 20, 20);
            checkboxIzmFam.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxNeTochnost = sheet.CheckBoxes.AddCheckBox(11, 1, 20, 20);
            checkboxNeTochnost.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxOkonchanie = sheet.CheckBoxes.AddCheckBox(12, 1, 20, 20);
            checkboxOkonchanie.CheckState = Spire.Xls.CheckState.Unchecked;
            switch (z.prichina)
            {
                case Utils.PrichinaZameni.IzmFam:
                    checkboxIzmFam.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.PrichinaZameni.NeTochnost:
                    checkboxNeTochnost.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.PrichinaZameni.Okonchanie:
                    checkboxOkonchanie.CheckState = Spire.Xls.CheckState.Checked;
                    break;
            }

            //ФИО
            sheet.Range["D14"].Text = z.Famip;
            sheet.Range["L14"].Text = z.Namep;
            sheet.Range["F16"].Text = z.Otchp;

            //Флажки для категории ЗЛ
            Spire.Xls.Core.ICheckBox checkboxCat1 = sheet.CheckBoxes.AddCheckBox(19, 1, 20, 20);
            checkboxCat1.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat2 = sheet.CheckBoxes.AddCheckBox(20, 1, 20, 20);
            checkboxCat2.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat3 = sheet.CheckBoxes.AddCheckBox(21, 1, 20, 20);
            checkboxCat3.Top = checkboxCat3.Top + 20;
            checkboxCat3.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat4 = sheet.CheckBoxes.AddCheckBox(22, 1, 20, 20);
            checkboxCat4.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat5 = sheet.CheckBoxes.AddCheckBox(23, 1, 20, 20);
            checkboxCat5.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat6 = sheet.CheckBoxes.AddCheckBox(24, 1, 20, 20);
            checkboxCat6.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat7 = sheet.CheckBoxes.AddCheckBox(25, 1, 20, 20);
            checkboxCat7.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat8 = sheet.CheckBoxes.AddCheckBox(26, 1, 20, 20);
            checkboxCat8.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat9 = sheet.CheckBoxes.AddCheckBox(19, 13, 20, 20);
            checkboxCat9.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat10 = sheet.CheckBoxes.AddCheckBox(20, 13, 20, 20);
            checkboxCat10.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat11 = sheet.CheckBoxes.AddCheckBox(21, 13, 20, 20);
            checkboxCat11.Top = checkboxCat11.Top + 20;
            checkboxCat1.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat12 = sheet.CheckBoxes.AddCheckBox(22, 13, 20, 20);
            checkboxCat12.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat13 = sheet.CheckBoxes.AddCheckBox(23, 13, 20, 20);
            checkboxCat13.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat14 = sheet.CheckBoxes.AddCheckBox(24, 13, 20, 20);
            checkboxCat14.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat15 = sheet.CheckBoxes.AddCheckBox(25, 13, 20, 20);
            checkboxCat15.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat16 = sheet.CheckBoxes.AddCheckBox(26, 13, 20, 20);
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

            //Пол
            Spire.Xls.Core.ICheckBox checkboxSexM = sheet.CheckBoxes.AddCheckBox(29, 4, 20, 20);
            checkboxSexM.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxSexF = sheet.CheckBoxes.AddCheckBox(29, 6, 20, 20);
            checkboxSexF.CheckState = Spire.Xls.CheckState.Unchecked;
            if (z.Sex.ToLower().Equals("м"))
            {
                checkboxSexM.CheckState = Spire.Xls.CheckState.Checked;
            }
            else
            {
                checkboxSexF.CheckState = Spire.Xls.CheckState.Checked;
            }

            //Дата рождения
            sheet.Range["O29"].Text = z.DR;
            //Место рождения
            sheet.Range["E31"].Text = z.BirthPlac;
            //ДУЛ
            LibGenerateZaFoms.Utils.ActionZaPolis.SetDoc(ref sheet, z.doc);
            //Гражданство
            sheet.Range["E36"].Text = z.Land;
            //Адрес регистрации
            LibGenerateZaFoms.Utils.ActionZaPolis.SetRegAddress(ref sheet, z.regAddress);
            //Бомж
            Spire.Xls.Core.ICheckBox checkboxBomg = sheet.CheckBoxes.AddCheckBox(46, 2, 20, 20);
            checkboxBomg.CheckState = Spire.Xls.CheckState.Unchecked;
            if (z.Bomg > 0) checkboxBomg.CheckState = Spire.Xls.CheckState.Checked;
            //Адрес пребывания
            LibGenerateZaFoms.Utils.ActionZaPolis.SetFactAddress(ref sheet, z.factAddress);
            //Сведения о втором документе
            LibGenerateZaFoms.Utils.ActionZaPolis.SetDoc2(ref sheet, z.doc2);

            //Начало вида на жительство
            sheet.Range["E60"].Text = z.VidStart;
            //Окончание вида на жительство
            sheet.Range["M60"].Text = z.VidEnd;

            //Трудовой договор
            sheet.Range["C63"].Text = z.TrDogNum;
            sheet.Range["K63"].Text = z.TrDogSign;
            sheet.Range["P63"].Text = z.TrDogBeg;
            sheet.Range["T63"].Text = z.TrDogEnd;
            sheet.Range["H64"].Text = z.TrDogLocation;

            //ЕАЭС
            sheet.Range["D66"].Text = z.EaesSer;
            sheet.Range["M66"].Text = z.EaesNum;
            sheet.Range["B68"].Text = z.EaesCat;

            //Место пребывания
            sheet.Range["B73"].Text = z.PlaceOfStay;
            sheet.Range["P74"].Text = z.DbegOfStay;
            sheet.Range["T74"].Text = z.DendOfStay;

            //СНИЛС
            sheet.Range["O75"].Text = z.Snils;

            //Контакты
            sheet.Range["G77"].Text = z.PhoneMob;
            sheet.Range["M77"].Text = z.PhoneHome;
            sheet.Range["S77"].Text = z.PhoneWork;
            sheet.Range["G78"].Text = z.Email;

            //Оповещения
            LibGenerateZaFoms.Utils.ActionZaPolis.SetNotif(ref sheet, z.notif);

            //Прежние данные
            sheet.Range["D85"].Text = z.OldFamip;
            sheet.Range["L85"].Text = z.OldNamep;
            sheet.Range["F87"].Text = z.OldOtchp;
            Spire.Xls.Core.ICheckBox checkboxOldSexM = sheet.CheckBoxes.AddCheckBox(89, 4, 20, 20);
            checkboxOldSexM.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxOldSexF = sheet.CheckBoxes.AddCheckBox(89, 6, 20, 20);
            checkboxOldSexF.CheckState = Spire.Xls.CheckState.Unchecked;
            if (z.OldSex.ToLower().Equals("м"))
            {
                checkboxOldSexM.CheckState = Spire.Xls.CheckState.Checked;
            }
            else
            {
                checkboxOldSexF.CheckState = Spire.Xls.CheckState.Checked;
            }
            sheet.Range["N89"].Text = z.OldDR;

            //Представитель
            if (z.agent != null) LibGenerateZaFoms.Utils.ActionZaPolis.SetAgent(ref sheet, z.agent);

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
            sheet.Range["H128"].Text = full_name;
            sheet.Range["I134"].Text = full_name;
            sheet.Range["I137"].Text = full_name;

            //Дата заявления
            sheet.Range["Q128"].Text = z.DZ;
            //Менеджер
            sheet.Range["N130"].Text = z.Manager;

            //Согласие
            Spire.Xls.Core.ICheckBox checkboxSogl1 = sheet.CheckBoxes.AddCheckBox(133, 1, 20, 20);
            checkboxSogl1.Top = checkboxSogl1.Top + 20;
            checkboxSogl1.CheckState = Spire.Xls.CheckState.Checked;
            Spire.Xls.Core.ICheckBox checkboxSogl2 = sheet.CheckBoxes.AddCheckBox(136, 1, 20, 20);
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

        public static void CreateListPDF(List<LibGenerateZaFoms.Models.ZaPolis> lz, bool showFile = true)
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
