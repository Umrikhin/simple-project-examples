using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibGenerateZaFoms.Models
{
    public class ZaGiveup
    {
        public int id { get; set; } = 0;
        public string SmoName { get; set; }
        public LibGenerateZaFoms.Utils.PrichinaGiveup prichina { get; set; }
        public string ENP { get; set; }
        public string Famip { get; set; }
        public string Namep { get; set; }
        public string Otchp { get; set; }
        public string Sex { get; set; }
        public string DR { get; set; }
        public string BirthPlac { get; set; }
        public Doc doc { get; set; }
        public string Land { get; set; }
        public string Snils { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string regAddress { get; set; }
        public string factAddress { get; set; }
        public Agent agent { get; set; }
        public string NumDover { get; set; }
        public string DateDover { get; set; }
        public string Manager { get; set; }
        public string DZ { get; set; }
        public string PathTemplate { get; set; }
        public string PathFile { get; set; }
        public string PathOut { get; set; }

        public ZaGiveup()
        {
            SmoName = string.Empty;
            prichina = Utils.PrichinaGiveup.Giveup;
            ENP = string.Empty;
            Famip = string.Empty;
            Namep = string.Empty;
            Otchp = string.Empty;
            Sex = string.Empty;
            DR = string.Empty;
            BirthPlac = string.Empty;
            doc = new Doc();
            Land = string.Empty;
            Snils = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            regAddress = string.Empty;
            factAddress = string.Empty;
            agent = new Agent();
            NumDover = string.Empty;
            DateDover = string.Empty;
            Manager = string.Empty;
            DZ = string.Empty;
            var path = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            PathTemplate = System.IO.Path.Combine(path.DirectoryName, "Template", "ZaGiveup.xlsx");
            PathFile = string.Empty;
            PathOut = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Заявления на сдачу");
        }
    }
}
