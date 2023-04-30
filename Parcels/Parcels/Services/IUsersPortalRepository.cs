using Parcels.Models;

namespace Parcels.Services
{
    public interface IUsersPortalRepository
    {
        List<UserPortal> GetUsersPortal(out string error);
        UserPortal? GetUserPortalActive(int id, out string error);
    }
}
