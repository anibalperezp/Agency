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
    public class CarroController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

       
        // GET: /Carro/
        public ActionResult Index(string Sorting_Order, string Search_Data,string Filter_Value, int? Page_No)
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
            var cadenas = from cadena in db.Carro select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cadenas = cadenas.Where(cadena => cadena.Empresa.nombre_empresa.ToUpper().Contains(Search_Data.ToUpper()) 
                    || cadena.name.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Empresa":
                    cadenas = cadenas.OrderByDescending(cadena => cadena.Empresa.nombre_empresa);
                    break;
                default:
                    cadenas = cadenas.OrderBy(cadena => cadena.Empresa.nombre_empresa);
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


        // GET: /Carro/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carro.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Carro/Create
        public ActionResult Create()
        {
            try 
            {
                var listEmpresaT = new List<Empresa>();
                foreach (var empresa in db.Empresa)
                {
                    foreach (var tipo in empresa.Empresa_TipoEmpresa)
                    {
                        if (tipo.Tipo_Empresa.nombre.Equals("transportista"))
                        {
                            listEmpresaT.Add(empresa);
                        }
                    }
                }
                ViewBag.idempresa = new SelectList(listEmpresaT, "idEmpresa", "nombre_empresa");
                ViewBag.idtipologia_carro = new SelectList(db.Tipologia_Carro, "idtipologia_carro", "nombre");
                return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Carro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCarro,chapaCarro,foto_carro1,foto_carro2,foto_carro3,idtipologia_carro,descripcion_carro,idempresa,idmarca,name")] Carro carro, HttpPostedFileBase picture, HttpPostedFileBase picture2, HttpPostedFileBase picture3)
        {
            try { 

            var strMappath = "/TripAgency/TripAgency/Content/Upload/Carro/" + "Carro" + " " + carro.name;
            DirectoryInfo di = null;
            string rutaimg = "";
            string rutaimg2 = "";
            string rutaimg3 = "";
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
                            return View(carro);
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
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
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
                            return View(carro);
                        }
                    }
                    var destinationPath = Path.Combine(Server.MapPath(di.Name), fileName);
                    picture2.SaveAs(name_file2);
                    Util ddd = new Util();
                    rutaimg2 = ddd.name(name_file2);
                }
            }

            if (picture3 == null)
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            else if (picture3.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
                if (!extensions.Contains(picture3.FileName.Substring(picture3.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                }
                else if (picture3.ContentLength > maxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                }
                else
                {
                    string fileName = Path.GetFileName(picture3.FileName);
                    var exten = picture3.FileName.Substring(picture3.FileName.LastIndexOf('.'));
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
                    var name_file3 = di.FullName + "\\" + fileName;
                    var path = Directory.GetFiles(strMappath);
                    var fullPath = strMappath + picture3.FileName;

                    foreach (var p in path.ToList())
                    {
                        if (p.Equals(fullPath))
                        {
                            ModelState.AddModelError("",
                                "El archivo " + picture3.FileName + " " + "ya existe");
                            return View(carro);
                        }
                    }
                    var destinationPath = Path.Combine(Server.MapPath(di.Name), fileName);
                    picture3.SaveAs(name_file3);
                    var ddd = new Util();
                    rutaimg3 = ddd.name(name_file3);
                }
            }

            if (ModelState.IsValid)
            {
                carro.foto_carro1 = rutaimg;
                carro.foto_carro2 = rutaimg2;
                carro.foto_carro3 = rutaimg3;
                db.Carro.Add(carro);
                db.SaveChanges();
                if (db.OficinaRenta.ToList().Count() != 0)
                {
                    foreach (var of in db.OficinaRenta.ToList())
                    {
                        var tph = new OficPerCar();
                        tph.idCarro = carro.idCarro;
                        tph.idOficina = of.idOficina;
                        carro.OficPerCar.Add(tph);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.idempresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", carro.idempresa);
            ViewBag.idtipologia_carro = new SelectList(db.Tipologia_Carro, "idtipologia_carro", "nombre", carro.idtipologia_carro);
            return View(carro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Carro/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carro.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            var listEmpresaT = new List<Empresa>();
            foreach (var empresa in db.Empresa)
            {
                foreach (var tipo in empresa.Empresa_TipoEmpresa)
                {
                    if (tipo.Tipo_Empresa.nombre.Equals("transportista"))
                    {
                        listEmpresaT.Add(empresa);
                    }
                }
            }
            ViewBag.idempresa = new SelectList(listEmpresaT, "idEmpresa", "nombre_empresa", carro.idempresa);
            ViewBag.idtipologia_carro = new SelectList(db.Tipologia_Carro, "idtipologia_carro", "nombre", carro.idtipologia_carro);
            return View(carro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Carro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCarro,chapaCarro,foto_carro1,foto_carro2,foto_carro3,idtipologia_carro,descripcion_carro,idempresa,idmarca,name")] Carro carro, HttpPostedFileBase picture, HttpPostedFileBase picture2, HttpPostedFileBase picture3)
        {
            try { 
            string rutaimg = "";
            string rutaimg2 = "";
            string rutaimg3 = "";
            var subtUpdate = db.Carro.ToList().FirstOrDefault(i => i.idCarro == carro.idCarro);
            if (picture == null)
            {
                if (subtUpdate.foto_carro1 != null)
                {
                    rutaimg = subtUpdate.foto_carro1;
                }
            }
            else 
                if (picture.ContentLength > 0)
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
                        var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/empresa"), fileName);
                        picture.SaveAs(path);
                        rutaimg = "/TripAgency/Content/upload/empresa/" + fileName;
                    }
                }

            if (picture2 == null)
            {
                if (subtUpdate.foto_carro2 != null)
                {
                    rutaimg2 = subtUpdate.foto_carro2;
                }
            }
            else 
                if (picture2.ContentLength > 0)
                {
                    const int maxContentLength = 1024 * 1024 * 5; //5 MB
                    var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
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
                        var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/empresa"), fileName);
                        picture2.SaveAs(path);
                        rutaimg2 = "/TripAgency/Content/upload/empresa/" + fileName;
                    }
                }

            if (picture3 == null)
            {
                if (subtUpdate.foto_carro3 != null)
                {
                    rutaimg3 = subtUpdate.foto_carro3;
                }
            }
            else 
                if (picture3.ContentLength > 0)
                {
                    const int maxContentLength = 1024 * 1024 * 5; //5 MB
                    var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
                    if (!extensions.Contains(picture3.FileName.Substring(picture3.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                    }
                    else if (picture3.ContentLength > maxContentLength)
                    {
                        ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                    }
                    else
                    {
                        //TO:DO
                        var fileName = System.IO.Path.GetFileName(picture3.FileName);
                        var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/empresa"), fileName);
                        picture3.SaveAs(path);
                        rutaimg3 = "/TripAgency/Content/upload/empresa/" + fileName;
                    }
                }

            if (ModelState.IsValid)
            {
                subtUpdate.foto_carro1 = rutaimg;
                subtUpdate.foto_carro2 = rutaimg2;
                subtUpdate.foto_carro3 = rutaimg3;
                subtUpdate.name = carro.name;
                subtUpdate.descripcion_carro = carro.descripcion_carro;
                subtUpdate.idempresa = carro.idempresa;
                subtUpdate.transmision = carro.transmision;
                subtUpdate.pasajeros = carro.pasajeros;
                subtUpdate.idtipologia_carro = carro.idtipologia_carro;
                db.Entry(subtUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idempresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", carro.idempresa);
            ViewBag.idtipologia_carro = new SelectList(db.Tipologia_Carro, "idtipologia_carro", "nombre", carro.idtipologia_carro);
            return View(carro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Carro/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carro.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Carro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Carro carro = db.Carro.Find(id);
                int cant = db.OficPerCar.ToList().Count;
                if (cant != 0)
                {
                    foreach (var tipo in db.OficPerCar.ToList())
                    {
                        if (tipo.idCarro == carro.idCarro)
                        {
                            db.OficPerCar.Remove(tipo);
                            db.SaveChanges();
                        }
                    }
                }
                else if (cant == 0)
                {

                }
            db.Carro.Remove(carro);
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
