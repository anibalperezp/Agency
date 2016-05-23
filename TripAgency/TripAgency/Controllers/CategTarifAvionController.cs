using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TripAgency.EF;

namespace TripAgency.Controllers
{
     [Authorize] 
    public class CategTarifAvionController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /CategTarifAvion/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try { 
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingNombre = String.IsNullOrEmpty(Sorting_Order) ? "Codigo" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var cadenas = from cadena in db.CategTarifAvion select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cadenas = cadenas.Where(cadena => cadena.codigocat.ToUpper().Contains(Search_Data.ToUpper()) || cadena.descripcion.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Codigo":
                    cadenas = cadenas.OrderByDescending(cadena => cadena.codigocat);
                    break;
                default:
                    cadenas = cadenas.OrderBy(cadena => cadena.codigocat);
                    break;
            }
            int Size_Of_Page = 6;
            int No_Of_Page = (Page_No ?? 1);
            return View(cadenas.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /CategTarifAvion/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategTarifAvion categtarifavion = db.CategTarifAvion.Find(id);
            if (categtarifavion == null)
            {
                return HttpNotFound();
            }
            return View(categtarifavion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /CategTarifAvion/Create
        public ActionResult Create()
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

        // POST: /CategTarifAvion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Create(
             [Bind(Include = "idCategTarifAvion,codigocat,descripcion,oneway,roundtrip")] CategTarifAvion
                 categtarifavion)
         {
             try
             {
                 if (ModelState.IsValid)
                 {
                     db.CategTarifAvion.Add(categtarifavion);
                     db.SaveChanges();
                     return RedirectToAction("Index");
                 }

                 return View(categtarifavion);
             }
             catch (Exception e1)
             {
                 string sms = e1.Message;
                 return View("Error");
                 throw;
             }
         }

         // GET: /CategTarifAvion/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategTarifAvion categtarifavion = db.CategTarifAvion.Find(id);
            if (categtarifavion == null)
            {
                return HttpNotFound();
            }
            return View(categtarifavion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /CategTarifAvion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idCategTarifAvion,codigocat,descripcion,oneway,roundtrip")] CategTarifAvion categtarifavion)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(categtarifavion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categtarifavion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /CategTarifAvion/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategTarifAvion categtarifavion = db.CategTarifAvion.Find(id);
            if (categtarifavion == null)
            {
                return HttpNotFound();
            }
            return View(categtarifavion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /CategTarifAvion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            CategTarifAvion categtarifavion = db.CategTarifAvion.Find(id);
            db.CategTarifAvion.Remove(categtarifavion);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
