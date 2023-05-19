using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    public class ResultGetList
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class RootGetList
    {
        public List<ResultGetList> result { get; set; }
    }

}
