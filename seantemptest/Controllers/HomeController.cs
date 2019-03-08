using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;

using seantemptest.Models;
using seantemptest.Providers;

namespace seantemptest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string query)
        {
            var ret = new RootNodeViewModel();
            try
            {
                var token = AuthProvider.GetAuthToken();
                var url = ConfigurationManager.AppSettings["Search_Url"].ToString();
                var req = WebRequest.Create(string.Format(url, query));
                
                req.Headers.Add("Authorization", token.TokenType + " " + token.AccessToken);
                req.Method = "Get";
              
                using (var searchResult = req.GetResponse())
                {
                    using (var reader = new StreamReader(searchResult.GetResponseStream()))
                    {
                        var temp = reader.ReadToEnd();
                        ret = JsonConvert.DeserializeObject<RootNodeViewModel>(temp);
                    }
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, data = ret }, JsonRequestBehavior.AllowGet);
        }
    }
}