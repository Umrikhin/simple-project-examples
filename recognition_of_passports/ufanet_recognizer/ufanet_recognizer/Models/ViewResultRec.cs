using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ufanet_recognizer.Models
{
    public class ViewResultAll
    {
        public string imgBase64 { get; set; }
        public List<ViewResultRec> rec { get; set; }
    }

    public class ViewResultRec
    {
        public string Field { get; set; }
        public string RecValue { get; set; }
    }
}
