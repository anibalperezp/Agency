using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class TemporadaExc
    {
        public List<string> GetExcXEmpresa(string idEmpresa)
        {
            var db = new TripAgencyEntities();
            int e = Convert.ToInt32(idEmpresa);
            var listexc = new List<string>();
            if (db.Excursion.ToList().Count != 0)
            {
                foreach (var exc in db.Excursion.ToList())
                {
                    if (exc.idempresa == e)
                    {
                        listexc.Add(exc.nombre_excursion);
                    }
                }
            }
            return listexc;
        }

        public string Min(int idexc, DateTime d)
        {
            var db = new TripAgencyEntities();
            List<TemporadaExcursion> list = db.TemporadaExcursion.ToList();
            double? min = 0;
            foreach (var v1 in list)
            {
                if (v1.Excursion.idExcursion == idexc && (v1.inicio <= d && v1.fin >= d))
                {
                    min = v1.precio_tarifa;
                }
            }
            return min.ToString();
        }
    }
}