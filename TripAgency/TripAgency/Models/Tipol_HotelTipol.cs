using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripAgency.Models
{
    public class Tipol_HotelTipoAsigned
    {
        public int idTipologia { get; set; }
        public string tipologia1 { get; set; }
        public bool assigned { get; set; }

        public string desc { get; set; }
    }
}