using AkıllıAraba.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using PagedList;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AkıllıAraba.Controllers
{
    [Authorize(Roles = "99")]
    public class UsersController : Controller
    {
        MySqlConnection baglantı;
        MySqlDataAdapter adapter;
        public ActionResult Index(int page = 1)
        {
            string sql = "CREATE VIEW userlist as Select id,tc,code,email,name,surname from users Order by id";
            DataTable dt = new DataTable();
            baglantı = new MySqlConnection("Server=Localhost;Database=smart_car;Uid=root;Pwd=;charset=utf8;");
            adapter = new MySqlDataAdapter();
            MySqlCommand commond = new MySqlCommand();
            commond.CommandText = sql;
            commond.Connection = baglantı;
            adapter.SelectCommand = commond;
            List<UsersViewModel> user = new List<UsersViewModel>();
            MySqlDataAdapter adapters = new MySqlDataAdapter("SELECT * FROM userlist", baglantı);
            adapters.Fill(dt);
            adapters.Dispose();
            baglantı.Close();
            foreach (DataRow item in dt.Rows)
            {
                UsersViewModel entity = new UsersViewModel
                {
                    id = int.Parse(item.ItemArray[0].ToString()),
                    tc = item.ItemArray[1].ToString(),
                    code = item.ItemArray[2].ToString(),
                    email = item.ItemArray[3].ToString(),
                    name = item.ItemArray[4].ToString(),
                    surname = item.ItemArray[5].ToString(),
                };
                user.Add(entity);
            }
            return View(user);
        }
        public List<SelectListItem> DropDownList(string path)
        {
            DataTable table = new DataTable();
            MySqlConnection connection = new MySqlConnection("Server=Localhost;Database=smart_car;Uid=root;Pwd=;charset=utf8;");
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand commond = new MySqlCommand();
            string sql = "SELECT * FROM " + path;
            commond.CommandText = sql;
            commond.Connection = connection;
            adapter.SelectCommand = commond;
            DropDownList dropdownList = new DropDownList();
            adapter.Fill(table);
            adapter.Dispose();
            connection.Close();
            dropdownList.DataSource = table;
            List<SelectListItem> DropList = new List<SelectListItem>();
            var count = 0;
            foreach (var item in table.Rows)
            {
                DropList.Add(new SelectListItem { Text = table.Rows[count].ItemArray[1].ToString(), Value = table.Rows[count].ItemArray[0].ToString() });
                ++count;
            }
            dropdownList.DataBind();
            return DropList;
        }
        public ActionResult Create(int id)
        {
            ViewBag.City = DropDownList("cities");
            ViewBag.Color = DropDownList("colors");
            ViewBag.EngineModel = DropDownList("engine_model");
            ViewBag.Brands = DropDownList("brands");
            return View();
        }
        [HttpPost]
        public ActionResult Create(VehiclesViewModel model, int id)
        {
            var datetime = DateTime.Now.ToString("yyyy/MM/dd");
            var entity = new VehiclesViewModel
            {
                user_id = id,
                color = model.color,
                engine_model = model.engine_model,
                plate = model.plate,
                brand = model.VehicleBrandList,
                model = model.VehicleModelList,
                city_code = model.plate_code,
            };
            /*var location = new LocationViewModel()
            {
                boylam = (27.7041138).ToString(),
                enlem = (27.7041138).ToString(),
                speed = (0).ToString(),
                vecihle_id = entity.ToString(),
                date = datetime
            };*/
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4500/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.PostAsJsonAsync("api/car/", entity).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("Index", "Users");
                }
                ViewBag.Mesaj = "Aynı Değerleri Girdiniz Değiştiriniz";
                ViewBag.City = DropDownList("cities");
                ViewBag.Color = DropDownList("colors");
                ViewBag.EngineModel = DropDownList("engine_model");
                ViewBag.Brands = DropDownList("brands");
                return View();
            }
        }
        public JsonResult SelectVehicle(int id)
        {
            //Bunu Yaparken View Kontrol Edersin
            var sql = "SELECT * FROM models WHERE models.brand_id = " + id;
            DataTable dtmodel = new DataTable();
            baglantı = new MySqlConnection("Server=Localhost;Database=smart_car;Uid=root;Pwd=;charset=utf8;");
            adapter = new MySqlDataAdapter();
            MySqlCommand commond = new MySqlCommand();
            commond.CommandText = sql;
            commond.Connection = baglantı;
            adapter.SelectCommand = commond;
            DropDownList DropDownmodelList = new DropDownList();
            adapter.Fill(dtmodel);
            adapter.Dispose();
            baglantı.Close();
            DropDownmodelList.DataSource = dtmodel;
            List<SelectListItem> ModelList = new List<SelectListItem>();
            var x = 0;
            foreach (var item in dtmodel.Rows)
            {
                ModelList.Add(new SelectListItem { Text = dtmodel.Rows[x].ItemArray[2].ToString(), Value = dtmodel.Rows[x].ItemArray[0].ToString() });
                ++x;
            }
            DropDownmodelList.DataBind();
            //ViewBag.Vehiclesbrandlist = citys;
            baglantı.Close();
            return Json(ModelList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(long id)
        {
            var sql = "SELECT * FROM `users` WHERE id = " + id;
            DataTable dt = new DataTable();
            baglantı = new MySqlConnection("Server=Localhost;Database=smart_car;Uid=root;Pwd=;charset=utf8;");
            adapter = new MySqlDataAdapter();
            MySqlCommand commond = new MySqlCommand();
            commond.CommandText = sql;
            commond.Connection = baglantı;
            adapter.SelectCommand = commond;
            adapter.Fill(dt);
            UsersViewModel model = new UsersViewModel
            {
                tc = dt.Rows[0].ItemArray[1].ToString(),
                code = dt.Rows[0].ItemArray[2].ToString(),
                email = dt.Rows[0].ItemArray[3].ToString(),
                name = dt.Rows[0].ItemArray[4].ToString(),
                surname = dt.Rows[0].ItemArray[5].ToString(),
            };
            adapter.Dispose();
            baglantı.Close();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(UsersViewModel model)
        {
            var entiy = new UsersViewModel
            {
                code = model.code,
                tc = model.tc,
                email = model.email,
                name = model.name,
                surname = model.surname,
                id = model.id
            };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4500/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("api/user/", entiy).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    return View();
                }
            }
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4500/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.DeleteAsync($"api/user/{id}").Result;
                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ViewBag.Mesaj = "Aynı Değerleri Girdiniz Değiştiriniz";
                    return View();
                }
            }
        }
        public ActionResult DeleteCar(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4500/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.DeleteAsync($"api/car/{id}").Result;
                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ViewBag.Mesaj = "Aynı Değerleri Girdiniz Değiştiriniz";
                    return View();
                }
            }
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(UsersViewModel model)
        {
            var entiy = new UsersViewModel
            {
                code = model.code,
                tc = model.tc,
                email = model.email,
                name = model.name,
                surname = model.surname
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4500/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.PostAsJsonAsync("api/user/register", model).Result;
                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("Index", "Users");
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
        public ActionResult Details(int id)
        {
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
                        foreach (var item in json)
                        {
                            var model = new UsersViewModel();
                            model.id = int.Parse(item["id"].ToString());
                            model.name = item["name"].ToString();
                            model.surname = item["surname"].ToString();
                            model.email = item["email"].ToString();
                            model.Marka = item["brand_name"].ToString();
                            model.CityName = item["city_name"].ToString();
                            model.Plaka =item["plate"].ToString();
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
    }
}