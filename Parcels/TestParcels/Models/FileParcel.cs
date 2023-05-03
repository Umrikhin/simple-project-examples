using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParcels.Models
{
    public class FileParcel
    {
        public Guid Id { get; set; }
        public string CTERR { get; set; } = string.Empty;
        public int IdUser { get; set; } = 1;
        public string StartFile { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public string RetFile { get; set; } = string.Empty;
        public DateTime? DateRet { get; set; }
    }
}
