using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LibGenerateZaFoms.Models;
using LibGenerateZaFoms.Utils;

namespace GenerateZaFoms
{
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZaVkl z = new ZaVkl();
            z.SmoName = "OOO МСО Панацея";
            z.Famip = "Василин";
            z.Namep = "Владимир";
            z.Otchp = "Константинович";
            z.Sex = "Ж";
            z.cat = Cat.cat11;
            z.DR = "15.06.1975";
            z.BirthPlac = "Поселок городского типа солнечнодольск изобильненского района ставропольского края российской федерации планеты";
            z.doc = new Doc()
            {
                TypeDoc = "Паспорт РФ",
                SerDoc = "60 08",
                NumDoc = "123456",
                DateDoc = "12.11.2019",
                NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа солнечнодольска в изобильненском и новоалександровском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
            };

            z.Land = "Россия";
            z.regAddress = new RegAddress()
            {
                Index = "356100",
                Subj = "Краснодарский край",
                Rayon = "Староминской район",
                Town = "Станица Староминская",
                LocalityOrCity = "Станица Новофедоровская",
                Street = "Красноармейская улица",
                House = "120/1",
                Korp = "2",
                Kv = "202",
                DateReg = "01.05.1998"
            };
            z.Bomg = 1;
            z.factAddress = new Address()
            {
                Index = "341000",
                Subj = "Ростовская область",
                Rayon = "Каменский район",
                Town = "город Каменск",
                LocalityOrCity = "хутор Глубокий",
                Street = "Буденновская улица",
                House = "67",
                Korp = "Б",
                Kv = "16"
            };

            z.doc2 = new Doc()
            {
                TypeDoc = "Вид на жительство",
                SerDoc = "604566608",
                NumDoc = "1234568754211",
                DateDoc = "23.01.2022",
                NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа Каменск-Уральского в Степновском районах Приморского края, а также Южном федерального округа российской федерации планеты Земля"
            };

            z.VidStart = "01.10.2022";
            z.VidEnd = "12.12.2023";

            z.TrDogNum = "0001";
            z.TrDogSign = "01.03.2022";
            z.TrDogBeg = "01.03.2022";
            z.TrDogEnd = "01.03.2024";
            z.TrDogLocation = "ООО Доннефтестрой, Ростов-на-Дону";

            z.EaesSer = "0002";
            z.EaesNum = "12345676";

            z.EaesCat = "Почетный консул";
            z.PlaceOfStay = "Воронеж, Россияйская Федерация";
            z.DbegOfStay = "12.12.2023";
            z.DendOfStay = "12.12.2025";
            z.Snils = "789-567-123 98";

            z.PhoneMob = "+7(918)556-34-67";
            z.PhoneHome = "8(863)675-23-12";
            z.PhoneWork = "8(863)275-00-03";
            z.Email = "remarka@paint.net";

            //Способы оповещения
            z.notif = new Notif()
            {
                info = Inform.other,
                DespriptionOther = "Лично"
            };

            //Представитель
            z.agent = new Agent()
            {
                Famip = "Константинов",
                Namep = "Евгений",
                Otchp = "Николаевич",
                Sex = "ж",
                DR = "20.10.1945",
                Land = "Российская Федерация",
                status = AgentStatus.Father,
                doc = new Doc()
                {
                    TypeDoc = "Паспорт РФ",
                    SerDoc = "60 12",
                    NumDoc = "654321",
                    DateDoc = "09.11.2007",
                    NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа Рызвяный в изобильненском и труновском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
                },
                docStatusSer = "1111",
                docStatusNum = "22222222",
                docStatusDbeg = "02.02.2023",
                Snils = "123-456-123 99",
                ENP = "2678564500006789",
                regAddress = new RegAddress()
                {
                    Index = "678000",
                    Subj = "Ставропольский край",
                    Rayon = "Шпаковский район",
                    Town = "город Михайловск",
                    LocalityOrCity = "ЖК Соколова",
                    Street = "Портовая улица",
                    House = "10/12",
                    Korp = "27",
                    Kv = "13",
                    DateReg = "10.08.1999"
                },
                Bomg = 0,
                factAddress = new Address()
                {
                    Index = "342000",
                    Subj = "Волгоградская область",
                    Rayon = "Кировский район",
                    Town = "город Андропов",
                    LocalityOrCity = "хутор Широбоков",
                    Street = "Малиновского маршала улица",
                    House = "13",
                    Korp = "7",
                    Kv = "32"
                },
                PhoneMob = "+7(909)555-44-77",
                PhoneHome = "8(861)678-23-12",
                PhoneWork = "8(861)234-00-00",
                Email = "biblio@globus.ru"
            };
            //Менеждер
            z.Manager = "Коптева Т.П.";
            //Дата заявления
            z.DZ = "09.01.2023";

            List<ZaVkl> lz = new List<ZaVkl>();
            lz.Add(z);
            ActionZaVkl.CreateListPDF(lz);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           ZaPolis z = new ZaPolis();
            z.SmoName = "OOO МСО Панацея";
            z.prichina = PrichinaZameni.NeTochnost;
            z.Famip = "Василин";
            z.Namep = "Владимир";
            z.Otchp = "Константинович";
            z.Sex = "Ж";
            z.cat = Cat.cat11;
            z.DR = "15.06.1975";
            z.BirthPlac = "Поселок городского типа солнечнодольск изобильненского района ставропольского края российской федерации планеты";
            z.doc = new Doc()
            {
                TypeDoc = "Паспорт РФ",
                SerDoc = "60 08",
                NumDoc = "123456",
                DateDoc = "12.11.2019",
                NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа солнечнодольска в изобильненском и новоалександровском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
            };
            z.Land = "Россия";
            z.regAddress = new RegAddress()
            {
                Index = "356100",
                Subj = "Краснодарский край",
                Rayon = "Староминской район",
                Town = "Станица Староминская",
                LocalityOrCity = "Станица Новофедоровская",
                Street = "Красноармейская улица",
                House = "120/1",
                Korp = "2",
                Kv = "202",
                DateReg = "01.05.1998"
            };
            z.Bomg = 1;
            z.factAddress = new Address()
            {
                Index = "341000",
                Subj = "Ростовская область",
                Rayon = "Каменский район",
                Town = "город Каменск",
                LocalityOrCity = "хутор Глубокий",
                Street = "Буденновская улица",
                House = "67",
                Korp = "Б",
                Kv = "16"
            };

            z.doc2 = new Doc()
            {
                TypeDoc = "Вид на жительство",
                SerDoc = "604566608",
                NumDoc = "1234568754211",
                DateDoc = "23.01.2022",
                NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа Каменск-Уральского в Степновском районах Приморского края, а также Южном федерального округа российской федерации планеты Земля"
            };

            z.VidStart = "01.10.2022";
            z.VidEnd = "12.12.2023";

            z.TrDogNum = "0001";
            z.TrDogSign = "01.03.2022";
            z.TrDogBeg = "01.03.2022";
            z.TrDogEnd = "01.03.2024";
            z.TrDogLocation = "ООО Доннефтестрой, Ростов-на-Дону";

            z.EaesSer = "0002";
            z.EaesNum = "12345676";

            z.EaesCat = "Почетный консул";
            z.PlaceOfStay = "Воронеж, Россияйская Федерация";
            z.DbegOfStay = "12.12.2023";
            z.DendOfStay = "12.12.2025";
            z.Snils = "789-567-123 98";

            z.PhoneMob = "+7(918)556-34-67";
            z.PhoneHome = "8(863)675-23-12";
            z.PhoneWork = "8(863)275-00-03";
            z.Email = "remarka@paint.net";

            //Способы оповещения
            z.notif = new Notif()
            {
                info = Inform.other,
                DespriptionOther = "Лично"
            };

            //Прежние данные
            z.OldFamip = "Новикова";
            z.OldNamep = "Елена";
            z.OldOtchp = "Андреевна";
            z.OldSex = "м";
            z.OldDR = "19.07.1986";

            //Представитель
            z.agent = new Agent()
            {
                Famip = "Константинов",
                Namep = "Евгений",
                Otchp = "Николаевич",
                Sex = "ж",
                DR = "20.10.1945",
                Land = "Российская Федерация",
                status = AgentStatus.Father,
                doc = new Doc()
                {
                    TypeDoc = "Паспорт РФ",
                    SerDoc = "60 12",
                    NumDoc = "654321",
                    DateDoc = "09.11.2007",
                    NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа Рызвяный в изобильненском и труновском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
                },
                docStatusSer = "1111",
                docStatusNum = "22222222",
                docStatusDbeg = "02.02.2023",
                Snils = "123-456-123 99",
                ENP = "2678564500006789",
                regAddress = new RegAddress()
                {
                    Index = "678000",
                    Subj = "Ставропольский край",
                    Rayon = "Шпаковский район",
                    Town = "город Михайловск",
                    LocalityOrCity = "ЖК Соколова",
                    Street = "Портовая улица",
                    House = "10/12",
                    Korp = "27",
                    Kv = "13",
                    DateReg = "10.08.1999"
                },
                Bomg = 1,
                factAddress = new Address()
                {
                    Index = "342000",
                    Subj = "Волгоградская область",
                    Rayon = "Кировский район",
                    Town = "город Андропов",
                    LocalityOrCity = "хутор Широбоков",
                    Street = "Малиновского маршала улица",
                    House = "13",
                    Korp = "7",
                    Kv = "32"
                },
                PhoneMob = "+7(909)555-44-77",
                PhoneHome = "8(861)678-23-12",
                PhoneWork = "8(861)234-00-00",
                Email = "biblio@globus.ru"
            };
            //Менеждер
            z.Manager = "Коптева Т.П.";
            //Дата заявления
            z.DZ = "09.01.2023";

            //Второе заявление
            ZaPolis z2 = new ZaPolis();
            z2.SmoName = "OOO МСО Панацея";
            z2.prichina = PrichinaZameni.Okonchanie;
            z2.Famip = "Сазонов";
            z2.Namep = "Алексеей";
            z2.Otchp = "Петрович";
            z2.Sex = "М";
            z2.cat = Cat.cat15;

            List<ZaPolis> lz = new List<ZaPolis>();
            lz.Add(z); lz.Add(z2);
            ActionZaPolis.CreateListPDF(lz);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ZaSk z = new ZaSk();
            z.SmoName = "OOO МСО Панацея";
            z.prichina = PrichinaVibor.ZamenaSmoPlace;
            z.formPolis = FormaPolis.Bumaga;
            z.ENP = "0001000200030004";
            z.OldSmo = "ООО Адмирал";
            z.Famip = "Василин";
            z.Namep = "Владимир";
            z.Otchp = "Константинович";
            z.Sex = "Ж";
            z.cat = Cat.cat11;
            z.DR = "15.06.1975";
            z.BirthPlac = "Поселок городского типа солнечнодольск изобильненского района ставропольского края российской федерации планеты";
            z.doc = new Doc()
            {
                TypeDoc = "Паспорт РФ",
                SerDoc = "60 08",
                NumDoc = "123456",
                DateDoc = "12.11.2019",
                NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа солнечнодольска в изобильненском и новоалександровском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
            };

            z.Land = "Россия";
            z.regAddress = new RegAddress()
            {
                Index = "356100",
                Subj = "Краснодарский край",
                Rayon = "Староминской район",
                Town = "Станица Староминская",
                LocalityOrCity = "Станица Новофедоровская",
                Street = "Красноармейская улица",
                House = "120/1",
                Korp = "2",
                Kv = "202",
                DateReg = "01.05.1998"
            };
            z.Bomg = 1;
            z.factAddress = new Address()
            {
                Index = "341000",
                Subj = "Ростовская область",
                Rayon = "Каменский район",
                Town = "город Каменск",
                LocalityOrCity = "хутор Глубокий",
                Street = "Буденновская улица",
                House = "67",
                Korp = "Б",
                Kv = "16"
            };

            z.doc2 = new Doc()
            {
                TypeDoc = "Вид на жительство",
                SerDoc = "604566608",
                NumDoc = "1234568754211",
                DateDoc = "23.01.2022",
                NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа Каменск-Уральского в Степновском районах Приморского края, а также Южном федерального округа российской федерации планеты Земля"
            };

            z.VidStart = "01.10.2022";
            z.VidEnd = "12.12.2023";

            z.TrDogNum = "0001";
            z.TrDogSign = "01.03.2022";
            z.TrDogBeg = "01.03.2022";
            z.TrDogEnd = "01.03.2024";
            z.TrDogLocation = "ООО Доннефтестрой, Ростов-на-Дону";

            z.EaesSer = "0002";
            z.EaesNum = "12345676";

            z.EaesCat = "Почетный консул";
            z.PlaceOfStay = "Воронеж, Россияйская Федерация";
            z.DbegOfStay = "12.12.2023";
            z.DendOfStay = "12.12.2025";
            z.Snils = "789-567-123 98";

            z.PhoneMob = "+7(918)556-34-67";
            z.PhoneHome = "8(863)675-23-12";
            z.PhoneWork = "8(863)275-00-03";
            z.Email = "remarka@paint.net";

            //Способы оповещения
            z.notif = new Notif()
            {
                info = Inform.other,
                DespriptionOther = "Лично"
            };

            //Представитель
            z.agent = new Agent()
            {
                Famip = "Константинов",
                Namep = "Евгений",
                Otchp = "Николаевич",
                Sex = "ж",
                DR = "20.10.1945",
                Land = "Российская Федерация",
                status = AgentStatus.Father,
                doc = new Doc()
                {
                    TypeDoc = "Паспорт РФ",
                    SerDoc = "60 12",
                    NumDoc = "654321",
                    DateDoc = "09.11.2007",
                    NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа Рызвяный в изобильненском и труновском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
                },
                docStatusSer = "1111",
                docStatusNum = "22222222",
                docStatusDbeg = "02.02.2023",
                Snils = "123-456-123 99",
                ENP = "2678564500006789",
                regAddress = new RegAddress()
                {
                    Index = "678000",
                    Subj = "Ставропольский край",
                    Rayon = "Шпаковский район",
                    Town = "город Михайловск",
                    LocalityOrCity = "ЖК Соколова",
                    Street = "Портовая улица",
                    House = "10/12",
                    Korp = "27",
                    Kv = "13",
                    DateReg = "10.08.1999"
                },
                Bomg = 0,
                factAddress = new Address()
                {
                    Index = "342000",
                    Subj = "Волгоградская область",
                    Rayon = "Кировский район",
                    Town = "город Андропов",
                    LocalityOrCity = "хутор Широбоков",
                    Street = "Малиновского маршала улица",
                    House = "13",
                    Korp = "7",
                    Kv = "32"
                },
                PhoneMob = "+7(909)555-44-77",
                PhoneHome = "8(861)678-23-12",
                PhoneWork = "8(861)234-00-00",
                Email = "biblio@globus.ru"
            };
            //Менеждер
            z.Manager = "Коптева Т.П.";
            //Дата заявления
            z.DZ = "09.01.2023";

            List<ZaSk> lz = new List<ZaSk>();
            lz.Add(z);
            ActionZaSk.CreateListPDF(lz);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ZaDelay z = new ZaDelay();
            z.SmoName = "OOO МСО Панацея";
            z.Famip = "Василин";
            z.Namep = "Владимир";
            z.Otchp = "Константинович";
            z.Sex = "М";
            z.DR = "15.06.1975";
            z.BirthPlac = "Поселок городского типа солнечнодольск изобильненского района ставропольского края российской федерации планеты";
            z.doc = new Doc()
            {
                TypeDoc = "Паспорт РФ",
                SerDoc = "60 08",
                NumDoc = "123456",
                DateDoc = "12.11.2019",
                NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа солнечнодольска в изобильненском и новоалександровском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
            };

            z.Land = "Россия";
            z.Snils = "789-567-123 98";
            z.regAddress = "356100, Краснодарский край, Староминской район, Станица Староминская, Станица Новофедоровская, Красноармейская улица, д 120/1, корп 2б, кв 205";
            z.factAddress = "341000, Ростовская область, Каменский район, город Каменск, хутор Глубокий, Буденновская улица, д 67, корп Б, кв 16 ";

            z.Phone = "+7(918)556-34-67";
            z.Email = "remarka@paint.net";

            //Представитель
            z.agent = new Agent()
            {
                Famip = "Константинов",
                Namep = "Евгений",
                Otchp = "Николаевич",
                DR = "20.10.1945",
                Land = "Российская Федерация",
                doc = new Doc()
                {
                    TypeDoc = "Паспорт РФ",
                    SerDoc = "60 12",
                    NumDoc = "654321",
                    DateDoc = "09.11.2007",
                    NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа Рызвяный в изобильненском и труновском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
                },
                PhoneMob = "+7(909)555-44-77",
                Email = "biblio@globus.ru"
            };
            z.NumDover = "0000001";
            z.DateDover = "21.07.2000";
            //Менеждер
            z.Manager = "Коптева Т.П.";
            //Дата заявления
            z.DZ = "09.01.2023";

            List<ZaDelay> lz = new List<ZaDelay>();
            lz.Add(z);
            ActionZaDelay.CreateListPDF(lz);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ZaGet z = new ZaGet();
            z.SmoName = "OOO МСО Панацея";
            z.Famip = "Василин";
            z.Namep = "Владимир";
            z.Otchp = "Константинович";
            z.Sex = "М";
            z.DR = "15.06.1975";
            z.BirthPlac = "Поселок городского типа солнечнодольск изобильненского района ставропольского края российской федерации планеты";
            z.doc = new Doc()
            {
                TypeDoc = "Паспорт РФ",
                SerDoc = "60 08",
                NumDoc = "123456",
                DateDoc = "12.11.2019",
                NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа солнечнодольска в изобильненском и новоалександровском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
            };

            z.Land = "Россия";
            z.Snils = "789-567-123 98";
            z.regAddress = "356100, Краснодарский край, Староминской район, Станица Староминская, Станица Новофедоровская, Красноармейская улица, д 120/1, корп 2б, кв 205";
            z.factAddress = "341000, Ростовская область, Каменский район, город Каменск, хутор Глубокий, Буденновская улица, д 67, корп Б, кв 16 ";

            z.Phone = "+7(918)556-34-67";
            z.Email = "remarka@paint.net";

            //Представитель
            z.agent = new Agent()
            {
                Famip = "Константинов",
                Namep = "Евгений",
                Otchp = "Николаевич",
                DR = "20.10.1945",
                Land = "Российская Федерация",
                doc = new Doc()
                {
                    TypeDoc = "Паспорт РФ",
                    SerDoc = "60 12",
                    NumDoc = "654321",
                    DateDoc = "09.11.2007",
                    NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа Рызвяный в изобильненском и труновском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
                },
                PhoneMob = "+7(909)555-44-77",
                Email = "biblio@globus.ru"
            };
            z.NumDover = "0000001";
            z.DateDover = "21.07.2000";
            //Менеждер
            z.Manager = "Коптева Т.П.";
            //Дата заявления
            z.DZ = "09.01.2023";

            List<ZaGet> lz = new List<ZaGet>();
            lz.Add(z);
            ActionZaGet.CreateListPDF(lz);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ZaGiveup z = new ZaGiveup();
            z.SmoName = "OOO МСО Панацея";
            z.prichina = PrichinaGiveup.Giveup;
            z.ENP = "1234567891234567";
            z.Famip = "Василин";
            z.Namep = "Владимир";
            z.Otchp = "Константинович";
            z.Sex = "М";
            z.DR = "15.06.1975";
            z.BirthPlac = "Поселок городского типа солнечнодольск изобильненского района ставропольского края российской федерации планеты";
            z.doc = new Doc()
            {
                TypeDoc = "Паспорт РФ",
                SerDoc = "60 08",
                NumDoc = "123456",
                DateDoc = "12.11.2019",
                NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа солнечнодольска в изобильненском и новоалександровском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
            };

            z.Land = "Россия";
            z.Snils = "789-567-123 98";
            z.regAddress = "356100, Краснодарский край, Староминской район, Станица Староминская, Станица Новофедоровская, Красноармейская улица, д 120/1, корп 2б, кв 205";
            z.factAddress = "341000, Ростовская область, Каменский район, город Каменск, хутор Глубокий, Буденновская улица, д 67, корп Б, кв 16 ";

            z.Phone = "+7(918)556-34-67";
            z.Email = "remarka@paint.net";

            //Представитель
            z.agent = new Agent()
            {
                Famip = "Константинов",
                Namep = "Евгений",
                Otchp = "Николаевич",
                DR = "20.10.1945",
                Land = "Российская Федерация",
                doc = new Doc()
                {
                    TypeDoc = "Паспорт РФ",
                    SerDoc = "60 12",
                    NumDoc = "654321",
                    DateDoc = "09.11.2007",
                    NpDoc = "Управление федеральной миграционной службы министерства внутренних дел Поселка городского типа Рызвяный в изобильненском и труновском районах ставропольского края, а также северокавказского федерального округа российской федерации планеты Земля"
                },
                PhoneMob = "+7(909)555-44-77",
                Email = "biblio@globus.ru"
            };
            z.NumDover = "0000001";
            z.DateDover = "21.07.2000";
            //Менеждер
            z.Manager = "Коптева Т.П.";
            //Дата заявления
            z.DZ = "09.01.2023";

            List<ZaGiveup> lz = new List<ZaGiveup>();
            lz.Add(z);
            ActionZaGiveup.CreateListPDF(lz);
        }
    }
}
