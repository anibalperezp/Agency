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
    public class EmpresaController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Empresa/
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
            var cats = from cat in db.Empresa select cat;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cats = cats.Where(cat => cat.nombre_empresa.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    cats = cats.OrderByDescending(cat => cat.nombre_empresa);
                    break;
                default:
                    cats = cats.OrderBy(cat => cat.nombre_empresa);
                    break;
            }
            int Size_Of_Page = 9;
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

        // GET: /Empresa/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        private void PopulateAssignedTiposEmpresaData(Empresa empresa)
        {
            var alltipol = db.Tipo_Empresa;
            var hotelTipologias = new HashSet<int?>(empresa.Empresa_TipoEmpresa.Select(c => c.idtipoempresa));
            var viewModel = new List<Tipo_Empresa_Asigned>();
            foreach (var tipo in alltipol)
            {
                viewModel.Add(new Tipo_Empresa_Asigned
                {
                    idTipoEmpresa = tipo.idtipoempresa,
                    tipoempresa = tipo.nombre,
                    assigned = hotelTipologias.Contains(tipo.idtipoempresa)
                });
            }
            ViewBag.TipoEmpresa = viewModel;
        }

        // GET: /Empresa/Create
        public ActionResult Create()
        {
            try { 
            var empresa = new Empresa();
            empresa.Empresa_TipoEmpresa = new List<Empresa_TipoEmpresa>();
            PopulateAssignedTiposEmpresaData(empresa);
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Empresa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmpresa,nombre_empresa,foto_empresa,direccion_empresa,telefono_empresa")] Empresa empresa, HttpPostedFileBase picture, string[] selectedTipos)
        {
            try { 
            var strMappath = "/TripAgency/TripAgency/Content/Upload/Empresa/" + empresa.nombre_empresa;
            DirectoryInfo di = null;
            string rutaimg = "";
            if (selectedTipos != null)
            {
                empresa.Empresa_TipoEmpresa = new List<Empresa_TipoEmpresa>();
                foreach (var course in selectedTipos)
                {
                    var tph = new Empresa_TipoEmpresa();
                    tph.idtipoempresa = int.Parse(course);
                    tph.idEmpresa = empresa.idEmpresa;
                    tph.asignado = true;
                    empresa.Empresa_TipoEmpresa.Add(tph);
                }
            }

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
                            return View(empresa);
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
                empresa.foto_empresa = rutaimg;
                db.Empresa.Add(empresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateAssignedTiposEmpresaData(empresa);
            return View(empresa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        private void UpdateEmpresaTipos(string[] selectedTipo, Empresa empresa)
        {
            List<int?> listint = new List<int?>();
            if (selectedTipo == null)
            {
                empresa.Empresa_TipoEmpresa = new List<Empresa_TipoEmpresa>();
                return;
            }
            var selectedTipoEmpresasHS = selectedTipo.ToList();
            foreach (var tenp in selectedTipoEmpresasHS)
            {
                listint.Add(Convert.ToInt32(tenp));
            }
            var empresaTipos = new HashSet<int?>(empresa.Empresa_TipoEmpresa.Select(c => c.idtipoempresa));
            var listempresaTipos = empresaTipos.ToList();
            foreach (var course in listint)
            {
                if (listempresaTipos.Contains(course) == false)
                {
                    var tph = new Empresa_TipoEmpresa();
                    tph.idtipoempresa = course;
                    tph.idEmpresa = empresa.idEmpresa;
                    tph.asignado = true;
                    empresa.Empresa_TipoEmpresa.Add(tph);
                }
            }
            var listHTs = empresa.Empresa_TipoEmpresa.ToList();
            foreach (var tipo in listHTs)
            {
                if (listint.Contains(tipo.idtipoempresa) == false)
                {
                    empresa.Empresa_TipoEmpresa.Remove(tipo);
                }
            }
        }

        // GET: /Empresa/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresa.Find(id);
            PopulateAssignedTiposEmpresaData(empresa);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Empresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? idEmpresa, [Bind(Include = "nombre_empresa,foto_empresa,direccion_empresa,telefono_empresa")] Empresa empresa, HttpPostedFileBase picture, string[] selectedTipos)
        {
            try { 
            var empresaToUpdate = db.Empresa.Include(i => i.Empresa_TipoEmpresa).Single(i => i.idEmpresa == idEmpresa);
            string rutaimg = "";
            if (picture == null)
            {
                if (empresaToUpdate.foto_empresa != null)
                {
                    rutaimg = empresaToUpdate.foto_empresa;
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
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/empresa"), fileName);
                    picture.SaveAs(path);
                    rutaimg = "/TripAgency/Content/upload/empresa/" + fileName;
                }
            }
            
            if (ModelState.IsValid)
            {

                empresaToUpdate.foto_empresa = rutaimg;
                empresaToUpdate.nombre_empresa = empresa.nombre_empresa;
                empresaToUpdate.telefono_empresa = empresa.telefono_empresa;
                empresaToUpdate.direccion_empresa = empresa.direccion_empresa;
                UpdateEmpresaTipos(selectedTipos, empresaToUpdate);
                db.Entry(empresaToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedTiposEmpresaData(empresa);
            return View(empresa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Empresa/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresa.Find(id);

            if (empresa == null)
            {
                return HttpNotFound();
            }
            else
            {
                int cantred = empresa.Empresa_TipoEmpresa.ToList().Count;
                if (cantred != 0)
                {
                    foreach (var tipo in empresa.Empresa_TipoEmpresa.ToList())
                    {
                        empresa.Empresa_TipoEmpresa.Remove(tipo);
                    }
                }
                else
                    if (cantred == 0)
                    {

                    }
            }
            
            return View(empresa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Empresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Empresa empresa = db.Empresa.Find(id);
            int cantred = empresa.Empresa_TipoEmpresa.ToList().Count;
            if (cantred != 0)
            {
                foreach (var tipo in empresa.Empresa_TipoEmpresa.ToList())
                {
                    empresa.Empresa_TipoEmpresa.Remove(tipo);
                }
            }
            else
                if (cantred == 0)
                {

                }
            db.Empresa.Remove(empresa);
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
