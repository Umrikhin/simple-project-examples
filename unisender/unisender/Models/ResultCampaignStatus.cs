using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unisender.Models
{
    public class ResultCampaignStatus
    {
        public string task_uuid { get; set; }
        public string status { get; set; }
    }

    public class RootCampaignStatus
    {
        public ResultCampaignStatus result { get; set; }
    }

}
