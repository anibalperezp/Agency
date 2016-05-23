using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class HotelModel
    {
        public int idHotel { get; set; }
        public string hotel1 { get; set; }

        public string Min(int idhotel, DateTime d)
        {   
            var db = new TripAgencyEntities();
            List<Temporada_Hotel> list = db.Temporada_Hotel.ToList();
            double? min = 0;
            foreach (var v1 in list)
            {
                if (v1.idhotel==idhotel && (v1.inicio <= d && v1.fin >= d))
                {
                    List<NomTempTipHotel> listar = v1.NomTempTipHotel.ToList();
                    if (listar.Count != 0)
                    {
                        min = listar[0].tarifa;
                        for (int i = 1; i < listar.Count; i++)
                        {
                            if (listar[i].tarifa <= min)
                            {
                                min = listar[i].tarifa;
                            }
                        }
                    }
                }
            }
            return min.ToString();
        }
    }
}