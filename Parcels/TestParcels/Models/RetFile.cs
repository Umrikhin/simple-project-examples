using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParcels.Models
{
    public class RetFile
    {
        public string NameFile { get; set; } = string.Empty;
        public byte[]? BodyRetFile { get; set; }
    }
}
