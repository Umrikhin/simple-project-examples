using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibGenerateZaFoms.Utils
{
    public enum Cat
    {
        cat1, cat2, cat3, cat4, cat5, cat6, cat7, cat8, cat9, cat10, cat11, cat12, cat13, cat14, cat15, cat16
    }

    public enum Inform
    {
        sms, mail, email, call, msg, other
    }

    public enum AgentStatus
    {
        Mother, Father, Opekun, Popechitel, Usinovitel, ByProxy
    }

    public enum PrichinaZameni
    {
        IzmFam, NeTochnost, Okonchanie
    }

    public enum PrichinaVibor
    {
        ViborSmo, ZamenaSmoOneYear, ZamenaSmoPlace, ZamenaSmoEndDog
    }

    public enum FormaPolis
    {
        Bumaga, CancelGetPolis
    }

    public enum PrichinaGiveup
    {
        Giveup, Utrata
    }
}
