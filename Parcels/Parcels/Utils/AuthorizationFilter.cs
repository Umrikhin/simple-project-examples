using Parcels.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Parcels.Utils
{
    public class AuthorizationFilter : Attribute, IAsyncActionFilter
    {
        IUsersPortalRepository _users;
        public AuthorizationFilter(IUsersPortalRepository users)
        {
            _users = users;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool exists_auth = false;
            if (context.HttpContext.Session.GetString("UserId_Parcels") != null)
            {
                exists_auth = true;
            }
            else
            {
                if (context.HttpContext.Request.Cookies.ContainsKey("UserId_Parcels"))
                {
                    var cookie = context.HttpContext.Request.Cookies["UserId_Parcels"] ?? string.Empty;

                    //Установка имени пользователя в сессию
                    try
                    {
                        int currentUserId = Convert.ToInt32(cookie);
                        string currentUserName = string.Empty;
                        string error = string.Empty;
                        var user = _users.GetUserPortalActive(currentUserId, out error);
                        if (error.Length > 0) throw new Exception(error);
                        currentUserName = user != null ? user.vcUserName : string.Empty;
                        if (currentUserName.Length > 0)
                        {
                            context.HttpContext.Session.SetString("UserId_Parcels", cookie);
                            context.HttpContext.Session.SetString("UserName", currentUserName);
                            exists_auth = true;
                        }
                    }
                    catch
                    {
                        context.HttpContext.Session.Remove("UserId_Parcels");
                        context.HttpContext.Response.Cookies.Delete("UserId_Parcels");
                    }
                }
            }
            if (!exists_auth)
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login" }));
            else
                await next();
        }
    }
}
