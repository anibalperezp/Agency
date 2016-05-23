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
    public class TarifaCarroController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /TarifaCarro/
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
            var cadenas = from cadena in db.TarifaCarro select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cadenas = cadenas.Where(cadena => cadena.Empresa.nombre_empresa.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Tipologia_Carro.nombre.Contains(Search_Data.ToUpper()) || cadena.Temporada.nombre.Contains(Search_Data.ToUpper())
                    || cadena.TemporadaEmpresa.periodo.Contains(Search_Data.ToUpper()) || cadena.Anno.anno1.Contains(Search_Data.ToUpper())
                    || cadena.Empresa.nombre_empresa.Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Empresa":
                    cadenas = cadenas.OrderByDescending(cadena => cadena.Empresa.nombre_empresa);
                    break;
                default:
                    cadenas = cadenas.OrderBy(cadena => cadena.Empresa.nombre_empresa);
                    break;
            }
            int Size_Of_Page = 10;
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

        // GET: /TarifaCarro/Details/5
        public ActionResult Details(int? id)
        {
            try{
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarifaCarro tarifacarro = db.TarifaCarro.Find(id);
            if (tarifacarro == null)
            {
                return HttpNotFound();
            }
            return View(tarifacarro);
              }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TarifaCarro/Create
        public ActionResult Create()
        {
            try { 
            var tarifa = new TarifaCarro();
            tarifa.Rangos_Tarifa = new List<Rangos_Tarifa>();
            PopulateTarifaCarros(tarifa);
            ViewBag.idAnno = new SelectList(db.Anno, "IdAnno", "anno1");
            ViewBag.idcarro = new SelectList(db.Carro, "idCarro", "name");
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa");
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre");
            ViewBag.idtipologia_carro = new SelectList(db.Tipologia_Carro, "idtipologia_carro", "nombre");
            ViewBag.idTemporadaEmpresa = new SelectList(db.TemporadaEmpresa, "idTemporadaEmpresa", "periodo");
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        private void PopulateTarifaCarros(TarifaCarro tarifa)
        {
            var allrangos = db.Rango;
            var tarifaRangos = new HashSet<int?>(tarifa.Rangos_Tarifa.Select(c => c.idrango));
            var viewModel = new List<Tarifa_Rangos_Asigned>();
            foreach (var tipo in allrangos)
            {
                viewModel.Add(new Tarifa_Rangos_Asigned
                {
                    idRano = tipo.idrango,
                    rango = tipo.rango1,
                    assigned = tarifaRangos.Contains(tipo.idrango),
                });
            }
            ViewBag.Courses = viewModel;
        }

        private void PopulateTarifaCarrosEdit(TarifaCarro tarifa)
        {
            var allrangos = tarifa.Rangos_Tarifa;
            var tarifaRangos = new HashSet<int?>(tarifa.Rangos_Tarifa.Select(c => c.idrango));
            var viewModel = new List<Tarifa_Rangos_Asigned>();
            foreach (var tipo in allrangos)
            {
                viewModel.Add(new Tarifa_Rangos_Asigned
                {
                    idRano = (int) tipo.idrango,
                    rango = tipo.Rango.rango1,
                    assigned = tarifaRangos.Contains(tipo.idrango),
                    tarifa = (double) tipo.tarifa
                });
            }
            ViewBag.Courses = viewModel;
        }

        public JsonResult periodos(string idanno, string idTemporada, string idEmpresa)
        {
            List<string> carsList = new TarifaCarroModel().GetPeriodosDeTarifaCarro(idanno, idTemporada, idEmpresa);
            return Json(carsList, JsonRequestBehavior.AllowGet);
        }

        // POST: /TarifaCarro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idcondtarifa,valor,seguro,deposito,descripcion,idAnno,idTemporada,idtipologia_carro,idcarro,idEmpresa,idTemporadaEmpresa")] TarifaCarro tarifacarro, string[] selectedRangos, string[] selectedTarifas, string State)
        {
            try { 
            if (ModelState.IsValid)
            {
                var sub = db.TemporadaEmpresa.FirstOrDefault(c => c.periodo == State);
                tarifacarro.idTemporadaEmpresa = sub.idTemporadaEmpresa;
                db.TarifaCarro.Add(tarifacarro);
                db.SaveChanges();
                if (selectedRangos != null)
                {
                    tarifacarro.Rangos_Tarifa = new List<Rangos_Tarifa>();
                    for (int i = 0; i < selectedRangos.Length; i++)
                    {
                        var tph = new Rangos_Tarifa();
                        tph.idrango = int.Parse(selectedRangos[i]);
                        tph.idcondtarifa = tarifacarro.idcondtarifa;
                        tph.asignado = true;
                        tph.tarifa = float.Parse(selectedTarifas[i]);
                        tarifacarro.Rangos_Tarifa.Add(tph);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.idAnno = new SelectList(db.Anno, "IdAnno", "anno1", tarifacarro.idAnno);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", tarifacarro.idEmpresa);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", tarifacarro.idTemporada);
            ViewBag.idtipologia_carro = new SelectList(db.Tipologia_Carro, "idtipologia_carro", "nombre", tarifacarro.idtipologia_carro);
            ViewBag.idTemporadaEmpresa = new SelectList(db.TemporadaEmpresa, "idTemporadaEmpresa", "periodo", tarifacarro.idTemporadaEmpresa);
            PopulateTarifaCarros(tarifacarro);
            return View(tarifacarro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TarifaCarro/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarifaCarro tarifacarro = db.TarifaCarro.Find(id);
            PopulateTarifaCarrosEdit(tarifacarro);
            if (tarifacarro == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAnno = new SelectList(db.Anno, "IdAnno", "anno1", tarifacarro.idAnno);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", tarifacarro.idEmpresa);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", tarifacarro.idTemporada);
            ViewBag.idtipologia_carro = new SelectList(db.Tipologia_Carro, "idtipologia_carro", "nombre", tarifacarro.idtipologia_carro);
            ViewBag.idTemporadaEmpresa = new SelectList(db.TemporadaEmpresa, "idTemporadaEmpresa", "periodo", tarifacarro.idTemporadaEmpresa);
            return View(tarifacarro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        //----**************************------------------***************-------------
        private void UpdateInstructorCourses(string[] selectedTarifas,string[] selectedRangos, TarifaCarro tarifaToUpdate)
        {
            if (selectedRangos == null)
            {
                tarifaToUpdate.Rangos_Tarifa = new List<Rangos_Tarifa>();
                return;
            }
            var selectedRTarfs = selectedRangos.ToList();
            List<int?> listint = selectedRTarfs.Select(tenp => Convert.ToInt32(tenp)).Select(dummy => (int?) dummy).ToList();
            var instructorCourses = new HashSet<int?>(tarifaToUpdate.Rangos_Tarifa.Select(c => c.idrango));
            var listHotelTipologias = instructorCourses.ToList();
            for (int i = 0; i < listint.Count; i++)
            {
                if (listHotelTipologias.Contains(listint[i]) == false)
                {
                    var tph = new Rangos_Tarifa();
                    tph.idrango = listint[i];
                    tph.idcondtarifa = tarifaToUpdate.idcondtarifa;
                    tph.asignado = true;
                    tph.tarifa = float.Parse(selectedTarifas[i]);
                    tarifaToUpdate.Rangos_Tarifa.Add(tph);
                }
            }
            var listHTs = tarifaToUpdate.Rangos_Tarifa.ToList();

            for (int j = 0; j < listHTs.Count; j++)
            {
                if (listint.Contains(listHTs[j].idrango) == false)
                {
                    tarifaToUpdate.Rangos_Tarifa.Remove(listHTs[j]);
                }
                else
                    if (listint.Contains(listHTs[j].idrango) == true)
                    {
                        listHTs[j].tarifa = float.Parse(selectedTarifas[j]);
                        db.Entry(listHTs[j]).State = EntityState.Modified;
                        db.SaveChanges();
                    }
            }
        }

        // POST: /TarifaCarro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idcondtarifa,valor,seguro,deposito,descripcion,idAnno,idTemporada,idtipologia_carro,idcarro,idEmpresa,idTemporadaEmpresa")] TarifaCarro tarifacarro, string[] selectedRangos, string State, string[] selectedTarifas)
        {
            try { 
            var tarifaToUpdate = db.TarifaCarro.Include(i => i.Rangos_Tarifa).Single(i => i.idcondtarifa == tarifacarro.idcondtarifa);
            if (ModelState.IsValid)
            {
                if (State != null)
                {
                    var sub = db.TemporadaEmpresa.FirstOrDefault(c => c.periodo == State);
                    tarifaToUpdate.idTemporadaEmpresa = sub.idTemporadaEmpresa;
                }
                tarifaToUpdate.deposito = tarifacarro.deposito;
                tarifaToUpdate.descripcion = tarifacarro.descripcion;
                tarifaToUpdate.idAnno = tarifacarro.idAnno;
                tarifaToUpdate.idEmpresa = tarifacarro.idEmpresa;
                tarifaToUpdate.idTemporada = tarifacarro.idTemporada;
                tarifaToUpdate.idtipologia_carro = tarifacarro.idtipologia_carro;
                tarifaToUpdate.seguro = tarifacarro.seguro;
                tarifaToUpdate.valor = tarifacarro.valor;
                UpdateInstructorCourses(selectedTarifas, selectedRangos, tarifaToUpdate);
                db.Entry(tarifaToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAnno = new SelectList(db.Anno, "IdAnno", "anno1", tarifacarro.idAnno);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", tarifacarro.idEmpresa);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", tarifacarro.idTemporada);
            ViewBag.idtipologia_carro = new SelectList(db.Tipologia_Carro, "idtipologia_carro", "nombre", tarifacarro.idtipologia_carro);
            ViewBag.idTemporadaEmpresa = new SelectList(db.TemporadaEmpresa, "idTemporadaEmpresa", "periodo", tarifacarro.idTemporadaEmpresa);
            PopulateTarifaCarrosEdit(tarifacarro);
            return View(tarifacarro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TarifaCarro/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarifaCarro tarifacarro = db.TarifaCarro.Find(id);
            if (tarifacarro == null)
            {
                return HttpNotFound();
            }
            else
            {
                int cant = tarifacarro.Rangos_Tarifa.ToList().Count;
                if (cant != 0)
                {
                    foreach (var tipo in tarifacarro.Rangos_Tarifa.ToList())
                    {
                        tarifacarro.Rangos_Tarifa.Remove(tipo);
                    }
                }
                else
                    if (cant == 0)
                    {

                    }
            }
            return View(tarifacarro);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TarifaCarro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            TarifaCarro tarifacarro = db.TarifaCarro.Find(id);
            int cant = tarifacarro.Rangos_Tarifa.ToList().Count;
            if (cant != 0)
            {
                foreach (var tipo in tarifacarro.Rangos_Tarifa.ToList())
                {
                    tarifacarro.Rangos_Tarifa.Remove(tipo);
                    db.SaveChanges();
                }
            }
            else
                if (cant == 0)
                {

                }
            db.TarifaCarro.Remove(tarifacarro);
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
