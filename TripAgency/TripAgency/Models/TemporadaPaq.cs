using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class TemporadaPaq
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        public List<string> GetPaqXEmpresa(string idEmpresa)
        {
            int Empresa = Convert.ToInt32(idEmpresa);
            var listexc = new List<string>();
            if (db.Excursion.ToList().Count != 0)
            {
                foreach (var exc in db.Paquete.ToList())
                {
                    if (exc.idEmpresa == Empresa)
                    {
                        listexc.Add(exc.nombre_paquete);
                    }
                }
            }
            return listexc;
        }

        public string Min(int idPaquete, DateTime d)
        {
            var db = new TripAgencyEntities();
            List<TemporadaPaquete> list = db.TemporadaPaquete.ToList();
            double? min = 0;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].idPaquete == idPaquete && (list[i].inicio <= d && list[i].fin >= d))
                {
                    min = list[i].precio_tarifa;
                }
            }
            return min.ToString();
        }
    }
}