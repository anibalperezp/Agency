using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TripAgency.EF;

namespace TripAgency.Controllers
{
    [Authorize] 
    public class AnnoController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();
        // GET: /Anno/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try
            {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Año" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var annos = from anno in db.Anno select anno;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                annos = annos.Where(anno => anno.anno1.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Año":
                    annos = annos.OrderByDescending(anno => anno.anno1);
                    break;
                default:
                    annos = annos.OrderBy(anno => anno.anno1);
                    break;
            }

            int Size_Of_Page = 22;
            int No_Of_Page = (Page_No ?? 1);
            return View(annos.ToPagedList(No_Of_Page, Size_Of_Page));
        }
         catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
    }

        // GET: /Anno/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Anno anno = db.Anno.Find(id);
                if (anno == null)
                {
                    return HttpNotFound();
                }
                return View(anno);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Anno/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.old = db.Anno.ToList().Last().anno1;
                return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Anno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="IdAnno,anno1")] Anno anno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Anno.Add(anno);
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
           

            return View(anno);
        }

        // GET: /Anno/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Anno anno = db.Anno.Find(id);
                if (anno == null)
                {
                    return HttpNotFound();
                }
                return View(anno);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
           
        }

        // POST: /Anno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="IdAnno,anno1")] Anno anno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(anno).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(anno);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
          
        }

        // GET: /Anno/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Anno anno = db.Anno.Find(id);
                if (anno == null)
                {
                    return HttpNotFound();
                }
                return View(anno);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
          
           
        }

        // POST: /Anno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Anno anno = db.Anno.Find(id);
                db.Anno.Remove(anno);
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
