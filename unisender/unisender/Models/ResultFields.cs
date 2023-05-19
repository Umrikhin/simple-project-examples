using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    public class ResultFields
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int is_visible { get; set; }
        public int view_pos { get; set; }
    }

    public class RootFields
    {
        public List<ResultFields> result { get; set; }
    }
}
