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
    public class Tipo_TarifaController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Tipo_Tarifa/
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
            var cats = from cat in db.Tipo_Tarifa select cat;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cats = cats.Where(cat => cat.tipo_tarifa1.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    cats = cats.OrderByDescending(cat => cat.tipo_tarifa1);
                    break;
                default:
                    cats = cats.OrderBy(cat => cat.tipo_tarifa1);
                    break;
            }
            int Size_Of_Page = 15;
            int No_Of_Page = (Page_No ?? 1);
            return View(cats.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Tipo_Tarifa/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Tarifa tipo_tarifa = db.Tipo_Tarifa.Find(id);
            if (tipo_tarifa == null)
            {
                return HttpNotFound();
            }
            return View(tipo_tarifa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Tipo_Tarifa/Create
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

        // POST: /Tipo_Tarifa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idTipoTarifa,tipo_tarifa1")] Tipo_Tarifa tipo_tarifa)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Tipo_Tarifa.Add(tipo_tarifa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_tarifa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Tipo_Tarifa/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Tarifa tipo_tarifa = db.Tipo_Tarifa.Find(id);
            if (tipo_tarifa == null)
            {
                return HttpNotFound();
            }
            return View(tipo_tarifa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Tipo_Tarifa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idTipoTarifa,tipo_tarifa1")] Tipo_Tarifa tipo_tarifa)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(tipo_tarifa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_tarifa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Tipo_Tarifa/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Tarifa tipo_tarifa = db.Tipo_Tarifa.Find(id);
            if (tipo_tarifa == null)
            {
                return HttpNotFound();
            }
            return View(tipo_tarifa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Tipo_Tarifa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try {
            Tipo_Tarifa tipo_tarifa = db.Tipo_Tarifa.Find(id);
            db.Tipo_Tarifa.Remove(tipo_tarifa);
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
