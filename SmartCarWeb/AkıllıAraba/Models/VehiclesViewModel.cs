using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AkıllıAraba.Models
{
    public class VehiclesViewModel
    {
        public int user_id { get; set; }
        [Required(ErrorMessage ="Ad kısmı Boş Kalamaz")]
        [MaxLength(50)]
        public string name { get; set; }
        [Required(ErrorMessage = "Soyad kısmı Boş Kalamaz")]
        [MaxLength(50)]
        public string surname { get; set; }
        [EmailAddress(ErrorMessage = "Geçerli Email Girin")]
        [Required(ErrorMessage = "Email Girin.")]
        public string email { get; set; }
        [Required(ErrorMessage = "Kod Boş Kalamaz")]
        [MinLength(6,ErrorMessage ="Code minimun 6 olacak")]
        public string code { get; set; }
        [Required(ErrorMessage = "Plaka Girin")]
        public string plate { get; set; }
        [Required(ErrorMessage = "Şehit Seçin")]
        public string plate_code { get; set; }
        public string city_code { get; set; }
        [Required(ErrorMessage = "Yakıt Türünü Seçin")]
        public string engine_model { get; set; }
        [Required(ErrorMessage = "Renk Seçin")]
        public string color{ get; set; }
        [Required(ErrorMessage = "Kod Boş Kalamaz")]
        public string brand{ get; set; }
        [Required(ErrorMessage = "Kod Boş Kalamaz")]
        public string model{ get; set; }
        /*[Required(ErrorMessage = "Kod Boş Kalamaz")]
        public string engine_model { get; set; }*/
        [Required(ErrorMessage = "Marka Seçiniz")]
        public string VehicleBrandList { get; set; }
        [Required(ErrorMessage = "Model Seçiniz")]
        public string VehicleModelList { get; set; }
        public int vehicle_state { get; set; }


    }
}