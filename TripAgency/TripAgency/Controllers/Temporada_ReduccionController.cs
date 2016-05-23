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

namespace TripAgency.Controllers
{
     [Authorize] 
    public class Temporada_ReduccionController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Temporada_Reduccion/
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
            var exc = from cadena in db.Temporada_Reduccion select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                exc = exc.Where(cadena =>  cadena.Estacion.estacion1.ToUpper().Contains(Search_Data.ToUpper()) || cadena.Temporada.nombre.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.periodo.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Hotel.nombre_hotel.ToUpper().Contains(Search_Data.ToUpper()) || cadena.Anno.anno1.ToUpper().Contains(Search_Data.ToUpper()));
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

        // GET: /Temporada_Reduccion/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada_Reduccion temporada_reduccion = db.Temporada_Reduccion.Find(id);
            if (temporada_reduccion == null)
            {
                return HttpNotFound();
            }
            return View(temporada_reduccion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Temporada_Reduccion/Create
        public ActionResult Create()
        {
            try { 
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1");
            ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel");
            ViewBag.idreduccion = new SelectList(db.Reduccion, "idReduccion", "reduccion1");
            ViewBag.idtemporada = new SelectList(db.Temporada, "idtemporada", "nombre");
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Temporada_Reduccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idTemporada_Reduccion,periodo,inicio,fin,idtemporada,idanno,valor,idhotel,idreduccion,idEstacion")] Temporada_Reduccion temporada_reduccion)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Temporada_Reduccion.Add(temporada_reduccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporada_reduccion.idEstacion);
            ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel", temporada_reduccion.idhotel);
            ViewBag.idtemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporada_reduccion.idtemporada);
            return View(temporada_reduccion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Temporada_Reduccion/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada_Reduccion temporada_reduccion = db.Temporada_Reduccion.Find(id);
            if (temporada_reduccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporada_reduccion.idEstacion);
            ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel", temporada_reduccion.idhotel);
            ViewBag.idtemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporada_reduccion.idtemporada);
            return View(temporada_reduccion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Temporada_Reduccion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idTemporada_Reduccion,periodo,inicio,fin,idtemporada,idanno,valor,idhotel,idreduccion,idEstacion")] Temporada_Reduccion temporada_reduccion)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(temporada_reduccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporada_reduccion.idEstacion);
            ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel", temporada_reduccion.idhotel);
            ViewBag.idtemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporada_reduccion.idtemporada);
            return View(temporada_reduccion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Temporada_Reduccion/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada_Reduccion temporada_reduccion = db.Temporada_Reduccion.Find(id);
            if (temporada_reduccion == null)
            {
                return HttpNotFound();
            }
            return View(temporada_reduccion);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Temporada_Reduccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Temporada_Reduccion temporada_reduccion = db.Temporada_Reduccion.Find(id);
            int cant = db.NomTempRedHotel.ToList().Count;
            if (cant != 0)
            {
                foreach (var tipo in db.NomTempRedHotel.ToList())
                {
                    if (tipo.idTemporada_Reduccion == temporada_reduccion.idTemporada_Reduccion)
                    {
                        db.NomTempRedHotel.Remove(tipo);
                        db.SaveChanges();
                    }
                }
            }
            else
                if (cant == 0)
                {

                }
            db.Temporada_Reduccion.Remove(temporada_reduccion);
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
