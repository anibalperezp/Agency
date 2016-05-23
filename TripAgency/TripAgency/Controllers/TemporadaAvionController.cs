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
    public class TemporadaAvionController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /TemporadaAvion/
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
            var exc = from cadena in db.TemporadaAvion select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                exc = exc.Where(cadena =>cadena.Estacion.estacion1.ToUpper().Contains(Search_Data.ToUpper()) ||
                    cadena.Temporada.nombre.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Anno.anno1.ToUpper().Contains(Search_Data.ToUpper()) || cadena.periodo.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Empresa":
                    exc = exc.OrderByDescending(cadena => cadena.Empresa.nombre_empresa);
                    break;
                case "Anno":
                    exc = exc.OrderByDescending(cadena => cadena.Anno.anno1);
                    break;
                default:
                    exc = exc.OrderBy(cadena => cadena.Empresa.nombre_empresa);
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

        // GET: /TemporadaAvion/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaAvion temporadaavion = db.TemporadaAvion.Find(id);
            if (temporadaavion == null)
            {
                return HttpNotFound();
            }
            return View(temporadaavion);
                   }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        
        }

        // GET: /TemporadaAvion/Create
        public ActionResult Create()
        {
            try
            {
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
                ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1");
                ViewBag.idEmpresa = new SelectList(listEmpresaT, "idEmpresa", "nombre_empresa");
                ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1");
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

        // POST: /TemporadaAvion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmpresa,idTemporadaVuelo,periodo,inicio,fin,descripcion_periodo,idanno,idEstacion,idTemporada,tarifa")] TemporadaAvion temporadaavion)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.TemporadaAvion.Add(temporadaavion);
                db.SaveChanges();
                var sd = db.Empresa.Find(temporadaavion.idEmpresa);
                if (sd.Avion.Count != 0)
                {
                    foreach (var avion in sd.Avion)
                    {
                        if (avion != null)
                        {
                            foreach (var clase in avion.NomAvionClase)
                            {
                                var tempnom1 = new NomTempAvionClase();
                                tempnom1.idVuelo = avion.idVuelo;
                                tempnom1.idTemporadaVuelo = temporadaavion.idTemporadaVuelo;
                                tempnom1.tarifa = 0;
                                tempnom1.idClase = clase.idClase;
                                db.NomTempAvionClase.Add(tempnom1);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                
                
                return RedirectToAction("Index");
            }

            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadaavion.idanno);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadaavion.idEstacion);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadaavion.idTemporada);
            return View(temporadaavion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaAvion/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TemporadaAvion temporadaavion = db.TemporadaAvion.Find(id);
                if (temporadaavion == null)
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
                ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadaavion.idanno);
                ViewBag.idEmpresa = new SelectList(listEmpresaT, "idEmpresa", "nombre_empresa", temporadaavion.idEmpresa);
                ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadaavion.idEstacion);
                ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadaavion.idTemporada);
                return View(temporadaavion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TemporadaAvion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmpresa,idTemporadaVuelo,periodo,inicio,fin,descripcion_periodo,idanno,idEstacion,idTemporada,tarifa")] TemporadaAvion temporadaavion)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(temporadaavion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadaavion.idanno);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadaavion.idEstacion);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadaavion.idTemporada);
            return View(temporadaavion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaAvion/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaAvion temporadaavion = db.TemporadaAvion.Find(id);
            if (temporadaavion == null)
            {
                return HttpNotFound();
            }
            return View(temporadaavion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TemporadaAvion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            TemporadaAvion temporadaavion = db.TemporadaAvion.Find(id);
            int cant = db.NomTempAvionClase.ToList().Count;
            if (cant != 0)
            {
                foreach (var tipo in db.NomTempAvionClase.ToList())
                {
                    if (tipo.idTemporadaVuelo == temporadaavion.idTemporadaVuelo)
                    {
                        db.NomTempAvionClase.Remove(tipo);
                        db.SaveChanges();
                    }
                }
            }
            else
                if (cant == 0)
                {

                }
            db.TemporadaAvion.Remove(temporadaavion);
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
            List<string> carsList = TarifaVueloModel.getInstance().GetPeriodosVuelos(idEmpresa, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult vuelotarifa(string idEmpresa, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaVueloModel.getInstance().Getvuelotarifa(idEmpresa, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult updateTarifa(string[] tarifas)
        {
            var carsList = "";
            bool update = TarifaVueloModel.getInstance().updateVueloTarifa(tarifas);
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

        public ActionResult Tarifa_Avion()
        {
            try {
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1");
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre");
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            return View(db.TemporadaAvion.ToList());
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
