using Parcels.Models;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Parcels.Services
{
    public class UsersPortalRepository : IUsersPortalRepository
    {
        private List<UserPortal> data = new();
        private string cn;
        public UsersPortalRepository(IConfiguration conf)
        {
            cn = conf.GetSection("ConnectionStrings")["BaseOmsConnection"];
        }

        public UserPortal? GetUserPortalActive(int id, out string error)
        {
            UserPortal? user = new UserPortal();
            error = string.Empty;
            try
            {
                using (IDbConnection db = new SqlConnection(cn))
                {
                    user = db.Query<UserPortal>($"SELECT * FROM tbUsers where id='{id}' and bActive=1").FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return user;
        }

        public List<UserPortal> GetUsersPortal(out string error)
        {
            List<UserPortal> users = new List<UserPortal>();
            error = string.Empty;
            try
            {
                using (IDbConnection db = new SqlConnection(cn))
                {
                    users = db.Query<UserPortal>("SELECT * FROM tbUsers").ToList();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return users;
        }
    }

    public class MockUsersPortalRepository : IUsersPortalRepository
    {
        private List<UserPortal> data;
        public MockUsersPortalRepository()
        {
            data = new List<UserPortal>() {
                new UserPortal() { id = 1, vcUserName="Admin", vcPass=Utils.Helpers.MD5Hash("123"), bActive=true, idSecurity=1 },
                new UserPortal() { id = 2, vcUserName="Root", vcPass=Utils.Helpers.MD5Hash("123"), bActive=true, idSecurity=1 },
                new UserPortal() { id = 3, vcUserName="Guest", vcPass=Utils.Helpers.MD5Hash("321"), bActive=true, idSecurity=1 }
            };
        }

        public UserPortal? GetUserPortalActive(int id, out string error)
        {
            UserPortal? user = new UserPortal();
            error = string.Empty;
            try
            {
                user = data.Where(x => x.id == id && x.bActive == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return user;
        }

        public List<UserPortal> GetUsersPortal(out string error)
        {
            List<UserPortal> users = new List<UserPortal>();
            error = string.Empty;
            try
            {
                users = data;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return users;
        }
    }
}
