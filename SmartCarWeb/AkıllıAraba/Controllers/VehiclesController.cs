using AkıllıAraba.Models;
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace AkıllıAraba.Controllers
{
  
    public class VehiclesController : Controller
    {
        MySqlConnection baglantı;
        MySqlDataAdapter adapter;

        public ActionResult Index(int page = 1)
        {
            int userID = Convert.ToInt32(Session["id"]);
            DataTable dt = new DataTable();
            baglantı = new MySqlConnection("Server=Localhost;Database=smart_car;Uid=root;Pwd=;charset=utf8;");
            adapter = new MySqlDataAdapter();
            MySqlCommand commond = new MySqlCommand();
            commond.Connection = baglantı;
            adapter.SelectCommand = commond;
            List<VehiclesViewModel> user = new List<VehiclesViewModel>();
            MySqlDataAdapter adapters=null;
            if (User.IsInRole("99"))
            {
                adapters = new MySqlDataAdapter("SELECT vehicle_locations.vehicle_state,vehicles.id,vehicles.plate,cities.city_name,colors.color_name,brands.brand_name,models.model_name,engine_model.type from vehicles JOIN users on users.id= vehicles.user_id join brands on brands.id = vehicles.brand join cities on vehicles.plate = cities.plate_code join engine_model on engine_model.id = vehicles.engine_model join models on vehicles.model = models.id join colors on colors.id = vehicles.color join vehicle_locations on vehicle_locations.vehicle_id = vehicles.id", baglantı);
            }
            else
            {
                adapters = new MySqlDataAdapter("SELECT vehicle_locations.vehicle_state, vehicles.id,vehicles.plate,cities.city_name,colors.color_name,brands.brand_name,models.model_name,engine_model.type from vehicles JOIN users on users.id= vehicles.user_id join brands on brands.id = vehicles.brand join cities on vehicles.plate = cities.plate_code join engine_model on engine_model.id = vehicles.engine_model join models on vehicles.model = models.id join colors on colors.id = vehicles.color join vehicle_locations on vehicle_locations.vehicle_id = vehicles.id WHERE vehicles.user_id= " + userID , baglantı);
            }
            adapters.Fill(dt);
            adapters.Dispose();
            baglantı.Close();
            foreach (DataRow item in dt.Rows)
            {
                VehiclesViewModel entity = new VehiclesViewModel
                {
                    vehicle_state= int.Parse(item.ItemArray[0].ToString()),
                    user_id = int.Parse(item.ItemArray[1].ToString()),
                    plate = item.ItemArray[2].ToString(),
                    city_code = item.ItemArray[3].ToString(),
                    color = item.ItemArray[4].ToString(),
                    brand = item.ItemArray[5].ToString(),
                    model = item.ItemArray[6].ToString(),
                    engine_model = item.ItemArray[7].ToString(),
                };
                user.Add(entity);
            }
            return View(user);
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
                HttpResponseMessage response = client.DeleteAsync($"api/car/{id}").Result;
                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("Index", "Vehicles");
                }
            }
            return View();
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
                HttpResponseMessage response = client.GetAsync($"api/car/{id}").Result;
                string result = response.Content.ReadAsStringAsync().Result;
                //dynamic jsonss = (List<VehiclesViewModel>)JsonConvert.DeserializeObject(result, typeof(List<VehiclesViewModel>));
                //dynamic json = JsonConvert.DeserializeObject(result);

                JObject Jsonparse = JObject.Parse(result);
                JArray json = (JArray)Jsonparse["data"];
                List<VehiclesViewModel> modelList = new List<VehiclesViewModel>();

                if (response.IsSuccessStatusCode == true)
                {
                    if (json != null)
                    {
                        foreach (var item in json)
                        {
                            var model = new VehiclesViewModel();
                            model.user_id = int.Parse((string)json[0]["id"].ToString());
                            model.name = (string)json[0]["name"].ToString();
                            model.surname = (string)json[0]["surname"].ToString();
                            model.model = (string)json[0]["brand_name"].ToString();
                            model.brand = (string)json[0]["model_name"].ToString();
                            model.engine_model = (string)json[0]["type"].ToString();
                            model.city_code = (string)json[0]["city_name"].ToString();
                            model.plate = (string)json[0]["plate"].ToString();
                            model.email = (string)json[0]["email"].ToString();
                            model.color = (string)json[0]["color_name"].ToString();
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

        static int vehicleID,stopStateValue; static string vehiclePlate, enlem,boylam;
        

        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.Client, Duration = 2)]
        public ActionResult VehicleDetail(int id,string plate)
        {
            VehicleDetailModel model = GetVehicleDetail(id);
            model.plate = plate;
            vehicleID = id; vehiclePlate = plate;
            return View(model);
        }

        [HttpGet]
        public PartialViewResult GetModelVehicle()
        {
            VehicleDetailModel model = GetVehicleDetail(vehicleID);
            model.plate = vehiclePlate;
            return PartialView("_DetailVehicle", model);
        }

      public ActionResult StopState()
        {
            StopCarModel stopModel = new StopCarModel();
            stopModel.id = vehicleID;
            stopModel.stop_state = 0;
            if (stopStateValue==0)
            {
                stopModel.stop_state = 1;
            }

            
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4500/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.PostAsJsonAsync("api/cardetail/stopcar/",stopModel).Result;
                if (response.IsSuccessStatusCode == true)
                {
                   
                }
               
            }

             VehicleDetailModel model = GetVehicleDetail(vehicleID);
             model.plate = vehiclePlate;
             return View("VehicleDetail",model);
        }


        public static VehicleDetailModel  GetVehicleDetail(int vehicle_id)
        {
            VehicleDetailModel DetailModel = new VehicleDetailModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4500/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.GetAsync($"api/cardetail/{vehicle_id}").Result;
                string result = response.Content.ReadAsStringAsync().Result;
                JObject Jsonparse = JObject.Parse(result);
                JArray json = (JArray)Jsonparse["data"];
                if (response.IsSuccessStatusCode == true)
                {
                    if (json != null)
                    {
                        DetailModel.speed = (string)json[0]["speed"].ToString();
                        VehiclesController.boylam = (string)json[0]["boylam"].ToString();
                        VehiclesController.enlem = (string)json[0]["enlem"].ToString();
                        DetailModel.enlem = (string)json[0]["enlem"].ToString();
                        DetailModel.boylam = (string)json[0]["boylam"].ToString();
                        stopStateValue = Convert.ToInt32((string)json[0]["stop_state"].ToString());
                        if (Convert.ToInt32((string)json[0]["stop_state"].ToString())==1)
                        {
                            DetailModel.stop_state = "Durdur";
                            DetailModel.butonRenk = "btn-danger";
                        }
                        else
                        {
                            DetailModel.stop_state = "Araç kilidini kaldır.";
                            DetailModel.butonRenk = "btn-success";
                        }

                        if (Convert.ToInt32((string)json[0]["vehicle_state"].ToString()) == 0)
                        {
                            DetailModel.vehicle_state = "Aracınızın durumu stabil";
                            DetailModel.durumRenk = "text-success";
                        }
                        else
                        {
                            DetailModel.vehicle_state = "Araç kaza yaptı.";
                            DetailModel.durumRenk = "text-danger";
                        }

                        DetailModel.vehicle_id = (string)json[0]["vehicle_id"].ToString();
                    }
                    
                }
                
            }

            return DetailModel;
        }

        public ActionResult Simulation(VehicleDetailModel Vmodel)
        {
            var entity = new VehicleDetailModel
            {
                speed = Vmodel.speed,
                stop_state = Vmodel.stop_state
            };
            if (Vmodel.stop_state == null)
            {
                Vmodel.stop_state = "0";
            }

            if (Vmodel.speed!=null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:4500/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    if (Vmodel.stop_state == "0")
                    {
                        //Simülasyon Calistirma
                        entity.stop_state = "1";
                        HttpResponseMessage response = client.PostAsJsonAsync("api/cardetail/simulation/", entity).Result;
                        if (response.IsSuccessStatusCode == true)
                        {
                            ViewBag.State = "1";
                            VehicleDetailModel model1 = GetVehicleDetail(vehicleID);
                            model1.plate = vehiclePlate;
                            return View("VehicleDetail", model1);
                        }
                    }
                    else if (Vmodel.stop_state == "1")
                    {
                        //Simülasyon Durdurma
                        entity.stop_state = "0";
                        HttpResponseMessage response = client.PostAsJsonAsync("api/cardetail/simulation/", entity).Result;
                        if (response.IsSuccessStatusCode == true)
                        {
                            ViewBag.State = "0";
                            VehicleDetailModel model1 = GetVehicleDetail(vehicleID);
                            model1.plate = vehiclePlate;
                            return View("VehicleDetail", model1);
                        }
                    }
                }
            }
            else
                ViewBag.Mesaj = "Hız alanını boş bırakmayın lütfen.";
            VehicleDetailModel model = GetVehicleDetail(vehicleID);
            model.plate = vehiclePlate;
            return View("VehicleDetail", model);


        }


        public ActionResult ResetSimulation()
        {
            var entity = new VehicleDetailModel
            {
                stop_state = "0"
            };

            using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:4500/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PostAsJsonAsync("api/cardetail/crashReset/", entity).Result;
                    if (response.IsSuccessStatusCode == true)
                    {
                        VehicleDetailModel models = GetVehicleDetail(vehicleID);
                        models.plate = vehiclePlate;
                        return View("VehicleDetail", models);
                    }

                }
            
            VehicleDetailModel model = GetVehicleDetail(vehicleID);
            model.plate = vehiclePlate;
            return View("VehicleDetail", model);


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