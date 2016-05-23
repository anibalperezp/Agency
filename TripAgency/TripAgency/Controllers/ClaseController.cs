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
    public class ClaseController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Clase/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
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

            var clases = from avion in db.Clase select avion;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                clases = clases.Where(avion => avion.nombre.ToUpper().Contains(Search_Data.ToUpper())
                    || avion.descripcion.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    clases = clases.OrderByDescending(avion => avion.nombre);
                    break;
                default:
                    clases = clases.OrderBy(avion => avion.nombre);
                    break;
            }
            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(clases.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Clase/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clase clase = db.Clase.Find(id);
            if (clase == null)
            {
                return HttpNotFound();
            }
            return View(clase);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        private void PopulateAssignedCategoryData(Clase clase)
        {
            var allcat = db.CategTarifAvion;
            var hotelTipologias = new HashSet<int?>(clase.NomClaseCatTarAvi.Select(c => c.idCategTarifAvion));
            var viewModel = new List<CategoriaTarifaAvionModel>();
            foreach (var tipo in allcat)
            {
                viewModel.Add(new CategoriaTarifaAvionModel
                {
                    idCategTarifAvion = tipo.idCategTarifAvion,
                    codigo = tipo.codigocat,
                    assigned = hotelTipologias.Contains(tipo.idCategTarifAvion)
                });
            }
            ViewBag.Courses = viewModel;
        }

        // GET: /Clase/Create
        public ActionResult Create()
        {
            try { 
            var clase = new Clase();
            clase.NomClaseCatTarAvi = new List<NomClaseCatTarAvi>();
            PopulateAssignedCategoryData(clase);
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Clase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idClase,nombre,descripcion")] Clase clase, string[] selectedCourses)
        {
            try { 
            if (ModelState.IsValid)
            {

                db.Clase.Add(clase);
                db.SaveChanges();
                if (selectedCourses != null)
                {
                    clase.NomClaseCatTarAvi = new List<NomClaseCatTarAvi>();
                    foreach (var course in selectedCourses)
                    {
                        var tph = new NomClaseCatTarAvi();
                        tph.idCategTarifAvion = int.Parse(course);
                        tph.idClase = clase.idClase;
                        tph.asignado = true;
                        clase.NomClaseCatTarAvi.Add(tph);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            PopulateAssignedCategoryData(clase);
            return View(clase);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        private void UpdateInstructorCourses(string[] selectedCatg, Clase claseToUpdate)
        {
             
            List<int?> listint = new List<int?>();
            if (selectedCatg == null)
            {
                claseToUpdate.NomClaseCatTarAvi = new List<NomClaseCatTarAvi>();
                return;
            }
            var selectedTiologiasHS = selectedCatg.ToList();
            foreach (var tenp in selectedTiologiasHS)
            {
                listint.Add(Convert.ToInt32(tenp));
            }
            var instructorCourses = new HashSet<int?>(claseToUpdate.NomClaseCatTarAvi.Select(c => c.idCategTarifAvion));
            var listHotelTipologias = instructorCourses.ToList();
            foreach (var course in listint)
            {
                if (listHotelTipologias.Contains(course) == false)
                {
                    var tph = new NomClaseCatTarAvi();
                    tph.idCategTarifAvion = course;
                    tph.idClase = claseToUpdate.idClase;
                    tph.asignado = true;
                    claseToUpdate.NomClaseCatTarAvi.Add(tph);
                }
            }
            var listHTs = claseToUpdate.NomClaseCatTarAvi.ToList();
            foreach (var tipo in listHTs)
            {
                if (listint.Contains(tipo.idCategTarifAvion) == false)
                {
                    claseToUpdate.NomClaseCatTarAvi.Remove(tipo);
                }
            }
         
        }

        // GET: /Clase/Edit/5Default1
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clase clase = db.Clase.Find(id);
            if (clase == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedCategoryData(clase);
            return View(clase);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Clase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idClase,nombre,descripcion")] Clase clase,string[] selectedCourses)
        {
            try { 
            var claseToUpdate = db.Clase.Include(i => i.NomClaseCatTarAvi).Single(i => i.idClase == clase.idClase);
            if (ModelState.IsValid)
            {
                claseToUpdate.nombre = clase.nombre;
                claseToUpdate.descripcion = clase.descripcion;
                UpdateInstructorCourses(selectedCourses, claseToUpdate);
                db.Entry(claseToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedCategoryData(clase);
            return View(clase);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Clase/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clase clase = db.Clase.Find(id);
            if (clase == null)
            {
                return HttpNotFound();
            }
            return View(clase);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Clase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Clase clase = db.Clase.Find(id);
            int cant = db.NomClaseCatTarAvi.ToList().Count;
            if (cant != 0)
            {
                foreach (var tipo in db.NomClaseCatTarAvi.ToList())
                {
                    if (tipo.idClase == clase.idClase)
                    {
                        db.NomClaseCatTarAvi.Remove(tipo);
                        db.SaveChanges();
                    }
                }
            }
            else
                if (cant == 0)
                {

                }
            db.Clase.Remove(clase);
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
