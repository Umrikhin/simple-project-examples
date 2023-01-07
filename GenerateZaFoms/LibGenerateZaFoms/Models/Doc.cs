using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibGenerateZaFoms.Models
{
    public class Doc
    {
        public string TypeDoc { get; set; }
        public string SerDoc { get; set; }
        public string NumDoc { get; set; }
        public string DateDoc { get; set; }
        public string NpDoc { get; set; }

        public Doc()
        {
            TypeDoc = string.Empty;
            SerDoc = string.Empty;
            NumDoc = string.Empty;
            DateDoc = string.Empty;
            NpDoc = string.Empty;
        }
    }
}
