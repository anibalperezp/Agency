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
    public class RentalController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Rental/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try { 
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingNombre = String.IsNullOrEmpty(Sorting_Order) ? "Empresa" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var cadenas = from cadena in db.Rental select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cadenas = cadenas.Where(cadena => cadena.nombre.ToUpper().Contains(Search_Data.ToUpper()) ||
                    cadena.descripcion.ToUpper().Contains(Search_Data.ToUpper()) || cadena.DestinoRental.nombre.ToUpper().Contains(Search_Data.ToUpper()) ||
                    cadena.OrigenRental.nombre.ToUpper().Contains(Search_Data.ToUpper()) || cadena.Empresa.nombre_empresa.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Empresa":
                    cadenas = cadenas.OrderByDescending(cadena => cadena.Empresa.nombre_empresa);
                    break;
                case "Nombre":
                    cadenas = cadenas.OrderByDescending(cadena => cadena.nombre);
                    break;
                default:
                    cadenas = cadenas.OrderBy(cadena => cadena.Empresa.nombre_empresa);
                    break;
            }
            int Size_Of_Page = 4;
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

        // GET: /Rental/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rental.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Rental/Create
        public ActionResult Create()
        {
            try { 
            ViewBag.idOrigenRental = new SelectList(db.OrigenRental, "idOrigenRental", "nombre");
            ViewBag.idDestinoRental = new SelectList(db.DestinoRental, "idDestinoRental", "nombre");
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Rental/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmpresa,idRental,nombre,foto,descripcion,idDestinoRental,idOrigenRental")] Rental rental, HttpPostedFileBase picture)
        {
            try { 
            var strMappath = "/TripAgency/TripAgency/Content/Upload/Rental/" + rental.nombre;
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
                            ModelState.AddModelError("", "El archivo " + picture.FileName + " " + "ya existe");
                            return View(rental);
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
                rental.foto = rutaimg;
                db.Rental.Add(rental);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", rental.idEmpresa);
            ViewBag.idOrigenRental = new SelectList(db.OrigenRental, "idOrigenRental", "nombre", rental.idOrigenRental);
            ViewBag.idDestinoRental = new SelectList(db.DestinoRental, "idDestinoRental", "nombre", rental.idDestinoRental);
            return View(rental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Rental/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rental.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", rental.idEmpresa);
            ViewBag.idOrigenRental = new SelectList(db.OrigenRental, "idOrigenRental", "nombre", rental.idOrigenRental);
            ViewBag.idDestinoRental = new SelectList(db.DestinoRental, "idDestinoRental", "nombre", rental.idDestinoRental);
            return View(rental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Rental/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmpresa,idRental,nombre,foto,descripcion,idDestinoRental,idOrigenRental")] Rental rental, HttpPostedFileBase picture)
        {
            try { 

            var rentalUpdate = db.Rental.ToList().FirstOrDefault(i => i.idRental == rental.idRental);
            string rutaimg = "";
            if (picture == null)
            {
                if (rentalUpdate.foto != null)
                {
                    rutaimg = rentalUpdate.foto;
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
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/destination"), fileName);
                    picture.SaveAs(path);
                    rutaimg = "/TripAgency/Content/upload/destination/" + fileName;
                }
            }
            if (ModelState.IsValid)
            {
                rentalUpdate.foto = rutaimg;
                rentalUpdate.idEmpresa = rental.idEmpresa;
                rentalUpdate.nombre = rental.nombre;
                rentalUpdate.descripcion = rental.descripcion;
                rentalUpdate.idRental = rental.idRental;
                rentalUpdate.idOrigenRental = rental.idOrigenRental;
                db.Entry(rentalUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", rental.idEmpresa);
            ViewBag.idOrigenRental = new SelectList(db.OrigenRental, "idOrigenRental", "nombre", rental.idOrigenRental);
            ViewBag.idDestinoRental = new SelectList(db.DestinoRental, "idDestinoRental", "nombre", rental.idDestinoRental);
            return View(rental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Rental/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rental.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Rental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Rental rental = db.Rental.Find(id);
            db.Rental.Remove(rental);
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
