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
    public class OficinaRentaController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /OficinaRenta/
        public ActionResult Index(string Sorting_Order, string Search_Data ,string Filter_Value, int? Page_No)
        {
            try { 
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

            var ofics = from ofc in db.OficinaRenta select ofc;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                ofics = ofics.Where(ofc => ofc.nombre.ToUpper().Contains(Search_Data.ToUpper()) || ofc.Empresa.nombre_empresa.ToUpper().Contains(Search_Data.ToUpper())
                    || ofc.Destino.destino1.ToUpper().Contains(Search_Data.ToUpper()) ||
                    ofc.descripcion.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    ofics = ofics.OrderByDescending(ofc => ofc.Empresa.nombre_empresa);
                    break;
                default:
                    ofics = ofics.OrderBy(ofc => ofc.Empresa.nombre_empresa);
                    break;
            }
            int Size_Of_Page = 11;
            int No_Of_Page = (Page_No ?? 1);
            return View(ofics.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /OficinaRenta/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OficinaRenta oficinarenta = db.OficinaRenta.Find(id);
            if (oficinarenta == null)
            {
                return HttpNotFound();
            }
            return View(oficinarenta);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /OficinaRenta/Create
        public ActionResult Create()
        {
            try { 
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
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

        public JsonResult subdestinos(string idDestino)
        {
            List<string> carsList = new SubModel().GetSubXDestinos(idDestino);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }

        // POST: /OficinaRenta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idOficina,nombre,imagen,descripcion,idSubdestino,idEmpresa")] OficinaRenta oficinarenta, HttpPostedFileBase picture)
        {
            try { 
            var strMappath = "/TripAgency/TripAgency/Content/Upload/OficinaRenta/" + oficinarenta.nombre;
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
                            return View(oficinarenta);
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
                oficinarenta.imagen = rutaimg;
                db.OficinaRenta.Add(oficinarenta);
                db.SaveChanges();
                if (db.Carro.ToList().Count() != 0)
                {
                    foreach (var car in db.Carro.ToList())
                    {
                        var tph = new OficPerCar();
                        tph.idOficina = oficinarenta.idOficina;
                        tph.idCarro = car.idCarro;
                        oficinarenta.OficPerCar.Add(tph);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", oficinarenta.idEmpresa);
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1");
            return View(oficinarenta);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /OficinaRenta/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OficinaRenta oficinarenta = db.OficinaRenta.Find(id);
            if (oficinarenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", oficinarenta.idEmpresa);
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1");
            return View(oficinarenta);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /OficinaRenta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idOficina,nombre,imagen,descripcion,idSubdestino,idEmpresa")] OficinaRenta oficinarenta, HttpPostedFileBase picture)
        {
            try { 
            var oficinaToUpdate = db.OficinaRenta.Single(i => i.idOficina == oficinarenta.idOficina);
            string rutaimg = "";
            if (picture == null)
            {
                if (oficinaToUpdate.imagen != null)
                {
                    rutaimg = oficinaToUpdate.imagen;
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
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/oficina"), fileName);
                    picture.SaveAs(path);
                    rutaimg = "/TripAgency/Content/upload/oficina/" + fileName;
                }
            }
            if (ModelState.IsValid)
            {
                oficinaToUpdate.imagen = rutaimg;
                oficinaToUpdate.nombre = oficinarenta.nombre;
                oficinaToUpdate.descripcion = oficinarenta.descripcion;
                oficinaToUpdate.idEmpresa = oficinarenta.idEmpresa;
                oficinaToUpdate.idDestino = oficinarenta.idDestino;
                db.Entry(oficinaToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", oficinarenta.idEmpresa);
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1");
            return View(oficinarenta);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /OficinaRenta/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OficinaRenta oficinarenta = db.OficinaRenta.Find(id);
            if (oficinarenta == null)
            {
                return HttpNotFound();
            }
            return View(oficinarenta);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /OficinaRenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            OficinaRenta oficinarenta = db.OficinaRenta.Find(id);
            if (oficinarenta == null)
            {
                return HttpNotFound();
            }
            else
            {
                int cant = db.OficPerCar.ToList().Count;
                if (cant != 0)
                {
                    foreach (var tipo in db.OficPerCar.ToList())
                    {
                        if (tipo.idOficina == oficinarenta.idOficina)
                        {
                            db.OficPerCar.Remove(tipo);
                            db.SaveChanges();
                        }
                       
                    }
                }
                else if (cant == 0)
                {

                }
            }
            db.OficinaRenta.Remove(oficinarenta);
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
