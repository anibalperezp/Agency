using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class TransferModel
    {
        public string Min(int idtransfer, DateTime d)
        {
            var db = new TripAgencyEntities();
            List<TemporadaRental> list = db.TemporadaRental.ToList();
            double? min = 0;
            foreach (var v1 in list)
            {
                if (v1.Rental.idRental == idtransfer && (v1.inicio <= d && v1.fin >= d))
                {
                    min = v1.precio_tarifa;
                }
            }
            return min.ToString();
        }
    }
}