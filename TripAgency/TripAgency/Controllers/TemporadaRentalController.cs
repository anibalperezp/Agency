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
    public class TemporadaRentalController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /TemporadaRental/
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
            var cadenas = from cadena in db.TemporadaRental select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cadenas = cadenas.Where(cadena => cadena.Anno.anno1.ToUpper().Contains(Search_Data.ToUpper()) ||
                    cadena.Rental.nombre.ToUpper().Contains(Search_Data.ToUpper()) || cadena.Estacion.estacion1.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Temporada.nombre.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.periodo.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Rental.Empresa.nombre_empresa.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Empresa":
                    cadenas = cadenas.OrderByDescending(cadena => cadena.Rental.Empresa.nombre_empresa);
                    break;
                case "Traslado":
                    cadenas = cadenas.OrderByDescending(cadena => cadena.Rental.nombre);
                    break;
                default:
                    cadenas = cadenas.OrderBy(cadena => cadena.Rental.nombre);
                    break;
            }
            int Size_Of_Page = 30;
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

        // GET: /TemporadaRental/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaRental temporadarental = db.TemporadaRental.Find(id);
            if (temporadarental == null)
            {
                return HttpNotFound();
            }
            return View(temporadarental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaRental/Create
        public ActionResult Create()
        {
            try { 
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1");
            ViewBag.idRental = new SelectList(db.Rental, "idRental", "nombre");
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre");
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TemporadaRental/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idTemporadaRental,periodo,inicio,fin,descripcion_periodo,idanno,precio_tarifa,idRental,idEstacion,idTemporada")] TemporadaRental temporadarental)
        {
            try { 
            if (ModelState.IsValid)
            {
                temporadarental.precio_tarifa = 0;
                db.TemporadaRental.Add(temporadarental);
                db.SaveChanges();
                return RedirectToAction("Index");
            
            }

            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadarental.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadarental.idEstacion);
            ViewBag.idRental = new SelectList(db.Rental, "idRental", "nombre", temporadarental.idRental);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadarental.idTemporada);
            return View(temporadarental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaRental/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaRental temporadarental = db.TemporadaRental.Find(id);
            if (temporadarental == null)
            {
                return HttpNotFound();
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadarental.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadarental.idEstacion);
            ViewBag.idRental = new SelectList(db.Rental, "idRental", "nombre", temporadarental.idRental);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadarental.idTemporada);
            return View(temporadarental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TemporadaRental/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idTemporadaRental,periodo,inicio,fin,descripcion_periodo,idanno,precio_tarifa,idRental,idEstacion,idTemporada")] TemporadaRental temporadarental)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(temporadarental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadarental.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadarental.idEstacion);
            ViewBag.idRental = new SelectList(db.Rental, "idRental", "nombre", temporadarental.idRental);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadarental.idTemporada);
            return View(temporadarental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaRental/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaRental temporadarental = db.TemporadaRental.Find(id);
            if (temporadarental == null)
            {
                return HttpNotFound();
            }
            return View(temporadarental);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TemporadaRental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            TemporadaRental temporadarental = db.TemporadaRental.Find(id);
            db.TemporadaRental.Remove(temporadarental);
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

        //********************************************************************************************
        //********Tarifa
        //********************************************************************************************
        public JsonResult periodos(string idRental, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaRentaModel.getInstance().GetPeriodosExc(idRental, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult renttarifa(string idRental, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaRentaModel.getInstance().Getexctarifa(idRental, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult updateTarifa(string[] tarifas)
        {
            var carsList = "";
            bool update = TarifaRentaModel.getInstance().updateExcTarifa(tarifas);
            if (update == true)
            {
                carsList = "Salvó correctamente";
                return Json(carsList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(carsList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Tarifa_Rental()
        {
            try { 
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1");
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre");
            ViewBag.idRental = new SelectList(db.Rental, "idRental", "nombre");
            return View(db.TemporadaRental.ToList());
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
