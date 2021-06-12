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
    public class AccountController : Controller
    {
        //Listteleme Yanlış olabilir.
        public ActionResult Index()
        {
            var id = Session["id"].ToString();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4500/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.GetAsync($"api/account/{id}").Result;
                string result = response.Content.ReadAsStringAsync().Result;
                //dynamic json = JsonConvert.DeserializeObject(result);
                JObject Jsonparse = JObject.Parse(result);
                JArray json = (JArray)Jsonparse["data"];
                //response.EnsureSuccessStatusCode();
                List<UsersViewModel> modelList = new List<UsersViewModel>();
                if (response.IsSuccessStatusCode == true)
                {
                    if (json != null)
                    {
                        foreach (var item in json.ToList())
                        {
                            var model = new UsersViewModel();
                            model.name = item["name"].ToString();
                            model.surname = item["surname"].ToString();
                            model.email = item["email"].ToString();
                            model.Marka = item["brand_name"].ToString();
                            model.CityName = item["city_name"].ToString();
                            model.Plaka = item["plate"].ToString();
                            modelList.Add(model);
                        }
                        return View(modelList);
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
        public ActionResult Location(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4500/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.GetAsync($"api/map/{id}").Result;
                string result = response.Content.ReadAsStringAsync().Result;
                JObject Jsonparse = JObject.Parse(result);
                JArray json = (JArray)Jsonparse["data"];
                LocationViewModel model = new LocationViewModel();
                if (response.IsSuccessStatusCode == true)
                {
                    if (json != null)
                    {
                        model.speed = (string)json[0]["speed"].ToString();
                        model.enlem = (string)json[0]["enlem"].ToString();
                        model.boylam = (string)json[0]["boylam"].ToString();
                        return View(model);
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