using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    public class ErrorEmailMessage
    {
        public string error { get; set; }
        public string code { get; set; }
        public string result { get; set; }
    }
}
