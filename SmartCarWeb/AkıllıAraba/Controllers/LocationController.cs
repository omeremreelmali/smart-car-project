using AkıllıAraba.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace AkıllıAraba.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Location()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4500/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.GetAsync($"api/map/").Result;
                string result = response.Content.ReadAsStringAsync().Result;
                JObject Jsonparse = JObject.Parse(result);
                JArray json = (JArray)Jsonparse["data"];
                List<LocationViewModel> modellist = new List<LocationViewModel>();
                if (response.IsSuccessStatusCode == true)
                {
                    if (json != null)
                    {
                        var index = 0;
                        foreach (var item in json)
                        {
                            var model = new LocationViewModel();
                            model.speed = (string)json[index]["speed"].ToString();
                            model.enlem = (string)json[index]["enlem"].ToString();
                            model.boylam = (string)json[index]["boylam"].ToString();
                            modellist.Add(model);
                            index++;
                        }
                        return View(modellist);
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    ViewBag.Mesaj = "Aynı Değerleri Girdiniz Değiştiriniz";
                    return View();
                }
            }
        }
    }
}