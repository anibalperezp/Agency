using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TripAgency.Controllers
{
    [Authorize] 
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try { 
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult About()
        {
            try { 
            ViewBag.Message = "Your application description page.";

            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult Contact()
        {
            try { 
            ViewBag.Message = "Your contact page.";

            return View();
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