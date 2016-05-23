using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class ScriptCalc
    {
        public string HotelPrice(string hotel, string tipology, string d1, string d2)
        {
            var db = new TripAgencyEntities();
            double? result = 0;
            var ds = Convert.ToDateTime(d1);
            var de = Convert.ToDateTime(d2);
            TimeSpan dif = de - ds;
            int days = dif.Days;
            int i = 0;

            while (i < days)
            {
                foreach (var v1 in db.Temporada_Hotel)
                {
                    if (v1.Hotel.nombre_hotel.Equals(hotel))
                    {
                        if (v1.inicio.Date < ds && v1.fin.Date > ds && v1.Anno.anno1.Equals(ds.Year.ToString()))
                        {
                            foreach (var v2 in v1.NomTempTipHotel.ToList())
                            {
                                if (v2.Tipologia.tipologia1 == tipology)
                                {
                                    result += v2.tarifa;
                                    if (ds.Day != 30 && ds.Day != 31)
                                    {
                                        var time = new DateTime(ds.Year, ds.Month, ds.Day + 1);
                                        ds = time;
                                        i++;
                                    }
                                    else
                                        if (ds.Day == 30 || ds.Day == 31)
                                        {
                                            var time = new DateTime(ds.Year, ds.Month + 1, 1);
                                            ds = time;
                                            i++;
                                        }
                                }
                            }
                        }
                    }
                }
                if (i == 0)
                {
                    return result.ToString();
                }
            }
            return result.ToString();
        }
    }
}