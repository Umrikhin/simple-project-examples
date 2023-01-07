using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibGenerateZaFoms.Models
{
    public class Address
    {
        public string Index { get; set; }
        public string Subj { get; set; }
        public string Rayon { get; set; }
        public string Town { get; set; }
        public string LocalityOrCity { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Korp { get; set; }
        public string Kv { get; set; }

        public Address()
        {
            Index = string.Empty;
            Subj = string.Empty;
            Rayon = string.Empty;
            Town = string.Empty;
            LocalityOrCity = string.Empty;
            Street = string.Empty;
            House = string.Empty;
            Korp = string.Empty;
            Kv = string.Empty;
        }
    }

    public class RegAddress : Address
    {
        public string DateReg { get; set; }

        public RegAddress()
            : base()
        {
            DateReg = string.Empty;
        }
    }
}
