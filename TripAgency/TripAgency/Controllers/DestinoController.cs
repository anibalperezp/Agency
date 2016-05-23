using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TripAgency.EF;
using TripAgency.Models;

namespace TripAgency.Controllers
{
     [Authorize] 
    public class DestinoController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Detino/
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

            var destinos = from destino in db.Destino select destino;

            if (!String.IsNullOrEmpty(Search_Data))
            {
                destinos = destinos.Where(destino => destino.destino1.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    destinos = destinos.OrderByDescending(destino => destino.destino1);
                    break;

                default:
                    destinos = destinos.OrderBy(destino => destino.destino1);
                    break;
            }
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(destinos.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Detino/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destino destino = db.Destino.Find(id);
            if (destino == null)
            {
                return HttpNotFound();
            }
            return View(destino);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Detino/Create
        public ActionResult Create()
        {
            try { 
            ViewBag.idpais = new SelectList(db.Pai, "idPais", "pais");
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Detino/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDestino,destino1,decripcion_destino,foto_destino1,foto_destino2,idpais")] Destino destino, HttpPostedFileBase picture, HttpPostedFileBase picture2)
        {
            try { 
            var strMappath = "/TripAgency/TripAgency/Content/Upload/Destinations/" + destino.destino1;
            DirectoryInfo di = null;
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
                    string fileName = Path.GetFileName(picture.FileName);
                    var exten = picture.FileName.Substring(picture.FileName.LastIndexOf('.'));
                    if (Directory.Exists(strMappath))
                    {
                        di = Directory.GetParent(strMappath);
                        var acces = di.GetAccessControl(AccessControlSections.Access);
                    }
                    if (!Directory.Exists(strMappath))
                    {
                        di = Directory.CreateDirectory(strMappath);
                        var acces = di.GetAccessControl(AccessControlSections.Access);
                    }
                    var file = strMappath + "//" + fileName;
                    di = Directory.GetParent(file);
                    var name_file = di.FullName + "\\" + fileName;
                    var path = Directory.GetFiles(strMappath);
                    var fullPath = strMappath + picture.FileName;

                    foreach (var p in path.ToList())
                    {
                        if (p.Equals(fullPath))
                        {
                            ModelState.AddModelError("", "El archivo " + picture.FileName + " " + "ya existe");
                            return View(destino);
                        }
                    }
                    var destinationPath = Path.Combine(Server.MapPath(di.Name), fileName);
                    picture.SaveAs(name_file);
                    var ddd = new Util();
                    rutaimg = ddd.name(name_file);
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
                    string fileName = Path.GetFileName(picture2.FileName);
                    var exten = picture2.FileName.Substring(picture2.FileName.LastIndexOf('.'));
                    if (Directory.Exists(strMappath))
                    {
                        di = Directory.GetParent(strMappath);
                        var acces = di.GetAccessControl(AccessControlSections.Access);
                    }
                    if (!Directory.Exists(strMappath))
                    {
                        di = Directory.CreateDirectory(strMappath);
                        var acces = di.GetAccessControl(AccessControlSections.Access);
                    }
                    var file = strMappath + "//" + fileName;
                    di = Directory.GetParent(file);
                    var name_file2 = di.FullName + "\\" + fileName;
                    var path = Directory.GetFiles(strMappath);
                    var fullPath = strMappath + picture2.FileName;

                    foreach (var p in path.ToList())
                    {
                        if (p.Equals(fullPath))
                        {
                            ModelState.AddModelError("", "El archivo " + picture2.FileName + " " + "ya existe");
                            return View(destino);
                        }
                    }
                    var destinationPath = Path.Combine(Server.MapPath(di.Name), fileName);
                    picture2.SaveAs(name_file2);
                    var ddd = new Util();
                    rutaimg2 = ddd.name(name_file2);
                }
            }

            if (ModelState.IsValid)
            {
                destino.foto_destino1 = rutaimg;
                destino.foto_destino2 = rutaimg2;
                db.Destino.Add(destino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idpais = new SelectList(db.Pai, "idPais", "pais", destino.idpais);
            return View(destino);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Detino/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destino destino = db.Destino.Find(id);
            if (destino == null)
            {
                return HttpNotFound();
            }
            ViewBag.idpais = new SelectList(db.Pai, "idPais", "pais", destino.idpais);
            return View(destino);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Detino/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDestino,destino1,decripcion_destino,foto_destino1,foto_destino2,idpais")] Destino destino, HttpPostedFileBase picture, HttpPostedFileBase picture2)
        {
            try { 
            string rutaimg = "";
            string rutaimg2 = "";
            var destUpdate = db.Destino.ToList().FirstOrDefault(i => i.idDestino == destino.idDestino);
            if (picture == null)
            {
                if (destUpdate.foto_destino1 != null)
                {
                    rutaimg = destUpdate.foto_destino1;
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
                if (destUpdate.foto_destino2 != null)
                {
                    rutaimg2 = destUpdate.foto_destino2;
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
                destUpdate.foto_destino1 = rutaimg;
                destUpdate.foto_destino2 = rutaimg2;
                destUpdate.destino1 = destino.destino1;
                destUpdate.decripcion_destino = destino.decripcion_destino;
                destUpdate.idpais = destino.idpais;
                db.Entry(destUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idpais = new SelectList(db.Pai, "idPais", "pais", destino.idpais);
            return View(destino);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Detino/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destino destino = db.Destino.Find(id);
            if (destino == null)
            {
                return HttpNotFound();
            }
            return View(destino);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Detino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Destino destino = db.Destino.Find(id);
            db.Destino.Remove(destino);
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
