using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using LibGenerateZaFoms.Models;
using Spire.Xls.Core;
using PdfSharp.Charting;
using Spire.Xls.Core.Spreadsheet;

namespace LibGenerateZaFoms.Utils
{
    public class ActionZaSk
    {
        static void SetDoc(ref Worksheet sh, LibGenerateZaFoms.Models.Doc doc)
        {
            if (doc == null) return;
            //Тип документа
            sh.Range["H46"].Text = doc.TypeDoc;
            //Серия
            sh.Range["D47"].Text = doc.SerDoc;
            //Номер
            sh.Range["I47"].Text = doc.NumDoc;
            //Дата выдачи
            sh.Range["R47"].Text = doc.DateDoc;
            //Кем выдан
            sh.Range["D48"].Text = doc.NpDoc;
        }

        static void SetDoc2(ref Worksheet sh, LibGenerateZaFoms.Models.Doc doc)
        {
            if (doc == null) return;
            //Тип документа
            sh.Range["E69"].Text = doc.TypeDoc;
            //Серия
            sh.Range["D70"].Text = doc.SerDoc;
            //Номер
            sh.Range["M70"].Text = doc.NumDoc;
            //Кем и когда выдан
            sh.Range["F71"].Text = doc.NpDoc + ", " + doc.DateDoc;
        }

        static void SetRegAddress(ref Worksheet sh, LibGenerateZaFoms.Models.RegAddress address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(53, 6, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                if (address.Index.Length == 6) textboxIndex.RichText.SetFont(0, 1, sh.Range["P53"].Style.Font);
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P53"].Text = address.Subj;
            //Район
            sh.Range["D55"].Text = address.Rayon;
            //Город
            sh.Range["P55"].Text = address.Town;
            //Населенный пункт
            sh.Range["F56"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P56"].Text = address.Street;
            //Дом
            sh.Range["F58"].Text = address.House;
            //Корпус
            sh.Range["N58"].Text = address.Korp;
            //Квартира
            sh.Range["T58"].Text = address.Kv;
            //Дата регистрации
            sh.Range["I59"].Text = address.DateReg;
        }

        static void SetFactAddress(ref Worksheet sh, LibGenerateZaFoms.Models.Address address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(62, 6, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                if (address.Index.Length == 6) textboxIndex.RichText.SetFont(0, 1, sh.Range["P62"].Style.Font);
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P62"].Text = address.Subj;
            //Район
            sh.Range["D64"].Text = address.Rayon;
            //Город
            sh.Range["P64"].Text = address.Town;
            //Населенный пункт
            sh.Range["F65"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P65"].Text = address.Street;
            //Дом
            sh.Range["F67"].Text = address.House;
            //Корпус
            sh.Range["N67"].Text = address.Korp;
            //Квартира
            sh.Range["T67"].Text = address.Kv;
        }

        static void SetNotif(ref Worksheet sh, LibGenerateZaFoms.Models.Notif notif)
        {
            if (notif == null) return;
            //Флажки для способов оповещения
            Spire.Xls.Core.ICheckBox checkboxSms = sh.CheckBoxes.AddCheckBox(91, 1, 20, 20);
            checkboxSms.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxMail = sh.CheckBoxes.AddCheckBox(91, 14, 20, 20);
            checkboxMail.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxEmail = sh.CheckBoxes.AddCheckBox(92, 1, 20, 20);
            checkboxEmail.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxCall = sh.CheckBoxes.AddCheckBox(92, 14, 20, 20);
            checkboxCall.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxMsg = sh.CheckBoxes.AddCheckBox(93, 1, 20, 20);
            checkboxMsg.CheckState = Spire.Xls.CheckState.Unchecked;
            checkboxMsg.Top = checkboxMsg.Top + 10;
            Spire.Xls.Core.ICheckBox checkboxOther = sh.CheckBoxes.AddCheckBox(93, 14, 20, 20);
            checkboxOther.CheckState = Spire.Xls.CheckState.Unchecked;
            checkboxOther.Top = checkboxOther.Top + 10;
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
                sh.Range["O94"].Text = notif.DespriptionOther;
            }
        }

        static void SetAgent(ref Worksheet sh, LibGenerateZaFoms.Models.Agent agent)
        {
            if (agent == null) return;
            //Фамилия
            sh.Range["D96"].Text = agent.Famip;
            //Имя
            sh.Range["L96"].Text = agent.Namep;
            //Отчество
            sh.Range["F98"].Text = agent.Otchp;
            //Пол
            Spire.Xls.Core.ICheckBox checkboxSexAM = sh.CheckBoxes.AddCheckBox(100, 4, 20, 20);
            checkboxSexAM.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxSexAF = sh.CheckBoxes.AddCheckBox(100, 6, 20, 20);
            checkboxSexAF.CheckState = Spire.Xls.CheckState.Unchecked;
            if (agent.Sex.ToLower().Equals("м"))
            {
                checkboxSexAM.CheckState = Spire.Xls.CheckState.Checked;
            }
            else if (agent.Sex.ToLower().Equals("ж"))
            {
                checkboxSexAF.CheckState = Spire.Xls.CheckState.Checked;
            }
            //Дата роджения
            sh.Range["N100"].Text = agent.DR;
            //Гражданство
            sh.Range["E102"].Text = agent.Land;
            //Статус представителя
            Spire.Xls.Core.ICheckBox checkboxMother = sh.CheckBoxes.AddCheckBox(104, 13, 20, 20);
            checkboxMother.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxFather = sh.CheckBoxes.AddCheckBox(105, 13, 20, 20);
            checkboxFather.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxOpekun = sh.CheckBoxes.AddCheckBox(104, 17, 20, 20);
            checkboxOpekun.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxPopechitel = sh.CheckBoxes.AddCheckBox(105, 17, 20, 20);
            checkboxPopechitel.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxUsinovitel = sh.CheckBoxes.AddCheckBox(104, 21, 20, 20);
            checkboxUsinovitel.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxByProxy = sh.CheckBoxes.AddCheckBox(105, 21, 20, 20);
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
            sh.Range["S106"].Text = agent.doc.TypeDoc;
            //Серия
            sh.Range["D108"].Text = agent.doc.SerDoc;
            //Номер
            sh.Range["I108"].Text = agent.doc.NumDoc;
            //Дата выдачи
            sh.Range["R108"].Text = agent.doc.DateDoc;
            //Кем выдан
            sh.Range["D109"].Text = agent.doc.NpDoc;

            //Документ статуса
            //Серия
            sh.Range["D111"].Text = agent.docStatusSer;
            //Номер
            sh.Range["I111"].Text = agent.docStatusNum;
            //Дата выдачи
            sh.Range["Q111"].Text = agent.docStatusDbeg;

            //СНИЛС
            sh.Range["O112"].Text = agent.Snils;

            //ЕНП
            sh.Range["M113"].Text = agent.ENP;

            //Адрес регистрации
            SetRegAgentAddress(ref sh, agent.regAddress);
            //Бомж
            Spire.Xls.Core.ICheckBox checkboxABomg = sh.CheckBoxes.AddCheckBox(122, 2, 20, 20);
            checkboxABomg.CheckState = Spire.Xls.CheckState.Unchecked;
            if (agent.Bomg > 0) checkboxABomg.CheckState = Spire.Xls.CheckState.Checked;
            //Адрес пребывания
            SetFactAgentAddress(ref sh, agent.factAddress);

            //Контакты
            sh.Range["G130"].Text = agent.PhoneMob;
            sh.Range["M130"].Text = agent.PhoneHome;
            sh.Range["S130"].Text = agent.PhoneWork;
            sh.Range["G131"].Text = agent.Email;
        }

        static void SetRegAgentAddress(ref Worksheet sh, LibGenerateZaFoms.Models.RegAddress address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(115, 6, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                if (address.Index.Length == 6) textboxIndex.RichText.SetFont(0, 1, sh.Range["P115"].Style.Font);
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P115"].Text = address.Subj;
            //Район
            sh.Range["D117"].Text = address.Rayon;
            //Город
            sh.Range["P117"].Text = address.Town;
            //Населенный пункт
            sh.Range["F118"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P118"].Text = address.Street;
            //Дом
            sh.Range["F120"].Text = address.House;
            //Корпус
            sh.Range["N120"].Text = address.Korp;
            //Квартира
            sh.Range["T120"].Text = address.Kv;
            //Дата регистрации
            sh.Range["I121"].Text = address.DateReg;
        }

        static void SetFactAgentAddress(ref Worksheet sh, LibGenerateZaFoms.Models.Address address)
        {
            if (address == null) return;
            //Индекс
            for (int i = 0; i < 6; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxIndex = sh.TextBoxes.AddTextBox(124, 6, 20, 20);
                textboxIndex.Left = textboxIndex.Left + i * 20;
                if (address.Index.Length == 6) textboxIndex.Text = address.Index[i].ToString();
                if (address.Index.Length == 6) textboxIndex.RichText.SetFont(0, 1, sh.Range["P124"].Style.Font);
                textboxIndex.TextFrame.IsAutoMargin = true;
                textboxIndex.HAlignment = CommentHAlignType.Center;
                textboxIndex.VAlignment = CommentVAlignType.Center;
            }
            //Субъект РФ
            sh.Range["P124"].Text = address.Subj;
            //Район
            sh.Range["D126"].Text = address.Rayon;
            //Город
            sh.Range["P126"].Text = address.Town;
            //Населенный пункт
            sh.Range["F127"].Text = address.LocalityOrCity;
            //Улица
            sh.Range["P127"].Text = address.Street;
            //Дом
            sh.Range["F129"].Text = address.House;
            //Корпус
            sh.Range["N129"].Text = address.Korp;
            //Квартира
            sh.Range["T129"].Text = address.Kv;
        }

        public static void CreatePDF(LibGenerateZaFoms.Models.ZaSk z, bool showFile = true)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(z.PathTemplate);
            Worksheet sheet = workbook.Worksheets[0];

            //Шапка заявления
            sheet.Range["K1"].Text = z.SmoName;
            sheet.Range["K3"].Text = (z.Famip + " " + z.Namep + " " + z.Otchp).Replace("  ", " ").Trim();

            sheet.Range["D8"].Text = z.SmoName;

            //Причина выбора
            Spire.Xls.Core.ICheckBox checkboxViborSmo = sheet.CheckBoxes.AddCheckBox(11, 1, 20, 20);
            checkboxViborSmo.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxSmoOneYear = sheet.CheckBoxes.AddCheckBox(12, 1, 20, 20);
            checkboxSmoOneYear.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxSmoPlace = sheet.CheckBoxes.AddCheckBox(13, 1, 20, 20);
            checkboxSmoPlace.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxSmoEndDog = sheet.CheckBoxes.AddCheckBox(14, 1, 20, 20);
            checkboxSmoEndDog.CheckState = Spire.Xls.CheckState.Unchecked;
            switch (z.prichina)
            {
                case Utils.PrichinaVibor.ViborSmo:
                    checkboxViborSmo.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.PrichinaVibor.ZamenaSmoOneYear:
                    checkboxSmoOneYear.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.PrichinaVibor.ZamenaSmoPlace:
                    checkboxSmoPlace.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.PrichinaVibor.ZamenaSmoEndDog:
                    checkboxSmoEndDog.CheckState = Spire.Xls.CheckState.Checked;
                    break;
            }

            //Форма полиса
            Spire.Xls.Core.ICheckBox checkboxBumaga = sheet.CheckBoxes.AddCheckBox(16, 1, 20, 20);
            checkboxBumaga.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxCancelGetPolis = sheet.CheckBoxes.AddCheckBox(17, 1, 20, 20);
            checkboxCancelGetPolis.CheckState = Spire.Xls.CheckState.Unchecked;
            switch (z.formPolis)
            {
                case Utils.FormaPolis.Bumaga:
                    checkboxBumaga.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.FormaPolis.CancelGetPolis:
                    checkboxCancelGetPolis.CheckState = Spire.Xls.CheckState.Checked;
                    break;
            }
            //ЕНП
            for (int i = 0; i < 16; i++)
            {
                Spire.Xls.Core.ITextBoxShape textboxEnp = sheet.TextBoxes.AddTextBox(18, 5, 20, 20);
                textboxEnp.Left = textboxEnp.Left + i * 20;
                if (z.ENP.Length == 16) textboxEnp.Text = z.ENP[i].ToString();
                textboxEnp.RichText.SetFont(0, 1, sheet.Range["B19"].Style.Font);
                textboxEnp.TextFrame.IsAutoMargin = true;
                textboxEnp.HAlignment = CommentHAlignType.Center;
                textboxEnp.VAlignment = CommentVAlignType.Center;
            }
            Spire.Xls.Core.ICheckBox checkboxNotEnp = sheet.CheckBoxes.AddCheckBox(18, 23, 20, 20);
            checkboxNotEnp.CheckState = Spire.Xls.CheckState.Unchecked;
            if (z.ENP.Length == 0) checkboxNotEnp.CheckState = Spire.Xls.CheckState.Checked;

            //Старая СМО
            sheet.Range["B19"].Text = z.OldSmo;

            //ФИО
            sheet.Range["D28"].Text = z.Famip;
            sheet.Range["L28"].Text = z.Namep;
            sheet.Range["F30"].Text = z.Otchp;

            //Пол
            Spire.Xls.Core.ICheckBox checkboxSexM = sheet.CheckBoxes.AddCheckBox(30, 17, 20, 20);
            checkboxSexM.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxSexF = sheet.CheckBoxes.AddCheckBox(30, 19, 20, 20);
            checkboxSexF.CheckState = Spire.Xls.CheckState.Unchecked;
            if (z.Sex.ToLower().Equals("м"))
            {
                checkboxSexM.CheckState = Spire.Xls.CheckState.Checked;
            }
            else if (z.Sex.ToLower().Equals("ж"))
            {
                checkboxSexF.CheckState = Spire.Xls.CheckState.Checked;
            }

            //Флажки для категории ЗЛ
            Spire.Xls.Core.ICheckBox checkboxCat1 = sheet.CheckBoxes.AddCheckBox(33, 1, 20, 20);
            checkboxCat1.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat2 = sheet.CheckBoxes.AddCheckBox(34, 1, 20, 20);
            checkboxCat2.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat3 = sheet.CheckBoxes.AddCheckBox(35, 1, 20, 20);
            checkboxCat3.Top = checkboxCat3.Top + 20;
            checkboxCat3.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat4 = sheet.CheckBoxes.AddCheckBox(36, 1, 20, 20);
            checkboxCat4.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat5 = sheet.CheckBoxes.AddCheckBox(37, 1, 20, 20);
            checkboxCat5.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat6 = sheet.CheckBoxes.AddCheckBox(38, 1, 20, 20);
            checkboxCat6.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat7 = sheet.CheckBoxes.AddCheckBox(39, 1, 20, 20);
            checkboxCat7.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat8 = sheet.CheckBoxes.AddCheckBox(40, 1, 20, 20);
            checkboxCat8.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat9 = sheet.CheckBoxes.AddCheckBox(33, 13, 20, 20);
            checkboxCat9.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat10 = sheet.CheckBoxes.AddCheckBox(34, 13, 20, 20);
            checkboxCat10.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat11 = sheet.CheckBoxes.AddCheckBox(35, 13, 20, 20);
            checkboxCat11.Top = checkboxCat11.Top + 20;
            checkboxCat1.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat12 = sheet.CheckBoxes.AddCheckBox(36, 13, 20, 20);
            checkboxCat12.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat13 = sheet.CheckBoxes.AddCheckBox(37, 13, 20, 20);
            checkboxCat13.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat14 = sheet.CheckBoxes.AddCheckBox(38, 13, 20, 20);
            checkboxCat14.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat15 = sheet.CheckBoxes.AddCheckBox(39, 13, 20, 20);
            checkboxCat15.CheckState = Spire.Xls.CheckState.Unchecked;

            Spire.Xls.Core.ICheckBox checkboxCat16 = sheet.CheckBoxes.AddCheckBox(40, 13, 20, 20);
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
            sheet.Range["E43"].Text = z.DR;
            //Место рождения
            sheet.Range["L43"].Text = z.BirthPlac;
            //ДУЛ
            LibGenerateZaFoms.Utils.ActionZaSk.SetDoc(ref sheet, z.doc);
            //Гражданство
            sheet.Range["E49"].Text = z.Land;
            //Адрес регистрации
            LibGenerateZaFoms.Utils.ActionZaSk.SetRegAddress(ref sheet, z.regAddress);
            //Бомж
            Spire.Xls.Core.ICheckBox checkboxBomg = sheet.CheckBoxes.AddCheckBox(60, 2, 20, 20);
            checkboxBomg.CheckState = Spire.Xls.CheckState.Unchecked;
            if (z.Bomg > 0) checkboxBomg.CheckState = Spire.Xls.CheckState.Checked;
            //Адрес пребывания
            LibGenerateZaFoms.Utils.ActionZaSk.SetFactAddress(ref sheet, z.factAddress);
            //Сведения о втором документе
            LibGenerateZaFoms.Utils.ActionZaSk.SetDoc2(ref sheet, z.doc2);

            //Начало вида на жительство
            sheet.Range["E74"].Text = z.VidStart;
            //Окончание вида на жительство
            sheet.Range["M74"].Text = z.VidEnd;

            //Трудовой договор
            sheet.Range["C77"].Text = z.TrDogNum;
            sheet.Range["K77"].Text = z.TrDogSign;
            sheet.Range["P77"].Text = z.TrDogBeg;
            sheet.Range["T77"].Text = z.TrDogEnd;
            sheet.Range["H78"].Text = z.TrDogLocation;

            //ЕАЭС
            sheet.Range["D80"].Text = z.EaesSer;
            sheet.Range["M80"].Text = z.EaesNum;
            sheet.Range["B82"].Text = z.EaesCat;

            //Место пребывания
            sheet.Range["B84"].Text = z.PlaceOfStay;
            sheet.Range["P85"].Text = z.DbegOfStay;
            sheet.Range["T85"].Text = z.DendOfStay;

            //СНИЛС
            sheet.Range["O86"].Text = z.Snils;

            //Контакты
            sheet.Range["G88"].Text = z.PhoneMob;
            sheet.Range["M88"].Text = z.PhoneHome;
            sheet.Range["S88"].Text = z.PhoneWork;
            sheet.Range["G89"].Text = z.Email;

            //Оповещения
            LibGenerateZaFoms.Utils.ActionZaSk.SetNotif(ref sheet, z.notif);

            //Представитель
            if (z.agent != null) LibGenerateZaFoms.Utils.ActionZaSk.SetAgent(ref sheet, z.agent);

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
            sheet.Range["H134"].Text = full_name;
            sheet.Range["I140"].Text = full_name;
            sheet.Range["I143"].Text = full_name;

            //Дата заявления
            sheet.Range["Q134"].Text = z.DZ;
            //Менеджер
            sheet.Range["N136"].Text = z.Manager;

            //Согласие
            Spire.Xls.Core.ICheckBox checkboxSogl1 = sheet.CheckBoxes.AddCheckBox(139, 1, 20, 20);
            checkboxSogl1.Top = checkboxSogl1.Top + 30;
            checkboxSogl1.CheckState = Spire.Xls.CheckState.Checked;
            Spire.Xls.Core.ICheckBox checkboxSogl2 = sheet.CheckBoxes.AddCheckBox(142, 1, 20, 20);
            checkboxSogl2.CheckState = Spire.Xls.CheckState.Checked;
            checkboxSogl2.Top = checkboxSogl2.Top + 30;

            sheet.PageSetup.TopMargin = 0.30;
            sheet.PageSetup.BottomMargin = 0.30;
            sheet.PageSetup.LeftMargin = 0.45;

            DateTime dR = DateTime.MinValue;
            DateTime.TryParse(z.DR, out dR);
            string filePdf = z.Famip + z.Namep + z.Otchp + dR.ToString("ddMMyyyy") + ".pdf";
            z.PathFile = filePdf;

            if (!Directory.Exists(z.PathOut)) Directory.CreateDirectory(z.PathOut);

            //Очистка от старых файлов
            foreach (var file in new DirectoryInfo(z.PathOut).GetFiles().Where(x => x.LastWriteTime < DateTime.Now.Date))
            {
                file.Delete();
            }

            workbook.SaveToFile(Path.Combine(z.PathOut, filePdf), FileFormat.PDF);
            if (showFile) System.Diagnostics.Process.Start(Path.Combine(z.PathOut, filePdf));
        }

        public static void CreateListPDF(List<LibGenerateZaFoms.Models.ZaSk> lz, bool showFile = true, bool merge = true)
        {
            List<string> pdfFilePaths = new List<string>();
            foreach (var item in lz)
            {
                CreatePDF(item, false);
                pdfFilePaths.Add(Path.Combine(item.PathOut, item.PathFile));
            }
            if (merge)
            {
                string filename = Path.Combine(lz[0].PathOut, "Zsk" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf");
                MergePDFs(filename, pdfFilePaths.ToArray());
                foreach (var item in pdfFilePaths)
                {
                    File.Delete(item);
                }
                if (showFile) System.Diagnostics.Process.Start(filename);
                return;
            }
            if (showFile)
            {
                if (lz.Count == 1)
                {
                    System.Diagnostics.Process.Start(Path.Combine(lz[0].PathOut, lz[0].PathFile));
                }
                if (lz.Count > 1)
                {
                    System.Diagnostics.Process.Start(lz[0].PathOut);
                }
            }
        }

        public static void MergePDFs(string targetPath, params string[] pdfs)
        {
            using (var targetDoc = new PdfDocument())
            {
                foreach (var pdf in pdfs)
                {
                    using (var pdfDoc = PdfReader.Open(pdf, PdfDocumentOpenMode.Import))
                    {
                        for (var i = 0; i <= pdfDoc.PageCount; i++)
                        {
                            if (i == pdfDoc.PageCount)
                            {
                                targetDoc.AddPage();
                            }
                            else
                            {
                                targetDoc.AddPage(pdfDoc.Pages[i]);
                            }
                        }
                    }
                }
                targetDoc.Save(targetPath);
            }
        }
    }
}
