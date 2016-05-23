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
    public class TemporadaPaqueteController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();
        // GET: /TemporadaPaquete/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try{
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
            var exc = from cadena in db.TemporadaPaquete select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                exc = exc.Where(cadena => cadena.Paquete.nombre_paquete.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Estacion.estacion1.ToUpper().Contains(Search_Data.ToUpper()) || cadena.Temporada.nombre.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Anno.anno1.ToUpper().Contains(Search_Data.ToUpper()) || cadena.periodo.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Empresa":
                    exc = exc.OrderByDescending(cadena => cadena.Paquete.Empresa.nombre_empresa);
                    break;
                default:
                    exc = exc.OrderBy(cadena => cadena.Paquete.Empresa.nombre_empresa);
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

        // GET: /TemporadaPaquete/Details/5
        public ActionResult Details(int? id)
        {
            try{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaPaquete temporadapaquete = db.TemporadaPaquete.Find(id);
            if (temporadapaquete == null)
            {
                return HttpNotFound();
            }
            return View(temporadapaquete);
   }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaPaquete/Create
        public ActionResult Create()
        {
            try{
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

        public JsonResult paquetes(string idEmpresa)
        {
            List<string> paqList = new TemporadaPaq().GetPaqXEmpresa(idEmpresa);
            return Json(paqList, JsonRequestBehavior.AllowGet);
        }

        // POST: /TemporadaPaquete/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idtemporadapaquete,periodo,inicio,fin,descripcion_periodo,idanno,precio_tarifa,idPaquete,idEstacion,idTemporada")] TemporadaPaquete temporadapaquete, string State)
        {
            try{
            if (ModelState.IsValid)
            {
                var sub = db.Paquete.FirstOrDefault(c => c.nombre_paquete == State);
                if (temporadapaquete.precio_tarifa == null)
                {
                    temporadapaquete.precio_tarifa = 0;
                }
                temporadapaquete.idPaquete = sub.idPaquete;
                db.TemporadaPaquete.Add(temporadapaquete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadapaquete.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadapaquete.idEstacion);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadapaquete.idTemporada);
            return View(temporadapaquete);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaPaquete/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaPaquete temporadapaquete = db.TemporadaPaquete.Find(id);
            if (temporadapaquete == null)
            {
                return HttpNotFound();
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadapaquete.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadapaquete.idEstacion);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadapaquete.idTemporada);
            return View(temporadapaquete);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TemporadaPaquete/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idtemporadapaquete,periodo,inicio,fin,descripcion_periodo,idanno,precio_tarifa,idPaquete,idEstacion,idTemporada")] TemporadaPaquete temporadapaquete, string State)
        {
            try{
            if (ModelState.IsValid)
            {
                var sub = db.Paquete.FirstOrDefault(c => c.nombre_paquete == State);
                temporadapaquete.idPaquete = sub.idPaquete;
                db.Entry(temporadapaquete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadapaquete.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadapaquete.idEstacion);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadapaquete.idTemporada);
            return View(temporadapaquete);
              }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaPaquete/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaPaquete temporadapaquete = db.TemporadaPaquete.Find(id);
            if (temporadapaquete == null)
            {
                return HttpNotFound();
            }
            return View(temporadapaquete);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TemporadaPaquete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            TemporadaPaquete temporadapaquete = db.TemporadaPaquete.Find(id);
            db.TemporadaPaquete.Remove(temporadapaquete);
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
        public JsonResult periodos(string idEmpresa, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaPaqueteModel.getInstance().GetPeriodosPaquete(idEmpresa, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult paqtarifa(string idEmpresa, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaPaqueteModel.getInstance().Getpaquetetarifa(idEmpresa, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult updateTarifa(string[] tarifas)
        {
            var carsList = "";
            bool update = TarifaPaqueteModel.getInstance().updatePaqueteTarifa(tarifas);
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

        public ActionResult Tarifa_Paquete()
        {
            try { 
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1");
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            return View(db.TemporadaPaquete.ToList());
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
