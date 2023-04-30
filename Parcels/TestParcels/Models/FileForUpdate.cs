using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParcels.Models
{
    public class FileForUpdate
    {
        public FileParcel? Parcel { get; set; }
        public bool IsUpdate { get; set; }
    }
}
