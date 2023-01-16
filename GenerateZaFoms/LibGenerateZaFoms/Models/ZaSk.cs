using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibGenerateZaFoms.Models
{
    public class ZaSk
    {
        public string SmoName { get; set; }
        public LibGenerateZaFoms.Utils.PrichinaVibor prichina { get; set; }
        public LibGenerateZaFoms.Utils.FormaPolis formPolis { get; set; }
        public string ENP { get; set; }
        public string OldSmo { get; set; }
        public string Famip { get; set; }
        public string Namep { get; set; }
        public string Otchp { get; set; }
        public string Sex { get; set; }
        public LibGenerateZaFoms.Utils.Cat cat { get; set; }
        public string DR { get; set; }
        public string BirthPlac { get; set; }
        public Doc doc { get; set; }
        public string Land { get; set; }
        public RegAddress regAddress { get; set; }
        public int Bomg { get; set; }
        public Address factAddress { get; set; }
        public Doc doc2 { get; set; }
        public string VidStart { get; set; }
        public string VidEnd { get; set; }
        public string TrDogNum { get; set; }
        public string TrDogSign { get; set; }
        public string TrDogBeg { get; set; }
        public string TrDogEnd { get; set; }
        public string TrDogLocation { get; set; }
        public string EaesSer { get; set; }
        public string EaesNum { get; set; }
        public string EaesCat { get; set; }
        public string PlaceOfStay { get; set; }
        public string DbegOfStay { get; set; }
        public string DendOfStay { get; set; }
        public string Snils { get; set; }
        public string PhoneMob { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneWork { get; set; }
        public string Email { get; set; }
        public Notif notif { get; set; }
        public Agent agent { get; set; }
        public string Manager { get; set; }
        public string DZ { get; set; }
        public string PathTemplate { get; set; }
        public string PathFile { get; set; }
        public string PathOut { get; set; }

        public ZaSk()
        {
            SmoName = string.Empty;
            prichina = Utils.PrichinaVibor.ViborSmo;
            formPolis = Utils.FormaPolis.Bumaga;
            ENP = string.Empty;
            OldSmo= string.Empty;
            Famip = string.Empty;
            Namep = string.Empty;
            Otchp = string.Empty;
            Sex = string.Empty;
            cat = Utils.Cat.cat1;
            DR = string.Empty;
            BirthPlac = string.Empty;
            doc = new Doc();
            Land = string.Empty;
            regAddress = new RegAddress();
            Bomg = 0;
            factAddress = new Address();
            doc2 = new Doc();
            VidStart = string.Empty;
            VidEnd = string.Empty;
            TrDogNum = string.Empty;
            TrDogSign = string.Empty;
            TrDogBeg = string.Empty;
            TrDogEnd = string.Empty;
            TrDogLocation = string.Empty;
            EaesSer = string.Empty;
            EaesNum = string.Empty;
            EaesCat = string.Empty;
            PlaceOfStay = string.Empty;
            DbegOfStay = string.Empty;
            DendOfStay = string.Empty;
            Snils = string.Empty;
            PhoneMob = string.Empty;
            PhoneHome = string.Empty;
            PhoneWork = string.Empty;
            Email = string.Empty;
            notif = new Notif();
            agent = new Agent();
            Manager = string.Empty;
            DZ = string.Empty;
            var path = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            PathTemplate = System.IO.Path.Combine(path.DirectoryName, "Template", "ZaSk.xlsx");
            PathFile = string.Empty;
            PathOut = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Заявления выбора");
        }
    }
}
