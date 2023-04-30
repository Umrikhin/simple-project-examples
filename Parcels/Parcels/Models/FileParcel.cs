using System.ComponentModel.DataAnnotations;

namespace Parcels.Models
{
    public class FileParcel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CTERR { get; set; } = string.Empty;

        [Required]
        public string StartFile { get; set; } = string.Empty;

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public string RetFile { get; set; } = string.Empty;

        public DateTime? DateRet { get; set; }
    }
}
