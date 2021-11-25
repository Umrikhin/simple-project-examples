using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ufanet_rec.Models
{
    public class BodyRequest
    {
        public string file { get; set; }
        public string doc_type { get; set; }
    }

    public class PassportRecognition
    {
        public string status { get; set; }
        public Passport detail { get; set; }
        public int status_id { get; set; }
        public string error_message { get; set; }
    }

    public class Passport
    {
        public string series_number { get; set; }
        public string authority { get; set; }
        public string issue_date { get; set; }
        public string authority_code { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
        public string birthplace { get; set; }
    }

    public class BodyResultRec403
    {
        public string detail { get; set; }
        public object error_detail { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public string error_message { get; set; }
    }

    public class BodyResultRec400
    {
        public string detail { get; set; }
        public object error_detail { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public string error_message { get; set; }
    }

    public class CredentialForToken
    {
        public string login { get; set; }
        public string password { get; set; }
    }

    public class BodyResultToken200
    {
        public DetailsToken detail { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public string error_message { get; set; }
    }

    public class DetailsToken
    {
        public string access { get; set; }
        public string refresh { get; set; }
    }
}
