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
    public class PaisController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Pais/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try { 
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Pais" : "";
            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;
            var paiss = from pais in db.Pai select pais;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                paiss = paiss.Where(pais => pais.pais.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Pais":
                    paiss = paiss.OrderByDescending(pais => pais.pais);
                    break;
                default:
                    paiss = paiss.OrderBy(pais => pais.pais);
                    break;
            }
            int Size_Of_Page = 15;
            int No_Of_Page = (Page_No ?? 1);
            return View(paiss.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Pais/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pai pai = db.Pai.Find(id);
            if (pai == null)
            {
                return HttpNotFound();
            }
            return View(pai);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Pais/Create
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

        // POST: /Pais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idPais,pais")] Pai pai,HttpPostedFileBase picture)
        {
            try { 
            string rutaimg = "";
            if (picture == null)
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            else if (picture.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
                if (!extensions.Contains(picture.FileName.Substring(picture.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                }
                else if (picture.ContentLength > maxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                }
                else
                {
                    //TO:DO
                    var fileName = System.IO.Path.GetFileName(picture.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/pais"), fileName);
                    picture.SaveAs(path);
                    rutaimg = "/TripAgency/Content/upload/pais/" + fileName;
                }
            }

            if (ModelState.IsValid)
            {
                pai.foto = rutaimg;
                db.Pai.Add(pai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pai);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Pais/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pai pai = db.Pai.Find(id);
            if (pai == null)
            {
                return HttpNotFound();
            }
            return View(pai);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Pais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPais,pais")] Pai pai, HttpPostedFileBase picture)
        {
            try { 
            var cadenaUpdate = db.Pai.ToList().FirstOrDefault(i => i.idPais == pai.idPais);
            string rutaimg = "";
            if (picture == null)
            {
                if (cadenaUpdate.foto != null)
                {
                    rutaimg = cadenaUpdate.foto;
                }
            }
            else if (picture.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
                if (!extensions.Contains(picture.FileName.Substring(picture.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                }
                else if (picture.ContentLength > maxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                }
                else
                {
                    //TO:DO
                    var fileName = System.IO.Path.GetFileName(picture.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/pais"), fileName);
                    picture.SaveAs(path);
                    rutaimg = "/TripAgency/Content/upload/pais/" + fileName;
                }
            }
            if (ModelState.IsValid)
            {
                cadenaUpdate.pais = pai.pais;
                cadenaUpdate.foto = rutaimg;
                db.Entry(cadenaUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pai);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Pais/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pai pai = db.Pai.Find(id);
            if (pai == null)
            {
                return HttpNotFound();
            }
            return View(pai);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Pai pai = db.Pai.Find(id);
            db.Pai.Remove(pai);
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
