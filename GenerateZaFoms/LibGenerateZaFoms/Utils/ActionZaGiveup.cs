using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace LibGenerateZaFoms.Utils
{
    public class ActionZaGiveup
    {
        static void SetDoc(ref Worksheet sh, LibGenerateZaFoms.Models.Doc doc)
        {
            if (doc == null) return;
            //Тип документа
            sh.Range["B23"].Text = doc.TypeDoc;
            //Серия
            sh.Range["D24"].Text = doc.SerDoc;
            //Номер
            sh.Range["J24"].Text = doc.NumDoc;
            //Дата выдачи
            sh.Range["S24"].Text = doc.DateDoc;
            //Кем выдан
            sh.Range["E26"].Text = doc.NpDoc;
        }

        static void SetAgent(ref Worksheet sh, LibGenerateZaFoms.Models.Agent agent)
        {
            if (agent == null) return;
            //Фамилия
            sh.Range["D36"].Text = agent.Famip;
            //Имя
            sh.Range["C38"].Text = agent.Namep;
            //Отчество
            sh.Range["G40"].Text = agent.Otchp;
            //Дата роджения
            sh.Range["E42"].Text = agent.DR;
            //Гражданство
            sh.Range["P42"].Text = agent.Land;

            //Документ
            //Тип документа
            sh.Range["B45"].Text = agent.doc.TypeDoc;
            //Серия
            sh.Range["D46"].Text = agent.doc.SerDoc;
            //Номер
            sh.Range["J46"].Text = agent.doc.NumDoc;
            //Дата выдачи
            sh.Range["S46"].Text = agent.doc.DateDoc;
            //Кем выдан
            sh.Range["E48"].Text = agent.doc.NpDoc;

            //Контакты
            sh.Range["F50"].Text = agent.PhoneMob;
            sh.Range["P50"].Text = agent.Email;
        }

        public static void CreatePDF(LibGenerateZaFoms.Models.ZaGiveup z, bool showFile = true)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(z.PathTemplate);
            Worksheet sheet = workbook.Worksheets[0];

            //Шапка заявления
            sheet.Range["K1"].Text = z.SmoName;
            sheet.Range["K3"].Text = (z.Famip + " " + z.Namep + " " + z.Otchp).Replace("  ", " ").Trim();

            //Причина сдачи
            Spire.Xls.Core.ICheckBox checkboxGiveup = sheet.CheckBoxes.AddCheckBox(8, 1, 20, 20);
            checkboxGiveup.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxUtrata = sheet.CheckBoxes.AddCheckBox(10, 1, 20, 20);
            checkboxUtrata.CheckState = Spire.Xls.CheckState.Unchecked;
            switch (z.prichina)
            {
                case Utils.PrichinaGiveup.Giveup:
                    checkboxGiveup.CheckState = Spire.Xls.CheckState.Checked;
                    break;
                case Utils.PrichinaGiveup.Utrata:
                    checkboxUtrata.CheckState = Spire.Xls.CheckState.Checked;
                    break;
            }
            //ЕНП
            sheet.Range["L9"].Text = z.ENP;

            //ФИО
            sheet.Range["D12"].Text = z.Famip;
            sheet.Range["C14"].Text = z.Namep;
            sheet.Range["G16"].Text = z.Otchp;

            //Пол
            Spire.Xls.Core.ICheckBox checkboxSexM = sheet.CheckBoxes.AddCheckBox(18, 4, 20, 20);
            checkboxSexM.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxSexF = sheet.CheckBoxes.AddCheckBox(18, 6, 20, 20);
            checkboxSexF.CheckState = Spire.Xls.CheckState.Unchecked;
            if (z.Sex.ToLower().Equals("м"))
            {
                checkboxSexM.CheckState = Spire.Xls.CheckState.Checked;
            }
            else if (z.Sex.ToLower().Equals("ж"))
            {
                checkboxSexF.CheckState = Spire.Xls.CheckState.Checked;
            }

            //Дата рождения
            sheet.Range["R18"].Text = z.DR;
            //Место рождения
            sheet.Range["F20"].Text = z.BirthPlac;
            //ДУЛ
            LibGenerateZaFoms.Utils.ActionZaGiveup.SetDoc(ref sheet, z.doc);
            //Гражданство
            sheet.Range["E27"].Text = z.Land;
            //СНИЛС
            sheet.Range["N29"].Text = z.Snils;

            //Контакты
            sheet.Range["F30"].Text = z.Phone;
            sheet.Range["P30"].Text = z.Email;

            //Адрес регистрации
            sheet.Range["B32"].Text = z.regAddress;
            //Адрес пребывания
            sheet.Range["B34"].Text = z.factAddress;

            //Представитель
            if (z.agent != null) LibGenerateZaFoms.Utils.ActionZaGiveup.SetAgent(ref sheet, z.agent);

            //Реквизиты доверенности
            sheet.Range["H49"].Text = z.NumDover;
            sheet.Range["Q49"].Text = z.DateDover;

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
            sheet.Range["H52"].Text = full_name;

            //Дата заявления
            sheet.Range["Q52"].Text = z.DZ;
            //Менеджер
            sheet.Range["N54"].Text = z.Manager;

            sheet.PageSetup.TopMargin = 0.10;
            sheet.PageSetup.BottomMargin = 0.10;
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

        public static void CreateListPDF(List<LibGenerateZaFoms.Models.ZaGiveup> lz, bool showFile = true, bool merge = true)
        {
            List<string> pdfFilePaths = new List<string>();
            foreach (var item in lz)
            {
                CreatePDF(item, false);
                pdfFilePaths.Add(Path.Combine(item.PathOut, item.PathFile));
            }
            if (merge)
            {
                string filename = Path.Combine(lz[0].PathOut, "Zgiveup" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf");
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
                        for (var i = 0; i < pdfDoc.PageCount; i++)
                            targetDoc.AddPage(pdfDoc.Pages[i]);
                    }
                }
                targetDoc.Save(targetPath);
            }
        }
    }
}
