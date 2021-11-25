using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ufanet_rec.Models;
using System.IO;
using Newtonsoft.Json;
using MimeKit;
using Microsoft.Extensions.Options;
using System.Net;

namespace ufanet_rec.Controllers
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
            try
            {
                Utils.ClearOldFile();
            }
            catch
            {

            }
            if (HttpContext.Session.GetString("result") != null)
            {
                var model = JsonConvert.DeserializeObject<ViewResultAll>(HttpContext.Session.GetString("result"));
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(3145728)]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadedFile != null)
                    {
                        var fileName = System.IO.Path.GetFileName(uploadedFile.FileName);
                        FileInfo f = new System.IO.FileInfo(fileName);
                        Guid img_prefix = Guid.NewGuid();
                        string img = $"{img_prefix}_{f.Name}";
                        if (!Directory.Exists(Utils.pathDir)) Directory.CreateDirectory(Utils.pathDir);
                        using (var fileStream = new FileStream(Path.Combine(Utils.pathDir, img), FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);
                        }
                        var rec = new ViewResultAll() { imageFilepath = img, rec = new List<ViewResultRec>() };
                        HttpContext.Session.SetString("result", JsonConvert.SerializeObject(rec));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Нет выбранного файла!");
                        if (HttpContext.Session.GetString("result") != null)
                        {
                            var model = JsonConvert.DeserializeObject<ViewResultAll>(HttpContext.Session.GetString("result"));
                            return View("Index", model);
                        }
                        return View("Index");
                    }
                }
                catch (Exception err)
                {
                    ModelState.AddModelError("", err.Message);
                    if (HttpContext.Session.GetString("result") != null)
                    {
                        var model = JsonConvert.DeserializeObject<ViewResultAll>(HttpContext.Session.GetString("result"));
                        return View("Index", model);
                    }
                    return View("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неверно задано изображение.");
                if (HttpContext.Session.GetString("result") != null)
                {
                    var model = JsonConvert.DeserializeObject<ViewResultAll>(HttpContext.Session.GetString("result"));
                    return View("Index", model);
                }
                return View("Index");
            }
        }

        [HttpGet]
        [Route("img-for-rec")]
        public IActionResult ImgFile()
        {
            string webRootPath = Utils.pathDir;
            string fileName = string.Empty;
            if (HttpContext.Session.GetString("result") != null)
            {
                var model = JsonConvert.DeserializeObject<ViewResultAll>(HttpContext.Session.GetString("result"));
                fileName = model.imageFilepath;
                var filePath = Path.Combine(webRootPath, fileName);
                if (filePath == null) return NotFound();
                Stream s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return new FileStreamResult(s, "image/jpeg");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Recognize()
        {
            string webRootPath = Utils.pathDir;
            string fileName = string.Empty;
            if (HttpContext.Session.GetString("result") != null)
            {
                var model = JsonConvert.DeserializeObject<ViewResultAll>(HttpContext.Session.GetString("result"));
                fileName = model.imageFilepath;
                var filePath = Path.Combine(webRootPath, fileName);
                if (filePath != null)
                {
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                    try
                    {
                        model.rec.Clear();
                        var token = await Utils.GetResponseAccessToken(_appSettings.login, _appSettings.password);
                        var passport = await Utils.GetResponseResultat(token, Utils.ImageToBase64(filePath));
                        var resultat = Utils.GetResult(passport);
                        model.rec = resultat;
                    }
                    catch(Exception err)
                    {
                        TempData["error_rec"] = err.Message;
                    }
                }
                HttpContext.Session.SetString("result", JsonConvert.SerializeObject(model));
                TempData["GoLink"] = "#form_result";
                return RedirectToAction("Index");
            }
            return View("Index");
        }

    }
}
