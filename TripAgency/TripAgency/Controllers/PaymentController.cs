using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TripAgency.EF;

namespace TripAgency.Controllers
{
    public class PaymentController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();
        // GET: Payment
        public ActionResult PaymentCar(int carM,string price,string d1,string d2)
        {
            try { 
            var ca = db.Carro.ToList().Find(c => c.idCarro == carM);
            return View(ca);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult PaymentHotel(int hotM, string price, string d1, string d2)
        {
            try { 
            var ho = db.Hotel.ToList().Find(c => c.idHotel == hotM);
            return View(ho);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult PaymentFlight(int flig, string priceF, string d1, string d2)
        {
            try { 
            var fl = db.Avion.ToList().Find(c => c.idVuelo == flig);
            return View(fl);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult PaymentPackage(int paqM, string priceE)
        {
            try { 
            var paq = db.Paquete.ToList().Find(c => c.idPaquete == paqM);
            return View(paq);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult PaymentTour(int excM, string priceE)
        {
            try { 
            var exc = db.Excursion.ToList().Find(c => c.idExcursion == excM);
            return View(exc);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult PaymentTransfer(Rental r)
        {
            try
            {
                var exc = db.Rental.ToList().Find(c => c.idRental == r.idRental);
                    return View(exc);
                }
                catch (Exception e1)
                {
                    string sms = e1.Message;
                    return View("Error");
                    throw;
                }
        }
    }
}