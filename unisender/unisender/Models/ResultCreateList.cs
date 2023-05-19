using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    public class ResultCreateList
    {
        public int id { get; set; }
    }

    public class RootCreateList
    {
        public ResultCreateList result { get; set; }
    }

}
