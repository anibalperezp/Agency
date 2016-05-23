using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripAgency.Models
{
    public class Tarifa_Rangos_Asigned
    {
        public int idRano { get; set; }
        public string rango { get; set; }
        public bool assigned { get; set; }
        public double tarifa { get; set; }
    }
}