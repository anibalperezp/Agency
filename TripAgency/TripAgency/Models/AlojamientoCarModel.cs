using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class AlojamientoCarModel
    {
        public string oficini { get; set; }
        public string oficfin { get; set; }
        public string fechaini { get; set; }
        public string fechafin { get; set; }

        public AlojamientoCarModel(string poficini, string poficfin, string pfechaini, string fechafin)
        {
            this.oficini = poficini;
            this.oficfin = poficfin;
            this.fechaini = pfechaini;
            this.fechafin = fechafin;
        }

        public int catDias()
        {
            int result = 0;
            DateTime inic = Convert.ToDateTime(fechaini);
            DateTime fin = Convert.ToDateTime(fechafin);
            TimeSpan dif = fin - inic;
            result = dif.Days;
            return result;
        }

        public DateTime fechD1()
        {
            DateTime inic = Convert.ToDateTime(fechaini);
            return inic;
        }

        public DateTime fechD2()
        {
            DateTime fin = Convert.ToDateTime(fechafin);
            return fin;
        }

    }
}