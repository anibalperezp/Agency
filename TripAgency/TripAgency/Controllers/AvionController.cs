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
using TripAgency.Models;

namespace TripAgency.Controllers
{
    [Authorize] 
    public class AvionController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Avion/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try
            {
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

                var aviones = from avion in db.Avion select avion;
                if (!String.IsNullOrEmpty(Search_Data))
                {
                    aviones = aviones.Where(avion => avion.Empresa.nombre_empresa.ToUpper().Contains(Search_Data.ToUpper()) || avion.nombre.ToUpper().Contains(Search_Data.ToUpper())
                        || avion.horallegada.ToUpper().Contains(Search_Data.ToUpper()) || avion.horasalida.ToUpper().Contains(Search_Data.ToUpper())
                        || avion.OrigenVuelo.nombre.ToUpper().Contains(Search_Data.ToUpper()) || avion.DestinoVuelo.nombre.ToUpper().Contains(Search_Data.ToUpper()) ||
                        avion.numVuelo.ToUpper().Contains(Search_Data.ToUpper()) || avion.TipoAvion.nombre.ToUpper().Contains(Search_Data.ToUpper()));
                }
                switch (Sorting_Order)
                {
                    case "Empresa":
                        aviones = aviones.OrderByDescending(avion => avion.Empresa.nombre_empresa);
                        break;
                    default:
                        aviones = aviones.OrderBy(avion => avion.Empresa.nombre_empresa);
                        break;
                }
                int Size_Of_Page = 15;
                int No_Of_Page = (Page_No ?? 1);
                return View(aviones.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
           
        }

        // GET: /Avion/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Avion avion = db.Avion.Find(id);
                if (avion == null)
                {
                    return HttpNotFound();
                }
                return View(avion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
          
        }

        private void PopulateAssignedClaseData(Avion avion)
        {
            
                var allcat = db.Clase;
                var avionClases = new HashSet<int?>(avion.NomAvionClase.Select(c => c.idClase));
                var viewModel = new List<ClaseModel>();
                foreach (var tipo in allcat)
                {
                    viewModel.Add(new ClaseModel
                    {
                        idClase = tipo.idClase,
                        nombre = tipo.nombre,
                        assigned = avionClases.Contains(tipo.idClase)
                    });
                }
                ViewBag.Courses = viewModel;
            }
        

        // GET: /Avion/Create
        public ActionResult Create()
        {
            try
            {

                var avion = new Avion();
                avion.NomAvionClase = new List<NomAvionClase>();
                var listEmpresaT = new List<Empresa>();
                foreach (var empresa in db.Empresa)
                {
                    foreach (var tipo in empresa.Empresa_TipoEmpresa)
                    {
                        if (tipo.Tipo_Empresa.nombre.Equals("aerotransportista"))
                        {
                            listEmpresaT.Add(empresa);
                        }
                    }
                }
                ViewBag.idEmpresa = new SelectList(listEmpresaT, "idEmpresa", "nombre_empresa");
                ViewBag.idTipoAvion = new SelectList(db.TipoAvion, "idTipoAvion", "nombre");
                ViewBag.idOrigenVuelo = new SelectList(db.OrigenVuelo, "idOrigenVuelo", "nombre");
                ViewBag.idDestinoVuelo = new SelectList(db.DestinoVuelo, "idDestinoVuelo", "nombre");
                PopulateAssignedClaseData(avion);
                return View();  
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
           
        }

        // POST: /Avion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVuelo,numVuelo,nombre,descripcion,idEmpresa,idOrigenVuelo,tsalida,idDestinoVuelo,tllegada,horallegada,horasalida,idTipoAvion")] Avion avion, string[] selectedCourses)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Avion.Add(avion);
                    db.SaveChanges();
                    if (selectedCourses != null)
                    {
                        avion.NomAvionClase = new List<NomAvionClase>();
                        foreach (var course in selectedCourses)
                        {
                            var tph = new NomAvionClase();
                            tph.idClase = int.Parse(course);
                            tph.idVuelo = avion.idVuelo;
                            tph.asignado = true;
                            avion.NomAvionClase.Add(tph);
                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index");
                }

                ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", avion.idEmpresa);
                ViewBag.idTipoAvion = new SelectList(db.TipoAvion, "idTipoAvion", "nombre", avion.idTipoAvion);
                ViewBag.idOrigenVuelo = new SelectList(db.OrigenVuelo, "idOrigenVuelo", "nombre");
                ViewBag.idDestinoVuelo = new SelectList(db.DestinoVuelo, "idDestinoVuelo", "nombre");
                PopulateAssignedClaseData(avion);
                return View(avion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        private void UpdateInstructorClases(string[] selectedClases, Avion avionToUpdate)
        {
            List<int?> listint = new List<int?>();
            if (selectedClases == null)
            {
                avionToUpdate.NomAvionClase = new List<NomAvionClase>();
                return;
            }
            var selectedTiologiasHS = selectedClases.ToList();
            foreach (var tenp in selectedTiologiasHS)
            {
                listint.Add(Convert.ToInt32(tenp));
            }
            var instructorCourses = new HashSet<int?>(avionToUpdate.NomAvionClase.Select(c => c.idClase));
            var listHotelTipologias = instructorCourses.ToList();
            foreach (var course in listint)
            {
                if (listHotelTipologias.Contains(course) == false)
                {
                    var tph = new NomAvionClase();
                    tph.idClase = course;
                    tph.idVuelo = avionToUpdate.idVuelo;
                    tph.asignado = true;
                    avionToUpdate.NomAvionClase.Add(tph);
                }
            }
            var listHTs = avionToUpdate.NomAvionClase.ToList();
            foreach (var tipo in listHTs)
            {
                if (listint.Contains(tipo.idClase) == false)
                {
                    avionToUpdate.NomAvionClase.Remove(tipo);
                }
            }
        }

        // GET: /Avion/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Avion avion = db.Avion.Find(id);
                if (avion == null)
                {
                    return HttpNotFound();
                }
                var listEmpresaT = new List<Empresa>();
                foreach (var empresa in db.Empresa)
                {
                    foreach (var tipo in empresa.Empresa_TipoEmpresa)
                    {
                        if (tipo.Tipo_Empresa.nombre.Equals("aerotransportista"))
                        {
                            listEmpresaT.Add(empresa);
                        }
                    }
                }
                ViewBag.idEmpresa = new SelectList(listEmpresaT, "idEmpresa", "nombre_empresa", avion.idEmpresa);
                ViewBag.idTipoAvion = new SelectList(db.TipoAvion, "idTipoAvion", "nombre", avion.idTipoAvion);
                ViewBag.idOrigenVuelo = new SelectList(db.OrigenVuelo, "idOrigenVuelo", "nombre", avion.idOrigenVuelo);
                ViewBag.idDestinoVuelo = new SelectList(db.DestinoVuelo, "idDestinoVuelo", "nombre",
                    avion.idDestinoVuelo);
                PopulateAssignedClaseData(avion);
                return View(avion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }

        }

        // POST: /Avion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVuelo,numVuelo,nombre,descripcion,idEmpresa,idOrigenVuelo,tsalida,idDestinoVuelo,tllegada,horallegada,horasalida,idTipoAvion")] Avion avion, string[] selectedCourses)
        {
            try
            {

                var avionToUpdate = db.Avion.Include(i => i.NomAvionClase).Single(i => i.idVuelo == avion.idVuelo);
                if (ModelState.IsValid)
                {
                    avionToUpdate.nombre = avion.nombre;
                    avionToUpdate.idOrigenVuelo = avion.idOrigenVuelo;
                    avionToUpdate.tsalida = avion.tsalida;
                    avionToUpdate.horasalida = avion.horasalida;
                    avionToUpdate.idDestinoVuelo = avion.idDestinoVuelo;
                    avionToUpdate.tllegada = avion.tllegada;
                    avionToUpdate.horallegada = avion.horallegada;
                    avionToUpdate.numVuelo = avion.numVuelo;
                    avionToUpdate.idEmpresa = avion.idEmpresa;
                    avionToUpdate.idTipoAvion = avion.idTipoAvion;
                    avionToUpdate.descripcion = avion.descripcion;
                    UpdateInstructorClases(selectedCourses, avionToUpdate);
                    db.Entry(avionToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", avion.idEmpresa);
                ViewBag.idTipoAvion = new SelectList(db.TipoAvion, "idTipoAvion", "nombre", avion.idTipoAvion);
                ViewBag.idOrigenVuelo = new SelectList(db.OrigenVuelo, "idOrigenVuelo", "nombre", avion.idOrigenVuelo);
                ViewBag.idDestinoVuelo = new SelectList(db.DestinoVuelo, "idDestinoVuelo", "nombre",
                    avion.idDestinoVuelo);
                PopulateAssignedClaseData(avion);
                return View(avion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Avion/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Avion avion = db.Avion.Find(id);
                if (avion == null)
                {
                    return HttpNotFound();
                }
                return View(avion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
            
        }

        // POST: /Avion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Avion avion = db.Avion.Find(id);
                int cant = db.NomAvionClase.ToList().Count;
                if (cant != 0)
                {
                    foreach (var tipo in db.NomAvionClase.ToList())
                    {
                        if (tipo.idVuelo == avion.idVuelo)
                        {
                            db.NomAvionClase.Remove(tipo);
                            db.SaveChanges();
                        }
                    }
                }
                else if (cant == 0)
                {

                }
                db.Avion.Remove(avion);
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
