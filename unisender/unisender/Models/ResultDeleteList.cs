using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    public class ResultDeleteList
    {
        public string error { get; set; }
        public string warnings { get; set; }
    }

    public class RootDeleteList
    {
        public ResultDeleteList result { get; set; }
    }
}
