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
    public class SubdestinoController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Subdestino/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try { 
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingDestino = String.IsNullOrEmpty(Sorting_Order) ? "Destino" : "";
            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;
            var subs = from sub in db.Subdestino select sub;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                subs = subs.Where(sub => sub.subdestino1.ToUpper().Contains(Search_Data.ToUpper()) ||
                    sub.Destino.destino1.ToUpper().Contains(Search_Data.ToUpper()) || sub.descripcion_subdestino.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Destino":
                    subs = subs.OrderByDescending(sub => sub.Destino.destino1);
                    break;
                default:
                    subs = subs.OrderBy(sub => sub.Destino.destino1);
                    break;
            }
            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(subs.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Subdestino/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subdestino subdestino = db.Subdestino.Find(id);
            if (subdestino == null)
            {
                return HttpNotFound();
            }
            return View(subdestino);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Subdestino/Create
        public ActionResult Create()
        {
            try { 
            ViewBag.iddestino = new SelectList(db.Destino, "idDestino", "destino1");
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Subdestino/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSubdestino,subdestino1,descripcion_subdestino,foto_sub1,foto_sub2,iddestino")] Subdestino subdestino, HttpPostedFileBase picture, HttpPostedFileBase picture2)
        {
            try { 
            string rutaimg = "";
            string rutaimg2 = "";
            if (picture == null)
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            else if (picture.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf" };
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
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/destination"), fileName);
                    picture.SaveAs(path);
                    rutaimg = "/TripAgency/Content/upload/destination/" + fileName;
                }
            }

            if (picture2 == null)
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            else if (picture2.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf" };
                if (!extensions.Contains(picture2.FileName.Substring(picture2.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                }
                else if (picture2.ContentLength > maxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                }
                else
                {
                    //TO:DO
                    var fileName = System.IO.Path.GetFileName(picture2.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/destination"), fileName);
                    picture2.SaveAs(path);
                    rutaimg2 = "/TripAgency/Content/upload/destination/" + fileName;
                }
            }

            if (ModelState.IsValid)
            {
                subdestino.foto_sub1 = rutaimg;
                subdestino.foto_sub2 = rutaimg2;
                db.Subdestino.Add(subdestino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.iddestino = new SelectList(db.Destino, "idDestino", "destino1", subdestino.iddestino);
            return View(subdestino);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Subdestino/Edit/5
        public ActionResult Edit(int? id)
        {

            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subdestino subdestino = db.Subdestino.Find(id);
            if (subdestino == null)
            {
                return HttpNotFound();
            }
            ViewBag.iddestino = new SelectList(db.Destino, "idDestino", "destino1", subdestino.iddestino);
            return View(subdestino);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Subdestino/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSubdestino,subdestino1,descripcion_subdestino,foto_sub1,foto_sub2,iddestino")] Subdestino subdestino, HttpPostedFileBase picture, HttpPostedFileBase picture2)
        {
            try { 
            string rutaimg = "";
            string rutaimg2 = "";
            var subtUpdate = db.Subdestino.ToList().FirstOrDefault(i => i.idSubdestino == subdestino.idSubdestino);
            if (picture == null)
            {
                if (subtUpdate.foto_sub1 != null)
                {
                    rutaimg = subtUpdate.foto_sub1;
                }
            }
            else if (picture.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf" };
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
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/destination"), fileName);
                    picture.SaveAs(path);
                    rutaimg = "/TripAgency/Content/upload/destination/" + fileName;
                }
            }

            if (picture2 == null)
            {
                if (subtUpdate.foto_sub2 != null)
                {
                    rutaimg2 = subtUpdate.foto_sub2;
                }
            }
            else if (picture2.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf" };
                if (!extensions.Contains(picture2.FileName.Substring(picture2.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                }
                else if (picture2.ContentLength > maxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                }
                else
                {
                    //TO:DO
                    var fileName = System.IO.Path.GetFileName(picture2.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/destination"), fileName);
                    picture2.SaveAs(path);
                    rutaimg2 = "/TripAgency/Content/upload/destination/" + fileName;
                }
            }

            if (ModelState.IsValid)
            {
                subtUpdate.foto_sub1 = rutaimg;
                subtUpdate.foto_sub2 = rutaimg2;
                subtUpdate.subdestino1 = subdestino.subdestino1;
                subtUpdate.descripcion_subdestino = subdestino.descripcion_subdestino;
                subtUpdate.iddestino = subdestino.iddestino;
                db.Entry(subtUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.iddestino = new SelectList(db.Destino, "idDestino", "destino1", subdestino.iddestino);
            return View(subdestino);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Subdestino/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subdestino subdestino = db.Subdestino.Find(id);
            if (subdestino == null)
            {
                return HttpNotFound();
            }
            return View(subdestino);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Subdestino/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public ActionResult DeleteConfirmed(int id)
         {
             try { 
             
         
         Subdestino subdestino = db.Subdestino.Find(id);
            db.Subdestino.Remove(subdestino);
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
