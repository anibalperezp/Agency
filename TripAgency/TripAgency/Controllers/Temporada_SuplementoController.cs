using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TripAgency.EF;

namespace TripAgency.Controllers
{
     [Authorize] 
    public class Temporada_SuplementoController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Temporada_Suplemento/
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try { 
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingNombre = String.IsNullOrEmpty(Sorting_Order) ? "Hotel" : "";
            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;
            var exc = from cadena in db.Temporada_Suplemento select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                exc = exc.Where(cadena =>  cadena.Estacion.estacion1.ToUpper().Contains(Search_Data.ToUpper()) || cadena.Temporada.nombre.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Anno.anno1.ToUpper().Contains(Search_Data.ToUpper()) || cadena.periodo.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Hotel.nombre_hotel.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Hotel":
                    exc = exc.OrderByDescending(cadena => cadena.Hotel.nombre_hotel);
                    break;
                default:
                    exc = exc.OrderBy(cadena => cadena.Hotel.nombre_hotel);
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

        // GET: /Temporada_Suplemento/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada_Suplemento temporada_suplemento = db.Temporada_Suplemento.Find(id);
            if (temporada_suplemento == null)
            {
                return HttpNotFound();
            }
            return View(temporada_suplemento);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Temporada_Suplemento/Create
        public ActionResult Create()
        {
            try { 
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1");
            ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel");
            ViewBag.idsuplemento = new SelectList(db.Suplemento, "idSuplemento", "suplemento1");
            ViewBag.idtipotarifa = new SelectList(db.Tipo_Tarifa, "idTipoTarifa", "tipo_tarifa1");
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

        // POST: /Temporada_Suplemento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idSuplementoHotel,periodo,inicio,fin,idanno,valor,idhotel,idsuplemento,idtipotarifa,idEstacion,idTemporada")] Temporada_Suplemento temporada_suplemento)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Temporada_Suplemento.Add(temporada_suplemento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporada_suplemento.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporada_suplemento.idEstacion);
            ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel", temporada_suplemento.idhotel);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporada_suplemento.idTemporada);
            return View(temporada_suplemento);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Temporada_Suplemento/Edit/5
        public ActionResult Edit(int? id)
        {
            try{           if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada_Suplemento temporada_suplemento = db.Temporada_Suplemento.Find(id);
            if (temporada_suplemento == null)
            {
                return HttpNotFound();
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporada_suplemento.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporada_suplemento.idEstacion);
            ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel", temporada_suplemento.idhotel);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporada_suplemento.idTemporada);
            return View(temporada_suplemento);
              }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Temporada_Suplemento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idSuplementoHotel,periodo,inicio,fin,idanno,valor,idhotel,idsuplemento,idtipotarifa,idEstacion,idTemporada")] Temporada_Suplemento temporada_suplemento)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(temporada_suplemento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporada_suplemento.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporada_suplemento.idEstacion);
            ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel", temporada_suplemento.idhotel);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporada_suplemento.idTemporada);
            return View(temporada_suplemento);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Temporada_Suplemento/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada_Suplemento temporada_suplemento = db.Temporada_Suplemento.Find(id);
            if (temporada_suplemento == null)
            {
                return HttpNotFound();
            }
            return View(temporada_suplemento);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Temporada_Suplemento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try {
            Temporada_Suplemento temporada_suplemento = db.Temporada_Suplemento.Find(id);
            int cant = db.NomTempSupHotel.ToList().Count;
            if (cant != 0)
            {
                foreach (var tipo in db.NomTempSupHotel.ToList())
                {
                    if (tipo.idSuplementoHotel == temporada_suplemento.idSuplementoHotel)
                    {
                        db.NomTempSupHotel.Remove(tipo);
                        db.SaveChanges();
                    }
                }
            }
            else
                if (cant == 0)
                {

                }
            db.Temporada_Suplemento.Remove(temporada_suplemento);
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
