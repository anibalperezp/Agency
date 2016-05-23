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
    public class EstacionController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Estacion/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try { 
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Nombre" : "";
            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;
            var estacions = from estacion in db.Estacion select estacion;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                estacions = estacions.Where(estacion => estacion.estacion1.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    estacions = estacions.OrderByDescending(estacion => estacion.estacion1);
                    break;
                
                default:
                    estacions = estacions.OrderBy(estacion => estacion.estacion1);
                    break;
            }
            int Size_Of_Page = 6;
            int No_Of_Page = (Page_No ?? 1);
            return View(estacions.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Estacion/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacion estacion = db.Estacion.Find(id);
            if (estacion == null)
            {
                return HttpNotFound();
            }
            return View(estacion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Estacion/Create
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

        // POST: /Estacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idEstacion,estacion1,descripcioon_estacion")] Estacion estacion)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Estacion.Add(estacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estacion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Estacion/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacion estacion = db.Estacion.Find(id);
            if (estacion == null)
            {
                return HttpNotFound();
            }
            return View(estacion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Estacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idEstacion,estacion1,descripcioon_estacion")] Estacion estacion)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(estacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estacion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Estacion/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacion estacion = db.Estacion.Find(id);
            if (estacion == null)
            {
                return HttpNotFound();
            }
            return View(estacion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Estacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Estacion estacion = db.Estacion.Find(id);
            db.Estacion.Remove(estacion);
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
