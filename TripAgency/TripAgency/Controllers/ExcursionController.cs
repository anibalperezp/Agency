using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TripAgency.EF;
using TripAgency.Models;

namespace TripAgency.Controllers
{
     [Authorize] 
    public class ExcursionController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Excursion/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try { 
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Empresa" : "";
            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;
            var cats = from cat in db.Excursion select cat;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cats = cats.Where(cat => cat.nombre_excursion.ToUpper().Contains(Search_Data.ToUpper()) ||
                    cat.descrpcion_excursion.ToUpper().Contains(Search_Data.ToUpper()) || cat.Empresa.nombre_empresa.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Empresa":
                    cats = cats.OrderByDescending(cat => cat.Empresa.nombre_empresa);
                    break;
                default:
                    cats = cats.OrderBy(cat => cat.Empresa.nombre_empresa);
                    break;
            }
            int Size_Of_Page = 12;
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

        public JsonResult subdestinos(string idDestino)
        {
            
            List<string> carsList = new SubModel().GetSubXDestinos(idDestino);
            return Json(carsList, JsonRequestBehavior.AllowGet);
         
        }

        // GET: /Excursion/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Excursion excursion = db.Excursion.Find(id);
            if (excursion == null)
            {
                return HttpNotFound();
            }
            return View(excursion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Excursion/Create
        public ActionResult Create()
        {
            try { 
            ViewBag.idempresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1");
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Excursion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idExcursion,nombre_excursion,descrpcion_excursion,idDestino,idempresa")] Excursion excursion, HttpPostedFileBase picture, HttpPostedFileBase picture2, string State)
        {
            try { 

            var strMappath = "/TripAgency/TripAgency/Content/Upload/Excursion/" + excursion.nombre_excursion;
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
                    string fileName;
                    fileName = Path.GetFileName(picture.FileName);
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
                    var name_file = di.FullName + "\\" + fileName;

                    var path = Directory.GetFiles(strMappath);
                    var fullPath = strMappath + picture.FileName;

                    foreach (var p in path.ToList())
                    {
                        if (p.Equals(fullPath))
                        {
                            ModelState.AddModelError("",
                                "El archivo " + picture.FileName + " " + "ya existe");
                            return View(excursion);
                        }
                    }
                    var destinationPath = Path.Combine(Server.MapPath(di.Name), fileName);
                    picture.SaveAs(name_file);
                    Util ddd = new Util();
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
                    string fileName = Path.GetFileName(picture2.FileName);
                    var exten = picture2.FileName.Substring(picture2.FileName.LastIndexOf('.'));
                    if (Directory.Exists(strMappath))
                    {
                        di = Directory.GetParent(strMappath);
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
                            return View(excursion);
                        }
                    }
                    var destinationPath = Path.Combine(Server.MapPath(di.Name), fileName);
                    picture2.SaveAs(name_file2);
                    Util ddd = new Util();
                    rutaimg2 = ddd.name(name_file2);
                }
            }

            if (ModelState.IsValid)
            {
                excursion.foto1 = rutaimg;
                excursion.foto2 = rutaimg2;
                db.Excursion.Add(excursion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idempresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", excursion.idempresa);
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1");
            return View(excursion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Excursion/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Excursion excursion = db.Excursion.Find(id);
            if (excursion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idempresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", excursion.idempresa);
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1");
            return View(excursion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Excursion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idExcursion,nombre_excursion,descrpcion_excursion,idDestino,idempresa")] Excursion excursion, HttpPostedFileBase picture, HttpPostedFileBase picture2, string State)
        {
            try { 
            string rutaimg = "";
            string rutaimg2 = "";
            var destUpdate = db.Excursion.ToList().FirstOrDefault(i => i.idExcursion == excursion.idExcursion);
            if (picture == null)
            {
                if (destUpdate.foto1 != null)
                {
                    rutaimg = destUpdate.foto1;
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
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/excursion"), fileName);
                    picture.SaveAs(path);
                    rutaimg = "/TripAgency/Content/upload/excursion/" + fileName;
                }
            }

            if (picture2 == null)
            {
                if (destUpdate.foto2 != null)
                {
                    rutaimg2 = destUpdate.foto2;
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
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/excursion"), fileName);
                    picture2.SaveAs(path);
                    rutaimg2 = "/TripAgency/Content/upload/excursion/" + fileName;
                }
            }
            if (ModelState.IsValid)
            {
                destUpdate.foto1 = rutaimg;
                destUpdate.foto2 = rutaimg2;
                destUpdate.descrpcion_excursion = excursion.descrpcion_excursion;
                destUpdate.idDestino = excursion.idDestino;
                destUpdate.nombre_excursion = excursion.nombre_excursion;
                destUpdate.idempresa = excursion.idempresa;
                destUpdate.idDestino = excursion.idDestino;
                db.Entry(destUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idempresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", excursion.idempresa);
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1");
            return View(excursion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Excursion/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Excursion excursion = db.Excursion.Find(id);
            if (excursion == null)
            {
                return HttpNotFound();
            }
            return View(excursion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Excursion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Excursion excursion = db.Excursion.Find(id);
            db.Excursion.Remove(excursion);
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
