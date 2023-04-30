namespace Parcels.Models
{
    public class UserPortal
    {
        public int id { get; set; }
        public string vcUserName { get; set; } = string.Empty;
        public string vcPass { get; set; } = string.Empty;
        public int idSecurity { get; set; }
        public bool bActive { get; set; }
    }
}
