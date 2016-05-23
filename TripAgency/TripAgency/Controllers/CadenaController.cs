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
    public class CadenaController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Cadena/
        public ActionResult Index(string Sorting_Order, string Search_Data ,string Filter_Value, int? Page_No)
        {
            try
            {
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

                var cadenas = from cadena in db.Cadena select cadena;
                if (!String.IsNullOrEmpty(Search_Data))
                {
                    cadenas = cadenas.Where(cadena => cadena.cadena_.ToUpper().Contains(Search_Data.ToUpper()));
                }
                switch (Sorting_Order)
                {
                    case "Nombre":
                        cadenas = cadenas.OrderByDescending(cadena => cadena.cadena_);
                        break;
                    default:
                        cadenas = cadenas.OrderBy(cadena => cadena.cadena_);
                        break;
                }
                int Size_Of_Page = 25;
                int No_Of_Page = (Page_No ?? 1);
                return View(cadenas.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Cadena/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cadena cadena = db.Cadena.Find(id);
                if (cadena == null)
                {
                    return HttpNotFound();
                }
                return View(cadena);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Cadena/Create
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

        // POST: /Cadena/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCadena,cadena_,logo_cadena,descrpcion_cadena")] Cadena cadena, HttpPostedFileBase picture)
        {
            try { 

            var strMappath = "/TripAgency/TripAgency/Content/Upload/Brand/" + cadena.cadena_;
            DirectoryInfo di = null;
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
                            ModelState.AddModelError("","El archivo " + picture.FileName + " " + "ya existe");
                            return View(cadena);
                        }
                    }
                    var destinationPath = Path.Combine(Server.MapPath(di.Name), fileName);
                    picture.SaveAs(name_file);
                    var ddd = new Util();
                    rutaimg = ddd.name(name_file);
                }
            }

            if (ModelState.IsValid)
            {
                cadena.logo_cadena = rutaimg;
                db.Cadena.Add(cadena);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cadena);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Cadena/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadena cadena = db.Cadena.Find(id);
            if (cadena == null)
            {
                return HttpNotFound();
            }
            return View(cadena);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Cadena/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? idCadena, [Bind(Include = "cadena_,logo_cadena,descrpcion_cadena")] Cadena cadena, HttpPostedFileBase picture)
        {
            try { 
            var cadenaUpdate = db.Cadena.ToList().FirstOrDefault(i => i.idCadena == idCadena);
            string rutaimg = "";
            if (picture == null)
            {
                if (cadenaUpdate.logo_cadena != null)
                {
                    rutaimg = cadenaUpdate.logo_cadena;
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
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/brand"), fileName);
                    picture.SaveAs(path);
                    rutaimg = "/TripAgency/Content/upload/brand/" + fileName;
                }
            }
            if (ModelState.IsValid)
            {
                cadenaUpdate.cadena_ = cadena.cadena_;
                cadenaUpdate.descrpcion_cadena = cadena.descrpcion_cadena;
                cadenaUpdate.logo_cadena = rutaimg;
                db.Entry(cadenaUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cadena);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Cadena/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadena cadena = db.Cadena.Find(id);
            if (cadena == null)
            {
                return HttpNotFound();
            }
            return View(cadena);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Cadena/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Cadena cadena = db.Cadena.Find(id);
                db.Cadena.Remove(cadena);
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
