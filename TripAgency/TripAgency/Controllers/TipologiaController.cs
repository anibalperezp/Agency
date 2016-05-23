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
    public class TipologiaController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Tipologia/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try { 
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingTipologia = String.IsNullOrEmpty(Sorting_Order) ? "Tipologia" : "";
            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;
            var tipos = from tipo in db.Tipologia select tipo;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                tipos = tipos.Where(tipo => tipo.tipologia1.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Tipologia":
                    tipos = tipos.OrderByDescending(tipo => tipo.tipologia1);
                    break;
                default:
                    tipos = tipos.OrderBy(tipo => tipo.tipologia1);
                    break;
            }
            int Size_Of_Page = 25;
            int No_Of_Page = (Page_No ?? 1);
            return View(tipos.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Tipologia/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipologia tipologia = db.Tipologia.Find(id);
            if (tipologia == null)
            {
                return HttpNotFound();
            }
            return View(tipologia);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Tipologia/Create
        public ActionResult Create()
        {
            try { 
            ViewBag.idTarificacion = new SelectList(db.Tarificacion, "idTarificacion", "nombre");
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Tipologia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTiologia,idTarificacion,tipologia1,descripcion")] Tipologia tipologia)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    db.Tipologia.Add(tipologia);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.idTarificacion = new SelectList(db.Tarificacion, "idTarificacion", "nombre", tipologia.idTarificacion);
                return View(tipologia);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Tipologia/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipologia tipologia = db.Tipologia.Find(id);
            if (tipologia == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTarificacion = new SelectList(db.Tarificacion, "idTarificacion", "nombre", tipologia.idTarificacion);
            return View(tipologia);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Tipologia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTiologia,tipologia1,idTarificacion,descripcion")] Tipologia tipologia)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(tipologia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTarificacion = new SelectList(db.Tarificacion, "idTarificacion", "nombre", tipologia.idTarificacion);
            return View(tipologia);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Tipologia/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipologia tipologia = db.Tipologia.Find(id);
            if (tipologia == null)
            {
                return HttpNotFound();
            }
            return View(tipologia);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Tipologia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Tipologia tipologia = db.Tipologia.Find(id);
            db.Tipologia.Remove(tipologia);
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
