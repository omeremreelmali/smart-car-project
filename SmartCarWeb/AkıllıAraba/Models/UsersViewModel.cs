using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AkıllıAraba.Models
{
    public class UsersViewModel
    {
        public int id { get; set; }
        [MaxLength(11, ErrorMessage = "Kimlik numarası 11 haneli olmalıdır"), MinLength(11, ErrorMessage = "Kimlik numarası 11 haneli olmalıdır")]
        public string tc { get; set; }
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
        public string CityName { get; set; }
        public string Marka { get; set; }
        public string Plaka { get; set; }
    }
}