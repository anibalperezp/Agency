using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class SubModel
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        public List<string> GetSubXDestinos(string dest)
        {
            int idDestino = Convert.ToInt32(dest);
            var listcars = new List<string>();
            if (db.Subdestino.ToList().Count != 0)
            {
                foreach (var car1 in db.Subdestino.ToList())
                {
                    if (car1.iddestino == idDestino)
                    {
                        listcars.Add(car1.subdestino1);
                    }
                }
            }
            return listcars;
        }

        public List<string> GetSubXDestinosN(string dest)
        {
            var listcars = new List<string>();
            var sub = db.Destino.FirstOrDefault(c => c.destino1 == dest);
            if (sub != null)
            {
                if (db.Subdestino.ToList().Count != 0)
                {
                    listcars.AddRange(from car1 in db.Subdestino.ToList() where car1.iddestino == sub.idDestino select car1.subdestino1);
                }
            }
            return listcars;
        }

        public List<string> GetSubHotels(string dest)
        {
            var listcars = new List<string>();
            int sub = int.Parse(dest);
            if (dest != null)
            {
                if (db.Hotel.ToList().Count != 0)
                {
                    listcars.AddRange(from car1 in db.Hotel.ToList() where car1.idDestino == sub select car1.nombre_hotel);
                }
            }
            return listcars;
        }

        public List<string> GetSubXModeles(string cat)
        {
            var listcars = new List<string>();
            var sub = db.Tipologia_Carro.FirstOrDefault(c => c.nombre == cat);
            if (sub != null)
            {
                if (db.Subdestino.ToList().Count != 0)
                {
                    listcars.AddRange(from car1 in db.Carro.ToList() where car1.idtipologia_carro == sub.idtipologia_carro select car1.name);
                }
            }
            return listcars;
        }

        public List<string> GetSubXExc(string sub1)
        {
            var listexc = new List<string>();
            var sub = db.Destino.FirstOrDefault(c => c.destino1 == sub1);
            if (sub != null)
            {
                if (db.Destino.ToList().Count != 0)
                {
                    listexc.AddRange(from car1 in db.Excursion.ToList() where car1.idDestino == sub.idDestino select car1.nombre_excursion);
                }
            }
            return listexc;
        }
        
    }
}