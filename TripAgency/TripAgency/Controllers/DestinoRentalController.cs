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
    public class DestinoRentalController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /DestinoRental/
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

            var rentas = from avion in db.DestinoRental select avion;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                rentas = rentas.Where(avion => avion.nombre.ToUpper().Contains(Search_Data.ToUpper())
                    || avion.descripcion.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    rentas = rentas.OrderByDescending(avion => avion.nombre);
                    break;
                default:
                    rentas = rentas.OrderBy(avion => avion.nombre);
                    break;
            }
            int Size_Of_Page = 9;
            int No_Of_Page = (Page_No ?? 1);
            return View(rentas.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /DestinoRental/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DestinoRental destinorental = db.DestinoRental.Find(id);
            if (destinorental == null)
            {
                return HttpNotFound();
            }
            return View(destinorental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /DestinoRental/Create
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

        // POST: /DestinoRental/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idDestinoRental,nombre,descripcion")] DestinoRental destinorental)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.DestinoRental.Add(destinorental);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
            return View(destinorental);
        }

        // GET: /DestinoRental/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DestinoRental destinorental = db.DestinoRental.Find(id);
            if (destinorental == null)
            {
                return HttpNotFound();
            }
            return View(destinorental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /DestinoRental/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idDestinoRental,nombre,descripcion")] DestinoRental destinorental)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(destinorental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(destinorental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /DestinoRental/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DestinoRental destinorental = db.DestinoRental.Find(id);
            if (destinorental == null)
            {
                return HttpNotFound();
            }
            return View(destinorental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }

        }

        // POST: /DestinoRental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            DestinoRental destinorental = db.DestinoRental.Find(id);
            db.DestinoRental.Remove(destinorental);
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
