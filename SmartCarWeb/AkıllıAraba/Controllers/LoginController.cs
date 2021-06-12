using AkıllıAraba.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AkıllıAraba.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModels model)
        {
            var entiy = new LoginViewModels
            {
                code = model.code,
                tc = model.tc,
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4500/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.PostAsJsonAsync("api/user/", entiy).Result;
                //response.EnsureSuccessStatusCode();
                //Session.Add("İd", entiy.id);
                string result = response.Content.ReadAsStringAsync().Result;
                JObject Jsonparse = JObject.Parse(result);
                JArray json = (JArray)Jsonparse["data"];
                if (response.IsSuccessStatusCode == true)
                {
                    FormsAuthentication.SetAuthCookie(entiy.tc, false);
                    Session.Add("id", (string)json[0]["id"].ToString());
                    return RedirectToAction("Index", "Vehicles");
                }
                else
                {
                    ViewBag.Mesaj = "TC veya Code Yanlış";
                    return View();
                }
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}