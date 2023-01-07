using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibGenerateZaFoms.Models
{
    public class Agent
    {
        public string Famip { get; set; }
        public string Namep { get; set; }
        public string Otchp { get; set; }
        public string Sex { get; set; }
        public string DR { get; set; }
        public string Land { get; set; }
        public LibGenerateZaFoms.Utils.AgentStatus status { get; set; }
        public Doc doc { get; set; }
        public string docStatusSer { get; set; }
        public string docStatusNum { get; set; }
        public string docStatusDbeg { get; set; }
        public string Snils { get; set; }
        public string ENP { get; set; }
        public RegAddress regAddress { get; set; }
        public int Bomg { get; set; }
        public Address factAddress { get; set; }
        public string PhoneMob { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneWork { get; set; }
        public string Email { get; set; }

        public Agent()
        {
            Famip = string.Empty;
            Namep = string.Empty;
            Otchp = string.Empty;
            Sex  = string.Empty;
            DR  = string.Empty;
            Land  = string.Empty;
            status = Utils.AgentStatus.Mother;
            doc = new Doc();
            docStatusSer  = string.Empty;
            docStatusNum  = string.Empty;
            docStatusDbeg  = string.Empty;
            Snils  = string.Empty;
            ENP  = string.Empty;
            regAddress = new RegAddress();
            Bomg  = 0;
            factAddress  = new Address();
            PhoneMob  = string.Empty;
            PhoneHome  = string.Empty;
            PhoneWork  = string.Empty;
            Email  = string.Empty;
        }
    }
}
