using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.SqlServer.Server;
using PagedList;
using TripAgency.EF;
using TripAgency.Models;

namespace TripAgency.Controllers
{
    [Authorize] 
    public class Temporada_HotelController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Temporada_Hotel/
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
            var cadenas = from cadena in db.Temporada_Hotel select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cadenas = cadenas.Where(cadena => cadena.Anno.anno1.ToUpper().Contains(Search_Data.ToUpper()) ||
                    cadena.Hotel.nombre_hotel.ToUpper().Contains(Search_Data.ToUpper()) || cadena.Estacion.estacion1.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Temporada.nombre.ToUpper().Contains(Search_Data.ToUpper()) || cadena.periodo.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Hotel":
                    cadenas = cadenas.OrderByDescending(cadena => cadena.Hotel.nombre_hotel);
                    break;
                case "Anno":
                    cadenas = cadenas.OrderByDescending(cadena => cadena.Anno.anno1);
                    break;
                default:
                    cadenas = cadenas.OrderBy(cadena => cadena.Hotel.nombre_hotel);
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

        private void PopulateAssignedReduccionData(Temporada_Hotel hotel)
        {
            var allcaenas = db.Cadena;
            var viewModelcadenas = new List<Cadena_Asigned>();

            foreach (var tipo in allcaenas)
            {
                viewModelcadenas.Add(new Cadena_Asigned
                {
                    idCadena = tipo.idCadena,
                    cadena1 = tipo.cadena_,
                    assigned=false
                });
            }
            ViewBag.Cadenas = viewModelcadenas;
        }

        // GET: /Temporada_Hotel/Details/5
        public ActionResult Details(int? id)
        {
            try 
            { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Temporada_Hotel temporada_hotel = db.Temporada_Hotel.Find(id);
                if (temporada_hotel == null)
                {
                    return HttpNotFound();
                }
                PopulateAssignedReduccionData(temporada_hotel);
                return View(temporada_hotel);
                }
            catch (Exception e1)
            {
               string sms = e1.Message;
               return View("Error");
               throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int? id, string[] selectedCadenas)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Temporada_Hotel temporada_hotel = db.Temporada_Hotel.Find(id);
                if (temporada_hotel == null)
                {
                    return HttpNotFound();
                }
                else if (selectedCadenas != null)
                {
                    for (int i = 0; i < selectedCadenas.Length; i++)
                    {
                        var p = int.Parse(selectedCadenas[i]);
                        var cad = db.Cadena.Single(c => c.idCadena == p);
                        foreach (var hot in cad.Hotel)
                        {
                            if (hot.idHotel != temporada_hotel.idhotel)
                            {
                                if (hot.Suplemento_Hotel.ToList().Count != 0)
                                {
                                    var temp2 = new Temporada_Suplemento();
                                    temp2.periodo = temporada_hotel.periodo;
                                    temp2.inicio = temporada_hotel.inicio;
                                    temp2.fin = temporada_hotel.fin;
                                    temp2.idEstacion = temporada_hotel.idEstacion;
                                    temp2.idTemporada = temporada_hotel.idTemporada;
                                    temp2.idanno = temporada_hotel.idanno;
                                    temp2.idhotel = hot.idHotel;
                                    db.Temporada_Suplemento.Add(temp2);
                                    db.SaveChanges();
                                    if (temp2 != null)
                                    {
                                        foreach (var tipos in hot.Suplemento_Hotel.ToList())
                                        {
                                            var tempnom2 = new NomTempSupHotel();
                                            tempnom2.idsuplemento = (int) tipos.Suplemento.idSuplemento;
                                            tempnom2.Suplemento = tipos.Suplemento;
                                            tempnom2.idSuplementoHotel = temp2.idSuplementoHotel;
                                            tempnom2.tarifa = 0;
                                            tempnom2.idtipotarifa = 7;
                                            temp2.NomTempSupHotel.Add(tempnom2);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                                if (hot.Reduccion_Hotel.ToList().Count != 0)
                                {
                                    var temp3 = new Temporada_Reduccion();
                                    temp3.periodo = temporada_hotel.periodo;
                                    temp3.inicio = temporada_hotel.inicio;
                                    temp3.fin = temporada_hotel.fin;
                                    temp3.idEstacion = temporada_hotel.idEstacion;
                                    temp3.idtemporada = temporada_hotel.idTemporada;
                                    temp3.idanno = temporada_hotel.idanno;
                                    temp3.idhotel = hot.idHotel;
                                    db.Temporada_Reduccion.Add(temp3);
                                    db.SaveChanges();
                                    if (temp3 != null)
                                    {
                                        foreach (var tipor in hot.Reduccion_Hotel.ToList())
                                        {
                                            var tempnom3 = new NomTempRedHotel();
                                            tempnom3.idreduccion = (int) tipor.Reduccion.idReduccion;
                                            tempnom3.Reduccion = tipor.Reduccion;
                                            tempnom3.idTemporada_Reduccion = temp3.idTemporada_Reduccion;
                                            tempnom3.tarifa = 0;
                                            tempnom3.idtipotarifa = 7;
                                            temp3.NomTempRedHotel.Add(tempnom3);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                                if (hot.Tipol_HotelTipol.ToList().Count != 0)
                                {
                                    Temporada_Hotel temporada_hotelnew = new Temporada_Hotel();
                                    temporada_hotelnew.descripcion_periodo = temporada_hotel.descripcion_periodo;
                                    temporada_hotelnew.fin = temporada_hotel.fin;
                                    temporada_hotelnew.idEstacion = temporada_hotel.idEstacion;
                                    temporada_hotelnew.idTemporada = temporada_hotel.idTemporada;
                                    temporada_hotelnew.idanno = temporada_hotel.idanno;
                                    temporada_hotelnew.idhotel = hot.idHotel;
                                    temporada_hotelnew.inicio = temporada_hotel.inicio;
                                    temporada_hotelnew.periodo = temporada_hotel.periodo;
                                    temporada_hotelnew.tarifa = temporada_hotel.tarifa;
                                    db.Temporada_Hotel.Add(temporada_hotelnew);
                                    db.SaveChanges();
                                    if (hot != null)
                                    {
                                        foreach (var tipo in hot.Tipol_HotelTipol.ToList())
                                        {
                                            var tempnom1 = new NomTempTipHotel();
                                            tempnom1.idTiologia = (int) tipo.Tipologia.idTiologia;
                                            tempnom1.Tipologia = tipo.Tipologia;
                                            tempnom1.idTemporadaHotel = temporada_hotelnew.idTemporadaHotel;
                                            tempnom1.tarifa = 0;
                                            tempnom1.maxninno = 2;
                                            tempnom1.maxadult = 3;
                                            tempnom1.minninno = 0;
                                            tempnom1.minadult = 1;
                                            tempnom1.idtipotarifa = 7;
                                            temporada_hotel.NomTempTipHotel.Add(tempnom1);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                PopulateAssignedReduccionData(temporada_hotel);
                return RedirectToAction("Index");
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Temporada_Hotel/Create
        public ActionResult Create()
        {
            try 
                { 
                    ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1");
                    ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1");
                    ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel");
                    ViewBag.idtipologia = new SelectList(db.Tipologia, "idTiologia", "tipologia1");
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

        // POST: /Temporada_Hotel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idTemporadaHotel,periodo,inicio,fin,descripcion_periodo,idanno,idhotel,idEstacion,idTemporada")] Temporada_Hotel temporada_hotel)
        {
            try 
            { 
                if (ModelState.IsValid)
                {

                    int id = temporada_hotel.idhotel;
                    var h = db.Hotel.FirstOrDefault(c => c.idHotel == id);
                    var temp2=new Temporada_Suplemento();
                    temp2.periodo = temporada_hotel.periodo;
                    temp2.inicio = temporada_hotel.inicio;
                    temp2.fin = temporada_hotel.fin;
                    temp2.idEstacion = temporada_hotel.idEstacion;
                    temp2.idTemporada = temporada_hotel.idTemporada;
                    temp2.idanno = temporada_hotel.idanno;
                    temp2.idhotel = temporada_hotel.idhotel;
                    db.Temporada_Suplemento.Add(temp2);
                    db.SaveChanges();
                    if (temp2 != null)
                    {
                        int cantS = h.Suplemento_Hotel.ToList().Count;
                        if (cantS != 0)
                        {
                            foreach (var tipos in h.Suplemento_Hotel.ToList())
                            {
                                var tempnom2 = new NomTempSupHotel();
                                tempnom2.idsuplemento = (int)tipos.Suplemento.idSuplemento;
                                tempnom2.Suplemento = tipos.Suplemento;
                                tempnom2.idSuplementoHotel = temp2.idSuplementoHotel;
                                tempnom2.tarifa = 0;
                                tempnom2.idtipotarifa = 7;
                                temp2.NomTempSupHotel.Add(tempnom2);
                                db.SaveChanges();
                            }
                        }
                        else
                            if (cantS == 0)
                            {

                            }
                     }

                    var temp3 = new Temporada_Reduccion();
                    temp3.periodo = temporada_hotel.periodo;
                    temp3.inicio = temporada_hotel.inicio;
                    temp3.fin = temporada_hotel.fin;
                    temp3.idEstacion = temporada_hotel.idEstacion;
                    temp3.idtemporada = temporada_hotel.idTemporada;
                    temp3.idanno = temporada_hotel.idanno;
                    temp3.idhotel = temporada_hotel.idhotel;
                    db.Temporada_Reduccion.Add(temp3);
                    db.SaveChanges();
                    if (temp3 != null)
                    {
                        int cantR = h.Reduccion_Hotel.ToList().Count;
                        if (cantR != 0)
                        {
                            foreach (var tipor in h.Reduccion_Hotel.ToList())
                            {
                                var tempnom3 = new NomTempRedHotel();
                                tempnom3.idreduccion = (int)tipor.Reduccion.idReduccion;
                                tempnom3.Reduccion = tipor.Reduccion;
                                tempnom3.idTemporada_Reduccion = temp3.idTemporada_Reduccion;
                                tempnom3.tarifa = 0;
                                tempnom3.idtipotarifa = 7;
                                temp3.NomTempRedHotel.Add(tempnom3);
                                db.SaveChanges();
                            }
                        }
                        else
                            if (cantR == 0)
                            {

                            }
                    }
                    temporada_hotel.tarifa = 0;
                    db.Temporada_Hotel.Add(temporada_hotel);
                    db.SaveChanges();
                    if (h != null)
                    {
                        int cant = h.Tipol_HotelTipol.ToList().Count;
                        if (cant != 0)
                        {
                            foreach (var tipo in h.Tipol_HotelTipol.ToList())
                            {
                                var tempnom1 = new NomTempTipHotel();
                                tempnom1.idTiologia = (int)tipo.Tipologia.idTiologia;
                                tempnom1.Tipologia = tipo.Tipologia;
                                tempnom1.idTemporadaHotel = temporada_hotel.idTemporadaHotel;
                                tempnom1.tarifa = 0;
                                tempnom1.maxninno = 2;
                                tempnom1.maxadult = 3;
                                tempnom1.minninno = 0;
                                tempnom1.minadult = 1;
                                tempnom1.idtipotarifa = 7;
                                temporada_hotel.NomTempTipHotel.Add(tempnom1);
                                db.SaveChanges();
                            }
                        }
                        else
                            if (cant == 0)
                            {

                            }
                    }
                    return RedirectToAction("Create");
                }

                ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporada_hotel.idanno);
                ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporada_hotel.idEstacion);
                ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel", temporada_hotel.idhotel);
                ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporada_hotel.idTemporada);
                return View(temporada_hotel);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Temporada_Hotel/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada_Hotel temporada_hotel = db.Temporada_Hotel.Find(id);
            if (temporada_hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporada_hotel.idanno);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporada_hotel.idEstacion);
            ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel", temporada_hotel.idhotel);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporada_hotel.idTemporada);
            return View(temporada_hotel);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Temporada_Hotel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idTemporadaHotel,periodo,inicio,fin,descripcion_periodo,idanno,idhotel,idEstacion,idTemporada")] Temporada_Hotel temporada_hotel)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    var tht = db.Temporada_Hotel.Single(p => p.idTemporadaHotel == temporada_hotel.idTemporadaHotel);
                    if (tht.Hotel.Temporada_Suplemento.ToList().Count != 0)
                    {
                        foreach (var supl in tht.Hotel.Temporada_Suplemento)
                        {
                            if (supl.periodo.Equals(tht.periodo) && supl.inicio.Equals(tht.inicio) && supl.fin.Equals(tht.fin) && supl.idEstacion == tht.idEstacion && supl.idanno == tht.idanno && supl.idTemporada == tht.idTemporada)
                            {
                                supl.periodo = temporada_hotel.periodo;
                                supl.inicio = temporada_hotel.inicio;
                                supl.fin = temporada_hotel.fin;
                                supl.idEstacion = temporada_hotel.idEstacion;
                                supl.idanno = temporada_hotel.idanno;
                                supl.idTemporada = temporada_hotel.idTemporada;
                                db.Entry(supl).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    if (tht.Hotel.Temporada_Reduccion.ToList().Count != 0)
                    {
                        foreach (var red in tht.Hotel.Temporada_Reduccion)
                        {
                            if (red.periodo.Equals(tht.periodo) && red.inicio.Equals(tht.inicio) && red.fin.Equals(tht.fin) && red.idEstacion == tht.idEstacion && red.idanno == tht.idanno && red.idtemporada == tht.idTemporada)
                            {
                                red.periodo = temporada_hotel.periodo;
                                red.inicio = temporada_hotel.inicio;
                                red.fin = temporada_hotel.fin;
                                red.idEstacion = temporada_hotel.idEstacion;
                                red.idanno = temporada_hotel.idanno;
                                red.idtemporada = temporada_hotel.idTemporada;
                                db.Entry(red).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    tht.tarifa = 0;
                    tht.periodo = temporada_hotel.periodo;
                    tht.inicio = temporada_hotel.inicio;
                    tht.fin = temporada_hotel.fin;
                    tht.idanno = temporada_hotel.idanno;
                    tht.idTemporada = temporada_hotel.idTemporada;
                    tht.idEstacion = temporada_hotel.idEstacion;
                    db.Entry(tht).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporada_hotel.idanno);
                ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporada_hotel.idEstacion);
                ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel", temporada_hotel.idhotel);
                ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporada_hotel.idTemporada);
                return View(temporada_hotel);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Temporada_Hotel/Delete/5
        public ActionResult Delete(int? id)
        {
            try 
            { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Temporada_Hotel temporada_hotel = db.Temporada_Hotel.Find(id);
                if (temporada_hotel == null)
                {
                    return HttpNotFound();
                }
                return View(temporada_hotel);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Temporada_Hotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
                    Temporada_Hotel temporada_hotel = db.Temporada_Hotel.Find(id);
                    //Suplemneto
                    foreach (var tipo in db.Temporada_Suplemento)
                    {
                        if (tipo.idEstacion == temporada_hotel.idEstacion && tipo.idTemporada == temporada_hotel.idTemporada && tipo.idhotel == temporada_hotel.idhotel && tipo.idanno == temporada_hotel.idanno && tipo.fin.Equals(temporada_hotel.fin) && tipo.inicio.Equals(temporada_hotel.inicio) && tipo.periodo.Equals(temporada_hotel.periodo))
                        {
                           int cantS = db.NomTempSupHotel.ToList().Count;
                           //Taxes for this Sup
                           if (cantS != 0)
                           {
                               foreach (var tipo1 in db.NomTempSupHotel.ToList())
                               {
                                   if (tipo1.idSuplementoHotel == tipo.idSuplementoHotel)
                                   {
                                       db.NomTempSupHotel.Remove(tipo1);
                                       db.SaveChanges();
                                   }
                               }
                           }
                           else
                               if (cantS == 0)
                               {

                               }
                           db.Temporada_Suplemento.Remove(tipo);
                           db.SaveChanges();
                       }
                    }
                    //Reduccion
                    foreach (var tipo in db.Temporada_Reduccion)
                    {
                        if (tipo.idEstacion == temporada_hotel.idEstacion && tipo.idtemporada == temporada_hotel.idTemporada && tipo.idhotel == temporada_hotel.idhotel && tipo.idanno == temporada_hotel.idanno && tipo.fin.Equals(temporada_hotel.fin) && tipo.inicio.Equals(temporada_hotel.inicio) && tipo.periodo.Equals(temporada_hotel.periodo))
                        {
                           int cantR = db.NomTempRedHotel.ToList().Count;
                           //Taxes for this Red
                           if (cantR != 0)
                           {
                               foreach (var tipo1 in db.NomTempRedHotel.ToList())
                               {
                                   if (tipo1.idTemporada_Reduccion == tipo.idTemporada_Reduccion)
                                   {
                                       db.NomTempRedHotel.Remove(tipo1);
                                       db.SaveChanges();
                                   }
                               }
                           }
                           else
                               if (cantR == 0)
                               {

                               }
                           db.Temporada_Reduccion.Remove(tipo);
                           db.SaveChanges();
                        }
                    }
                    int cant = db.NomTempTipHotel.ToList().Count;
                    //Taxes for this Tipology
                    if (cant != 0)
                    {
                        foreach (var tipo in db.NomTempTipHotel.ToList())
                        {
                            if (tipo.idTemporadaHotel == temporada_hotel.idTemporadaHotel)
                            {
                                db.NomTempTipHotel.Remove(tipo);
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                        if (cant == 0)
                        {

                        }
                    db.Temporada_Hotel.Remove(temporada_hotel);
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
        /// <summary>
        /// Tipologia per
        /// </summary>
        /// <param name="idhotel"></param>
        /// <param name="idanno"></param>
        /// <param name="idEstacion"></param>
        /// <param name="idTemporada"></param>
        /// <param name="idtipotarifa"></param>
        /// <returns></returns>
        public JsonResult periodos(string idhotel, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaHotelModel.getInstance().GetPeriodosHotel(idhotel,idanno,idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Tipologia Tar
        /// </summary>
        /// <param name="idhotel"></param>
        /// <param name="idanno"></param>
        /// <param name="idEstacion"></param>
        /// <param name="idTemporada"></param>
        /// <param name="idtipotarifa"></param>
        /// <returns></returns>
        public JsonResult tipotarifa(string idhotel, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaHotelModel.getInstance().Gettipotarifa(idhotel, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult updateTarifaTip(string[] tarifas, int idTipoTarifa)
        {
            var carsList = "";
            bool update = TarifaHotelModel.getInstance().updateTipolTarifa(tarifas, idTipoTarifa);
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

        /// <summary>
        /// Suplemento per
        /// </summary>
        /// <param name="idhotel"></param>
        /// <param name="idanno"></param>
        /// <param name="idEstacion"></param>
        /// <param name="idTemporada"></param>
        /// <param name="idtipotarifa"></param>
        /// <returns></returns>
        public JsonResult periodosS(string idhotel, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaHotelModel.getInstance().GetPeriodosHotelS(idhotel, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Suplemento Tar
        /// </summary>
        /// <param name="idhotel"></param>
        /// <param name="idanno"></param>
        /// <param name="idEstacion"></param>
        /// <param name="idTemporada"></param>
        /// <param name="idtipotarifa"></param>
        /// <returns></returns>
        public JsonResult tipotarifaS(string idhotel, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaHotelModel.getInstance().GettipotarifaS(idhotel, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult updateTarifaSup(string[] tarifas, int idTipoTarifa)
        {
            var carsList = "";
            bool update = TarifaHotelModel.getInstance().updateSupleTarifa(tarifas, idTipoTarifa);
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

        /// <summary>
        /// Suplemento per
        /// </summary>
        /// <param name="idhotel"></param>
        /// <param name="idanno"></param>
        /// <param name="idEstacion"></param>
        /// <param name="idTemporada"></param>
        /// <param name="idtipotarifa"></param>
        /// <returns></returns>
        public JsonResult periodosR(string idhotel, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaHotelModel.getInstance().GetPeriodosHotelR(idhotel, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Suplemento Tar
        /// </summary>
        /// <param name="idhotel"></param>
        /// <param name="idanno"></param>
        /// <param name="idEstacion"></param>
        /// <param name="idTemporada"></param>
        /// <param name="idtipotarifa"></param>
        /// <returns></returns>
        public JsonResult tipotarifaR(string idhotel, string idanno, string idEstacion)
        {
            List<string> carsList = TarifaHotelModel.getInstance().GettipotarifaR(idhotel, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult updateTarifaRed(string[] tarifas, int idTipoTarifa)
        {
            var carsList = "";
            bool update = TarifaHotelModel.getInstance().updateReduTarifa(tarifas, idTipoTarifa);
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

        public JsonResult planBase(string idhotel, string idanno, string idEstacion)
        {
            string carsList = TarifaHotelModel.getInstance().GetPlanBase(idhotel, idanno, idEstacion);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Tarifa_Hotel()
        {
            try { 
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1");
            ViewBag.idhotel = new SelectList(db.Hotel, "idHotel", "nombre_hotel");
            ViewBag.idTipoTarifa = new SelectList(db.Tipo_Tarifa, "idTipoTarifa", "tipo_tarifa1");
            
            return View(db.Temporada_Hotel.ToList());
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
