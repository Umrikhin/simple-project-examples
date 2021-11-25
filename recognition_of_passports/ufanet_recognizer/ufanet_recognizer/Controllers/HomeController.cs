using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ufanet_recognizer.Models;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Net;
using ufanet_recognizer.Infrastructure;

namespace ufanet_recognizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly appSettings _appSettings;

        public HomeController(IOptions<appSettings> appConfiguration)
        {
            _appSettings = appConfiguration.Value;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("result") != null)
            {
                var model = JsonConvert.DeserializeObject<ViewResultAll>(HttpContext.Session.GetString("result"));
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Recognize(string img_base64)
        {
            if (!string.IsNullOrEmpty(img_base64))
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var model = new ViewResultAll();
                model.imgBase64 = img_base64;
                model.rec = new List<ViewResultRec>();
                try
                {
                    var token = await Utils.GetResponseAccessToken(_appSettings.login, _appSettings.password);
                    var passport = await Utils.GetResponseResultat(token, img_base64);
                    var resultat = Utils.GetResult(passport);
                    model.rec = resultat;
                }
                catch (Exception err)
                {
                    TempData["error_rec"] = err.Message;
                }
                HttpContext.Session.SetString("result", JsonConvert.SerializeObject(model));
            }
            else
            {
                TempData["error_rec"] = "Файл не выбран!";
            }
            TempData["GoLink"] = "#form_result";
            return RedirectToAction("Index");
        }

    }
}
