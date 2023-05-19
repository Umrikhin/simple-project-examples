using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    public class ResultTask
    {
        public string task_uuid { get; set; }
        public string task_type { get; set; }
        public string status { get; set; }
        public string file_to_download { get; set; }
    }

    public class RootTask
    {
        public ResultTask result { get; set; }
    }

}
