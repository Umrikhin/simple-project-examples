using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    public class ResultCreateCampaing
    {
        public int campaign_id { get; set; }
        public string status { get; set; }
        public int count { get; set; }
    }

    public class RootCreateCampaing
    {
        public ResultCreateCampaing result { get; set; }
    }


}
