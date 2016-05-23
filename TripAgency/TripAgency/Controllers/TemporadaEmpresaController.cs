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
    public class TemporadaEmpresaController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /TemporadaEmpresa/
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
            var cadenas = from cadena in db.TemporadaEmpresa select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cadenas = cadenas.Where(cadena => cadena.Anno.anno1.ToUpper().Contains(Search_Data.ToUpper()) ||
                    cadena.Empresa.nombre_empresa.ToUpper().Contains(Search_Data.ToUpper()) || cadena.Estacion.estacion1.ToUpper().Contains(Search_Data.ToUpper())
                    || cadena.Temporada.nombre.ToUpper().Contains(Search_Data.ToUpper()) || cadena.periodo.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    cadenas = cadenas.OrderByDescending(cadena => cadena.Empresa.nombre_empresa);
                    break;
                default:
                    cadenas = cadenas.OrderBy(cadena => cadena.Empresa.nombre_empresa);
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

        private void PopulateAssignedEmpresaData()
        {
            var listEmpresaT = new List<Empresa>();
            foreach (var empresa in db.Empresa)
            {
                foreach (var tipo in empresa.Empresa_TipoEmpresa)
                {
                    if (tipo.Tipo_Empresa.nombre.Equals("transportista"))
                    {
                        listEmpresaT.Add(empresa);
                    }
                }
            }
            var viewModelcadenas = new List<Empresa_Asigned>();

            foreach (var tipo in listEmpresaT)
            {
                viewModelcadenas.Add(new Empresa_Asigned
                {
                    idEmpresa = tipo.idEmpresa,
                    empresa1 = tipo.nombre_empresa,
                    assigned = false
                });
            }
            ViewBag.Empresas = viewModelcadenas;
        }

        // GET: /TemporadaEmpresa/Details/5
        public ActionResult Details(int? id)
        {
            try
            { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TemporadaEmpresa temporadaempresa = db.TemporadaEmpresa.Find(id);
                if (temporadaempresa == null)
                {
                    return HttpNotFound();
                }
                PopulateAssignedEmpresaData();
                return View(temporadaempresa);
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
        public ActionResult Details(int? id, string[] selectedEmpresas)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TemporadaEmpresa temporada_emp = db.TemporadaEmpresa.Find(id);
                if (temporada_emp == null)
                {
                    return HttpNotFound();
                }
                else if (selectedEmpresas != null)
                {
                    for (int i = 0; i < selectedEmpresas.Length; i++)
                    {
                        var p = int.Parse(selectedEmpresas[i]);
                        var cad = db.Empresa.Single(c => c.idEmpresa == p);
                        var tempnom2 = new TemporadaEmpresa();
                        tempnom2.periodo = temporada_emp.periodo;
                        tempnom2.inicio = temporada_emp.inicio;
                        tempnom2.fin = temporada_emp.fin;
                        tempnom2.descripcion_periodo = temporada_emp.descripcion_periodo;
                        tempnom2.idanno = temporada_emp.idanno;
                        tempnom2.idEmpresa = cad.idEmpresa;
                        tempnom2.idEstacion = temporada_emp.idEstacion;
                        tempnom2.idTemporada = temporada_emp.idTemporada;
                        cad.TemporadaEmpresa.Add(tempnom2);
                        db.SaveChanges();
                    }
                }
                PopulateAssignedEmpresaData();
                return RedirectToAction("Index");
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaEmpresa/Create
        public ActionResult Create()
        {
            try
            {
                var listEmpresaT = new List<Empresa>();
                foreach (var empresa in db.Empresa)
                {
                    foreach (var tipo in empresa.Empresa_TipoEmpresa)
                    {
                        if (tipo.Tipo_Empresa.nombre.Equals("transportista"))
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

        // POST: /TemporadaEmpresa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idTemporadaEmpresa,periodo,inicio,fin,descripcion_periodo,idanno,idEmpresa,idEstacion,idTemporada")] TemporadaEmpresa temporadaempresa)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.TemporadaEmpresa.Add(temporadaempresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadaempresa.idanno);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", temporadaempresa.idEmpresa);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadaempresa.idEstacion);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadaempresa.idTemporada);
            return View(temporadaempresa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaEmpresa/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaEmpresa temporadaempresa = db.TemporadaEmpresa.Find(id);
            if (temporadaempresa == null)
            {
                return HttpNotFound();
            }
            var listEmpresaT = new List<Empresa>();
            foreach (var empresa in db.Empresa)
            {
                foreach (var tipo in empresa.Empresa_TipoEmpresa)
                {
                    if (tipo.Tipo_Empresa.nombre.Equals("transportista"))
                    {
                        listEmpresaT.Add(empresa);
                    }
                }
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadaempresa.idanno);
            ViewBag.idEmpresa = new SelectList(listEmpresaT, "idEmpresa", "nombre_empresa", temporadaempresa.idEmpresa);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadaempresa.idEstacion);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadaempresa.idTemporada);
            return View(temporadaempresa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TemporadaEmpresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idTemporadaEmpresa,periodo,inicio,fin,descripcion_periodo,idanno,idEmpresa,idEstacion,idTemporada")] TemporadaEmpresa temporadaempresa)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(temporadaempresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idanno = new SelectList(db.Anno, "IdAnno", "anno1", temporadaempresa.idanno);
            ViewBag.idEmpresa = new SelectList(db.Empresa, "idEmpresa", "nombre_empresa", temporadaempresa.idEmpresa);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "estacion1", temporadaempresa.idEstacion);
            ViewBag.idTemporada = new SelectList(db.Temporada, "idtemporada", "nombre", temporadaempresa.idTemporada);
            return View(temporadaempresa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /TemporadaEmpresa/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporadaEmpresa temporadaempresa = db.TemporadaEmpresa.Find(id);
            if (temporadaempresa == null)
            {
                return HttpNotFound();
            }
            return View(temporadaempresa);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /TemporadaEmpresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            TemporadaEmpresa temporadaempresa = db.TemporadaEmpresa.Find(id);
            db.TemporadaEmpresa.Remove(temporadaempresa);
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
