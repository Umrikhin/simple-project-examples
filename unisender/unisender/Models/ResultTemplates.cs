using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    public class ResultTemplate
    {
        public string id { get; set; }
        public string sub_user_login { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string lang_code { get; set; }
        public string subject { get; set; }
        public string attachments { get; set; }
        public string screenshot_url { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
        public string message_format { get; set; }
        public string type { get; set; }
        public string body { get; set; }
        public object raw_body { get; set; }
        public string fullsize_screenshot_url { get; set; }
    }

    public class RootResultTemplate
    {
        public List<ResultTemplate> result { get; set; }
    }
}
