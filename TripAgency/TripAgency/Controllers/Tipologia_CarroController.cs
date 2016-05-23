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
    public class Tipologia_CarroController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Tipologia_Carro/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try { 
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingTipologia = String.IsNullOrEmpty(Sorting_Order) ? "Nombre" : "";
            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }
            ViewBag.FilterValue = Search_Data;
            var tipos = from tipo in db.Tipologia_Carro select tipo;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                tipos = tipos.Where(tipo => tipo.nombre.ToUpper().Contains(Search_Data.ToUpper()) || tipo.descripcion.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    tipos = tipos.OrderByDescending(tipo => tipo.nombre);
                    break;
                default:
                    tipos = tipos.OrderBy(tipo => tipo.nombre);
                    break;
            }
            int Size_Of_Page = 4;
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

        // GET: /Tipologia_Carro/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipologia_Carro tipologia_carro = db.Tipologia_Carro.Find(id);
            if (tipologia_carro == null)
            {
                return HttpNotFound();
            }
            return View(tipologia_carro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Tipologia_Carro/Create
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

        // POST: /Tipologia_Carro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idtipologia_carro,nombre,descripcion")] Tipologia_Carro tipologia_carro)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Tipologia_Carro.Add(tipologia_carro);
                db.SaveChanges();
                return RedirectToAction("Index");
            
            }

            return View(tipologia_carro);

            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Tipologia_Carro/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipologia_Carro tipologia_carro = db.Tipologia_Carro.Find(id);
            if (tipologia_carro == null)
            {
                return HttpNotFound();
            }
            return View(tipologia_carro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Tipologia_Carro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idtipologia_carro,nombre,descripcion")] Tipologia_Carro tipologia_carro)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(tipologia_carro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipologia_carro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Tipologia_Carro/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipologia_Carro tipologia_carro = db.Tipologia_Carro.Find(id);
            if (tipologia_carro == null)
            {
                return HttpNotFound();
            }
            return View(tipologia_carro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Tipologia_Carro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Tipologia_Carro tipologia_carro = db.Tipologia_Carro.Find(id);
            db.Tipologia_Carro.Remove(tipologia_carro);
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
