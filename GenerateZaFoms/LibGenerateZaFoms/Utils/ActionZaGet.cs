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
    public class ActionZaGet
    {
        static void SetDoc(ref Worksheet sh, LibGenerateZaFoms.Models.Doc doc)
        {
            if (doc == null) return;
            //Тип документа
            sh.Range["B20"].Text = doc.TypeDoc;
            //Серия
            sh.Range["D21"].Text = doc.SerDoc;
            //Номер
            sh.Range["J21"].Text = doc.NumDoc;
            //Дата выдачи
            sh.Range["S21"].Text = doc.DateDoc;
            //Кем выдан
            sh.Range["E23"].Text = doc.NpDoc;
        }

        static void SetAgent(ref Worksheet sh, LibGenerateZaFoms.Models.Agent agent)
        {
            if (agent == null) return;
            //Фамилия
            sh.Range["D33"].Text = agent.Famip;
            //Имя
            sh.Range["C35"].Text = agent.Namep;
            //Отчество
            sh.Range["G37"].Text = agent.Otchp;
            //Дата роджения
            sh.Range["E39"].Text = agent.DR;
            //Гражданство
            sh.Range["P39"].Text = agent.Land;

            //Документ
            //Тип документа
            sh.Range["B42"].Text = agent.doc.TypeDoc;
            //Серия
            sh.Range["D43"].Text = agent.doc.SerDoc;
            //Номер
            sh.Range["J43"].Text = agent.doc.NumDoc;
            //Дата выдачи
            sh.Range["S43"].Text = agent.doc.DateDoc;
            //Кем выдан
            sh.Range["E45"].Text = agent.doc.NpDoc;

            //Контакты
            sh.Range["F47"].Text = agent.PhoneMob;
            sh.Range["P47"].Text = agent.Email;
        }

        public static void CreatePDF(LibGenerateZaFoms.Models.ZaGet z, bool showFile = true)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(z.PathTemplate);
            Worksheet sheet = workbook.Worksheets[0];

            //Шапка заявления
            sheet.Range["K1"].Text = z.SmoName;
            sheet.Range["K3"].Text = (z.Famip + " " + z.Namep + " " + z.Otchp).Replace("  ", " ").Trim();
            if (z.agent.Famip.Length > 0) sheet.Range["K3"].Text = (z.agent.Famip + " " + z.agent.Namep + " " + z.agent.Otchp).Replace("  ", " ").Trim();

            //ФИО
            sheet.Range["D9"].Text = z.Famip;
            sheet.Range["C11"].Text = z.Namep;
            sheet.Range["G13"].Text = z.Otchp;

            //Пол
            Spire.Xls.Core.ICheckBox checkboxSexM = sheet.CheckBoxes.AddCheckBox(15, 4, 20, 20);
            checkboxSexM.CheckState = Spire.Xls.CheckState.Unchecked;
            Spire.Xls.Core.ICheckBox checkboxSexF = sheet.CheckBoxes.AddCheckBox(15, 6, 20, 20);
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
            sheet.Range["R15"].Text = z.DR;
            //Место рождения
            sheet.Range["F17"].Text = z.BirthPlac;
            //ДУЛ
            LibGenerateZaFoms.Utils.ActionZaGet.SetDoc(ref sheet, z.doc);
            //Гражданство
            sheet.Range["E24"].Text = z.Land;
            //СНИЛС
            sheet.Range["N26"].Text = z.Snils;

            //Контакты
            sheet.Range["F27"].Text = z.Phone;
            sheet.Range["P27"].Text = z.Email;

            //Адрес регистрации
            sheet.Range["B29"].Text = z.regAddress;
            //Адрес пребывания
            sheet.Range["B31"].Text = z.factAddress;

            //Представитель
            if (z.agent != null) LibGenerateZaFoms.Utils.ActionZaGet.SetAgent(ref sheet, z.agent);

            //Реквизиты доверенности
            sheet.Range["H46"].Text = z.NumDover;
            sheet.Range["Q46"].Text = z.DateDover;

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
            sheet.Range["H49"].Text = full_name;

            //Дата заявления
            sheet.Range["Q49"].Text = z.DZ;
            //Менеджер
            sheet.Range["N51"].Text = z.Manager;

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

        public static void CreateListPDF(List<LibGenerateZaFoms.Models.ZaGet> lz, bool showFile = true, bool merge = true)
        {
            List<string> pdfFilePaths = new List<string>();
            foreach (var item in lz)
            {
                CreatePDF(item, false);
                pdfFilePaths.Add(Path.Combine(item.PathOut, item.PathFile));
            }
            if (merge)
            {
                string filename = Path.Combine(lz[0].PathOut, "Zget" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf");
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
                        for (var i = 0; i < Math.Min(pdfDoc.PageCount, 1); i++)
                            targetDoc.AddPage(pdfDoc.Pages[i]);
                    }
                }
                targetDoc.Save(targetPath);
            }
        }
    }
}
