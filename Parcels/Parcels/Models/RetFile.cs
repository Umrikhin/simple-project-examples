using System.ComponentModel.DataAnnotations;

namespace Parcels.Models
{
    public class RetFile
    {
        [Required]
        public string NameFile { get; set; } = string.Empty;
        [Required]
        public byte[]? BodyRetFile { get; set; }
    }
}
