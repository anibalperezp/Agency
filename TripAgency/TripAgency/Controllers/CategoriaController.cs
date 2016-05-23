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
    public class CategoriaController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Categoria/
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
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Nombre" : "";
            var cats = from cat in db.Categoria select cat;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cats = cats.Where(cat => cat.categoria1.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    cats = cats.OrderByDescending(cat => cat.categoria1);
                    break;
                default:
                    cats = cats.OrderBy(cat => cat.categoria1);
                    break;
            }
            int Size_Of_Page = 4;
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

        // GET: /Categoria/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Categoria/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
           
        }

        // POST: /Categoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idCategoria,categoria1")] Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Categoria.Add(categoria);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(categoria);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }

        }

        // GET: /Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Categoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idCategoria,categoria1")] Categoria categoria)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Categoria categoria = db.Categoria.Find(id);
            db.Categoria.Remove(categoria);
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
