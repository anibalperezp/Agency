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
    public class TemporadaExcursionController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /TemporadaExcursion/
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
            var exc = from cadena in db.TemporadaExcursion select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                exc = exc.Where(cadena => cadena.Excursion.nombre_excursion.ToUpper().Contains(Search_Data.ToUpper()) 
                    || cadena.Estacion.estacion1.ToUpper().Contains(Search_Data.ToUpper()) || cadena.Temporada.nombre.ToUpper().Contains(Search_Data.ToUpper()) 
                    || cadena.Anno.anno1.ToUpper().Contains(Search_Data.ToUpper()) || cadena.periodo.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Excursion.Empresa.nombre_empresa.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Empresa":
                    exc = exc.OrderByDescending(cadena => cadena.Excursion.Empresa.nombre_empresa);
                    break;
                default:
                    exc = exc.OrderBy(cadena => cadena.Excursion.Empresa.nombre_empresa);
                    break;
            }
            int Size_Of_Page = 30;
            int No_Of_Page = (Page_No ?? 1);
            return View(exc.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaExcursion/Details/5
        public ActionResult Details(int? id)
        {
            try{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaExcursion temporadaexcursion = db.TemporadaExcursion.Find(id);
            if (temporadaexcursion == null)
            {
                return HttpNotFound();
            }
            return View(temporadaexcursion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaExcursion/Create
        public ActionResult Create()
        {
            try { 
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1");
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
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

        public JsonResult excursiones(string idEmpresa)
        {
            List<string> excList = new TemporadaExc().GetExcXEmpresa(idEmpresa);
            return Json(excList, JsonRequestBehavior.AllowGet);
        }

        // POST: /TemporadaExcursion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idtemporadaexcursion,periodo,inicio,fin,descripcion_periodo,idanno,precio_tarifa,idExcursion,idEstacion,idTemporada")] TemporadaExcursion temporadaexcursion, string State)
        {
            try
            {
            var sub = db.Excursion.FirstOrDefault(c => c.nombre_excursion == State);
            if (ModelState.IsValid)
            {
                if (temporadaexcursion.precio_tarifa == null)
                {
                    temporadaexcursion.precio_tarifa = 0;
                }
                temporadaexcursion.idExcursion = sub.idExcursion;
                db.TemporadaExcursion.Add(temporadaexcursion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadaexcursion.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadaexcursion.idEstacion);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadaexcursion.idTemporada);
            return View(temporadaexcursion);
             }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }

        }

        // GET: /TemporadaExcursion/Edit/5
        public ActionResult Edit(int? id)
        {
            try{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaExcursion temporadaexcursion = db.TemporadaExcursion.Find(id);
            if (temporadaexcursion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadaexcursion.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadaexcursion.idEstacion);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadaexcursion.idTemporada);
            return View(temporadaexcursion);
             }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TemporadaExcursion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idtemporadaexcursion,periodo,inicio,fin,descripcion_periodo,idanno,precio_tarifa,idExcursion,idEstacion,idTemporada")] TemporadaExcursion temporadaexcursion, string State)
        {
            try
            {
            if (ModelState.IsValid)
            {
                var sub = db.Excursion.FirstOrDefault(c => c.nombre_excursion == State);
                temporadaexcursion.idExcursion = sub.idExcursion;
                db.Entry(temporadaexcursion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadaexcursion.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadaexcursion.idEstacion);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadaexcursion.idTemporada);
            return View(temporadaexcursion);
 }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaExcursion/Delete/5
        public ActionResult Delete(int? id)
        {
            try{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaExcursion temporadaexcursion = db.TemporadaExcursion.Find(id);
            if (temporadaexcursion == null)
            {
                return HttpNotFound();
            }
            return View(temporadaexcursion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TemporadaExcursion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try{
            TemporadaExcursion temporadaexcursion = db.TemporadaExcursion.Find(id);
            db.TemporadaExcursion.Remove(temporadaexcursion);
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
        public JsonResult periodos(string idDestino, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaExcModel.getInstance().GetPeriodosExc(idDestino, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult exctarifa(string idDestino, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaExcModel.getInstance().Getexctarifa(idDestino, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult updateTarifa(string[] tarifas)
        {
            var carsList = "";
            bool update = TarifaExcModel.getInstance().updateExcTarifa(tarifas);
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

        public ActionResult Tarifa_Excursion()
        {
            try{
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1");
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre");
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1");
            return View(db.TemporadaExcursion.ToList());
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
