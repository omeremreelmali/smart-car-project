using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkıllıAraba.Models
{
    public class VehicleDetailModel
    {
        public string plate { get; set; }
        
        public string speed { get; set; }
        public string enlem { get; set; }
        public string boylam { get; set; }
        public string vehicle_id { get; set; }
        public string vehicle_state { get; set; }
        public string stop_state { get; set; }
        public string butonIslev { get; set; }
        public string butonRenk { get; set; }
        public string durumRenk { get; set; }
    }
}