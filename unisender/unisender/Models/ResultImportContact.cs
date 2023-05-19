using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    public class Log
    {
        public int index { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }

    public class ResultImportContact
    {
        public int total { get; set; }
        public int inserted { get; set; }
        public int updated { get; set; }
        public int deleted { get; set; }
        public int new_emails { get; set; }
        public int invalid { get; set; }
        public List<Log> log { get; set; }
    }

    public class RootImportContact
    {
        public ResultImportContact result { get; set; }
    }
}
