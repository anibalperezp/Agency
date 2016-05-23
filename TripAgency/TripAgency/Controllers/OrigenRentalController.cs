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
    public class OrigenRentalController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /OrigenRental/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try { 
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingNombre = String.IsNullOrEmpty(Sorting_Order) ? "Nombre" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var origens = from avion in db.OrigenRental select avion;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                origens = origens.Where(avion => avion.nombre.ToUpper().Contains(Search_Data.ToUpper())
                    || avion.descripcion.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    origens = origens.OrderByDescending(avion => avion.nombre);
                    break;
                default:
                    origens = origens.OrderBy(avion => avion.nombre);
                    break;
            }
            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(origens.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /OrigenRental/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrigenRental origenrental = db.OrigenRental.Find(id);
            if (origenrental == null)
            {
                return HttpNotFound();
            }
            return View(origenrental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /OrigenRental/Create
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

        // POST: /OrigenRental/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idOrigenRental,nombre,descripcion")] OrigenRental origenrental)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.OrigenRental.Add(origenrental);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(origenrental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /OrigenRental/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrigenRental origenrental = db.OrigenRental.Find(id);
            if (origenrental == null)
            {
                return HttpNotFound();
            }
            return View(origenrental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /OrigenRental/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idOrigenRental,nombre,descripcion")] OrigenRental origenrental)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(origenrental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(origenrental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /OrigenRental/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrigenRental origenrental = db.OrigenRental.Find(id);
            if (origenrental == null)
            {
                return HttpNotFound();
            }
            return View(origenrental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /OrigenRental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            OrigenRental origenrental = db.OrigenRental.Find(id);
            db.OrigenRental.Remove(origenrental);
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
