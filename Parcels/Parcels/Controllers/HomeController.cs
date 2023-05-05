using Microsoft.AspNetCore.Mvc;
using Parcels.Models;
using Parcels.Services;
using Parcels.Utils;
using System.Text.RegularExpressions;
using System;
using System.Runtime.ConstrainedExecution;

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
        public IActionResult Index(int day = 1, int terr = 0)
        {
            var rows = _repData.GetTop(day: day);
            if (terr > 0) { rows = rows.Where(x => x.CTERR.Equals(terr.ToString())).ToList(); }
            ViewBag.Days = GetDaysForSelect(day);
            ViewBag.Terr = GetTerrForSelect(terr);
            return View(rows);
        }

        //Получение данных по состоянию счета за период (выбранный год)
        [HttpPost]
        [ServiceFilter(typeof(AuthorizationFilter))]
        public IActionResult GetDataParcels(int days = 1, int terr = 0)
        {
            return RedirectToAction("Index", "Home", new { day = days, terr });
        }

        Microsoft.AspNetCore.Mvc.Rendering.SelectList GetDaysForSelect(int selItem)
        {
            List<Period> items = new List<Period>();
            items.Add(new Period() { Val = 1, Name = "Последний 1 день" });
            items.Add(new Period() { Val = 3, Name = "Последние 3 дня" });
            items.Add(new Period() { Val = 5, Name = "Последние 5 дней" });
            items.Add(new Period() { Val = 10, Name = "Последние 10 дней" });
            items.Add(new Period() { Val = 15, Name = "Последние 15 дней" });
            items.Add(new Period() { Val = 30, Name = "Последние 30 дней" });
            var selList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(items, "Val", "Name");
            selList.Where(x => x.Value == selItem.ToString()).ToList().ForEach(z => { z.Selected = true; });
            return selList;
        }

        Microsoft.AspNetCore.Mvc.Rendering.SelectList GetTerrForSelect(int selItem)
        {
            List<ElTerr> items = new List<ElTerr>();
            items.Add(new ElTerr() { Val = 0, Name = "Все" });
            items.Add(new ElTerr() { Val = 401, Name = "Ростов" });
            items.Add(new ElTerr() { Val = 404, Name = "Азов" });
            items.Add(new ElTerr() { Val = 407, Name = "Батайск" });
            items.Add(new ElTerr() { Val = 409, Name = "Б.Калитва" });
            items.Add(new ElTerr() { Val = 430, Name = "Новошахтинск" });
            items.Add(new ElTerr() { Val = 415, Name = "Гуково" });
            items.Add(new ElTerr() { Val = 417, Name = "Донецк" });
            items.Add(new ElTerr() { Val = 235, Name = "Мясниковский" });
            items.Add(new ElTerr() { Val = 231, Name = "Матвеево-Курганский" });
            items.Add(new ElTerr() { Val = 219, Name = "Зимовниковский" });
            items.Add(new ElTerr() { Val = 245, Name = "Пролетарский" });
            items.Add(new ElTerr() { Val = 233, Name = "Милютинский" });
            items.Add(new ElTerr() { Val = 207, Name = "Боковский" });
            var selList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(items, "Val", "Name");
            selList.Where(x => x.Value == selItem.ToString()).ToList().ForEach(z => { z.Selected = true; });
            return selList;
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
