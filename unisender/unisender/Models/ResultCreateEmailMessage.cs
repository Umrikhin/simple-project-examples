using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    // https://json2csharp.com/
    public class ResultCreateEmailMessage
    {
        public int message_id { get; set; }
    }

    public class RootCreateEmailMessage
    {
        public ResultCreateEmailMessage result { get; set; }
    }
}
