using Microsoft.AspNetCore.Mvc;
using Parcels.Models;
using Parcels.Services;
using Parcels.Utils;
using System.Text.RegularExpressions;
using System;

namespace Parcels.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly Repository _repData;
        private IUsersPortalRepository _repositoryUsers;

        public HomeController(IUsersPortalRepository repoUsersPortal, IConfiguration config)
        {
            _repositoryUsers = repoUsersPortal;
            int timeout = Convert.ToInt32(config.GetSection("MaxTimeOut").Value);
            string connectionString = config.GetSection("ConnectionStrings")["DefaultConnection"];
            _repData = new Repository(connectionString, timeout);
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        public IActionResult Index(int day = 1)
        {
            var rows = _repData.GetTop(day: day);
            return View(rows);
        }

        public IActionResult Login()
        {
            return View();
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId_Parcels");
            DeleteCookie();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginUser login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string err = string.Empty;
                    var users = _repositoryUsers.GetUsersPortal(out err);
                    if (err.Length > 0)
                    {
                        ModelState.AddModelError("", err);
                        return View("Login", login);
                    }
                    int currentUserId = -1;
                    string currentUserName = string.Empty;
                    var p = Helpers.MD5Hash(login.Password);
                    if (users.Where(x => x.vcUserName.ToLower() == login.User.ToLower() && x.vcPass == Helpers.MD5Hash(login.Password) && x.bActive == true).Count() > 0)
                    {
                        var user = users.Where(x => x.vcUserName.ToLower() == login.User.ToLower() && x.vcPass == Helpers.MD5Hash(login.Password) && x.bActive == true).FirstOrDefault();
                        currentUserId = user != null ? user.id : -1;
                        currentUserName = user != null ? user.vcUserName : string.Empty;
                        HttpContext.Session.SetString("UserId_Parcels", currentUserId.ToString());
                        HttpContext.Session.SetString("UserName", currentUserName);
                        SaveCookie(currentUserId.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователь не существует. Вход невозможен!");
                        return View("Login", login);
                    }
                }
                catch (Exception err)
                {
                    ModelState.AddModelError("", err.Message);
                    return View("Login", login);
                }
            }
            else
            {
                return View("Login", login);
            }
        }
        private void SaveCookie(string UserId)
        {
            try
            {
                if (!Request.Cookies.ContainsKey("UserId_Parcels"))
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = new DateTimeOffset(DateTime.Now.AddYears(1));
                    Response.Cookies.Append("UserId_Parcels", UserId, option);
                }
            }
            catch
            {

            }
        }
        private void DeleteCookie()
        {
            try
            {
                Response.Cookies.Delete("UserId_Parcels");
            }
            catch
            {

            }
        }

        [HttpGet]
        public IActionResult FindUsers()
        {
            var name = HttpContext.Request.Query["frarment"].ToString();
            string error = string.Empty;
            var data = _repositoryUsers.GetUsersPortal(out error).Where(x => x.vcUserName.ToLower().Contains(name.ToLower()) && x.bActive == true).ToList();
            return Ok(data);
        }
    }
}
