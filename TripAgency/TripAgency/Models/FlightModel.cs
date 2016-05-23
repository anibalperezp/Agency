using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class FlightModel
    {
        public List<Avion> Orig(string ori,string tdest,string tlleg )
        {
            var db = new TripAgencyEntities();
            var list = new List<Avion>();
            if (db.Avion.ToList().Count != 0)
            {
                foreach (var v1 in db.Avion.ToList())
                {
                    if (v1.OrigenVuelo.nombre.Equals(ori))
                    {
                        list.Add(v1);
                    }
                }
            }
            return list;
        }

        public string Min(int idVuelo, DateTime d)
        {
            var db = new TripAgencyEntities();
            List<NomTempAvionClase> list = db.NomTempAvionClase.ToList();
            double? min = 0;
            foreach (var v1 in list)
            {
                if (v1.Avion.idEmpresa == idVuelo && (v1.TemporadaAvion.inicio <= d && v1.TemporadaAvion.fin >= d))
                {
                    min = v1.tarifa;
                }
            }
            return min.ToString();
        }
    }
}