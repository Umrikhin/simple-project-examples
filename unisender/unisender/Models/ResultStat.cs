using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    public class ResultStat
    {
        public int total { get; set; }
        public int sent { get; set; }
        public int delivered { get; set; }
        public int read_unique { get; set; }
        public int read_all { get; set; }
        public int clicked_unique { get; set; }
        public int clicked_all { get; set; }
        public int unsubscribed { get; set; }
        public int spam { get; set; }
    }

    public class RootStat
    {
        public ResultStat result { get; set; }
    }
}
