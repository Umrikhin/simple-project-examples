using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGenerateZaFoms.Utils;

namespace LibGenerateZaFoms.Models
{
    public class Notif
    {
        public Inform info { get; set; }
        public string DespriptionOther { get; set; }

        public Notif()
        {
            info = Inform.sms;
            DespriptionOther = string.Empty;
        }
    }
}
