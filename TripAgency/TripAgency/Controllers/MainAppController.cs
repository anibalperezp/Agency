using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PagedList;
using TripAgency.EF;
using TripAgency.Models;

namespace TripAgency.Controllers
{
    public class MainAppController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();
        private static string destin1 = "";
        private static string cat1 = "";
        private static string priceH = "";
        private static string model1 = "";
        private static string subdestin1 = "";
        private static Hotel htemp = null;
        private static Paquete ptemp = null;
        private static Excursion etemp = null;
        private static Carro ctemp = null;
        private static AlojamientoCarModel alojCarro = null;

        #region Main Menus

        #region Index

        public ActionResult Index()
        {
            try { 
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        [HttpPost]
        public ActionResult Index(string destiniy, string category, string model, string ofice1, string ofice2,string start, string end, string destinyex, string excursion)
        {
            try { 
                    #region destiny != null

                    if (destiniy != null)
                    {
                        if (!destiniy.Equals("Choose One"))
                        {
                            destin1 = destiniy;
                            return RedirectToAction("Hotels");
                        }
                    }
                    #endregion

                    #region category and model != null

                    if (category != null && model != null)
                    {
                        if (!category.Equals("Choose One") && model.Equals("Choose One"))
                        {
                            cat1 = category;
                            return RedirectToAction("Cars");
                        }
                        else 
                            if (!category.Equals("Choose One") && !model.Equals("Choose One"))
                            {
                                if (!ofice1.Equals("Choose One") && !ofice2.Equals("Choose One"))
                                {
                                    cat1 = category;
                                    model1 = model;
                                    alojCarro = new AlojamientoCarModel(ofice1, ofice2, start, end);
                                    return RedirectToAction("CarCategory");
                                }
                            }
                    }

                    #endregion

                    #region excurion != null

                    if (excursion != null)
                    {
                        if (!excursion.Equals("Choose One"))
                        {
                            destin1 = destinyex;
                            var exc = db.Excursion.ToList().FirstOrDefault(i => i.nombre_excursion == excursion);
                            if (exc != null)
                            {
                                etemp = exc;
                            }
                            return RedirectToAction("Excursion");
                        }
                        else if (!destinyex.Equals("Choose One"))
                        {
                            destin1 = destinyex;
                            return RedirectToAction("Excursions");
                        }
                    }

                    #endregion

                    return View();
                    }
                    catch (Exception e1)
                    {
                        string sms = e1.Message;
                        return View("Error");
                        throw;
                    }
        }

        #endregion

        #region Destination

        public ActionResult Destinies()
        {
            try { 
            var list = new List<Subdestino>();
            var listh = new List<Hotel>();
            var liste = new List<Excursion>();
            foreach (var dest in db.Destino.ToList())
            {
                foreach (var hot in dest.Hotel)
                {
                    listh.Add(hot);
                }
                foreach (var exc in dest.Excursion)
                {
                    liste.Add(exc);
                }
            }
            ViewBag.count = list.Count.ToString();
            ViewBag.counth = listh.Count.ToString();
            ViewBag.counte = liste.Count.ToString();
            return View(db.Destino.ToList());
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult Destiny(string destiny)
        {
            try { 
            ViewBag.destiny = destiny;
            destin1 = destiny;
            var destProv = db.Destino.ToList().FirstOrDefault(i => i.destino1 == destiny);
            return View(destProv);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        #endregion

        #region Menu Car

        public ActionResult Cars(string category, string Sorting_Order, string Search_Data, string Filter_Value,int? Page_No)
        {
            try { 
            int Size_Of_Page = 0;
            int No_Of_Page = 0;
            var subdests = new List<Carro>();
            if (category == null)
            {
                var destProv = db.Tipologia_Carro.ToList().FirstOrDefault(i => i.nombre == cat1);
                foreach (var car in db.Carro.ToList())
                {
                    if (car.Tipologia_Carro.nombre.Equals(cat1))
                    {
                        subdests.Add(car);
                    }
                }
                ViewBag.name = destProv.nombre;
                ViewBag.desc = destProv.descripcion;
                ViewBag.countcar = subdests.Count.ToString();
                Size_Of_Page = 6;
                No_Of_Page = (Page_No ?? 1);
            }
            else 
                if (category != null)
                {
                    cat1 = category;
                    var destProv = db.Tipologia_Carro.ToList().FirstOrDefault(i => i.nombre == category);
                    foreach (var car in db.Carro.ToList())
                    {
                        if (car.Tipologia_Carro.nombre.Equals(category))
                        {
                            subdests.Add(car);
                        }
                    }
                    ViewBag.name = destProv.nombre;
                    ViewBag.desc = destProv.descripcion;
                    ViewBag.countcar = subdests.Count.ToString();
                    Size_Of_Page = 6;
                    No_Of_Page = (Page_No ?? 1);
                }
            return View(subdests.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        [HttpPost]
        public ActionResult Cars(string namec, string ofice1, string ofice2, string start, string end)
        {
            try 
            {
                if (!ofice1.Equals("Choose One") && !ofice2.Equals("Choose One"))
                {
                    var car = db.Carro.ToList().FirstOrDefault(i => i.name == namec);
                    alojCarro = new AlojamientoCarModel(ofice1, ofice2, start, end);
                    if (car != null)
                    {
                        ctemp = car;
                        return RedirectToAction("Car");
                    }
                }

                return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        #endregion

        #region Menu Excursions

        public JsonResult exc(string destinyex)
        {
            List<string> list = new SubModel().GetSubXExc(destinyex);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Excursions(string destiny)
        {
            try { 
            var subdests = new List<Excursion>();
            if (destiny == null)
            {
                var destProv = db.Destino.ToList().FirstOrDefault(i => i.destino1 == destin1);
                if (destProv != null)
                {
                    subdests = destProv.Excursion.ToList();
                    ViewBag.destiny = destProv.destino1;
                    ViewBag.photo = destProv.foto_destino2;
                    ViewBag.desc = destProv.decripcion_destino;
                }
            }
            else if (destiny != null)
            {
                var destProv = db.Destino.ToList().FirstOrDefault(i => i.destino1 == destiny);
                destin1 = destiny;
                if (destProv != null)
                {
                    subdests = destProv.Excursion.ToList();
                    ViewBag.destiny = destProv.destino1;
                    ViewBag.photo = destProv.foto_destino2;
                    ViewBag.desc = destProv.decripcion_destino;
                }
            }

            return View(subdests);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        [HttpPost]
        public ActionResult Excursions(string subdestiny, string namee)
        {
            try { 
            var exc = db.Excursion.ToList().FirstOrDefault(i => i.nombre_excursion == namee);
            subdestin1 = exc.Destino.destino1;
            if (exc != null)
            {
                etemp = exc;
                return RedirectToAction("Excursion");
            }
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult ExcursionsSubDestinies(string subdestiny)
        {
            try { 
            var listh = new List<Hotel>();
            var liste = new List<Excursion>();
            if (subdestiny != null)
            {
                var destProv = db.Destino.ToList().FirstOrDefault(i => i.destino1 == subdestiny);
                var hotels = db.Hotel.ToList();
                var excs = db.Excursion.ToList();
                ViewBag.destiny = destProv.destino1;
                ViewBag.desc = destProv.decripcion_destino;
                ViewBag.photo = destProv.foto_destino2;
                foreach (var hot in hotels)
                {
                    listh.Add(hot);
                }
                liste = excs.Where(e => e.Destino.destino1.Equals(subdestiny)).ToList();
                ViewBag.counth = listh.Count.ToString();
                ViewBag.counte = liste.Count.ToString();
            }
            else if (subdestiny == null)
            {
                var destProv = db.Destino.ToList().FirstOrDefault(i => i.destino1 == subdestin1);
                var hotels = db.Hotel.ToList();
                var excs = db.Excursion.ToList();
                ViewBag.destiny = destProv.destino1;
                ViewBag.desc = destProv.decripcion_destino;
                ViewBag.photo = destProv.foto_destino2;
                foreach (var hot in hotels)
                {
                    listh.Add(hot);
                }
                liste = excs.Where(e => e.Destino.destino1.Equals(subdestin1)).ToList();
                ViewBag.counth = listh.Count.ToString();
                ViewBag.counte = liste.Count.ToString();
            }
            return View(liste);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        [HttpPost]
        public ActionResult ExcursionsSubDestinies(string subdestiny, string namee)
        {
            try { 
            var exc = db.Excursion.ToList().FirstOrDefault(i => i.nombre_excursion == namee);
            if (exc != null)
            {
                etemp = exc;
                return RedirectToAction("Excursion");
            }
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult Excursion(string namehD)
        {
            try{
            Excursion exc = null;
            if (namehD == null)
            {
                exc = etemp;
                ViewBag.destiny = destin1;
                ViewBag.subdestiny = subdestin1;
                if (exc != null) ViewBag.nameh = exc.nombre_excursion;
            }
            else if (namehD != null)
            {
                exc = db.Excursion.ToList().FirstOrDefault(i => i.nombre_excursion == namehD);
                ViewBag.destiny = destin1;
                ViewBag.subdestiny = subdestin1;
                if (exc != null) ViewBag.nameh = exc.nombre_excursion;
            }
            return View(exc);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        #endregion

        #region Menu Flights

        public ActionResult Flights()
        {
            try
            {
                List<Avion> list=db.Avion.ToList();
                var a=list.OrderByDescending(p=>p.DestinoVuelo.nombre);
                return View(a.ToList());
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public string ClasesSplit(Avion av)
        {
            string sf = "";
            if (av.NomAvionClase.Count!=0)
            {
                foreach (var v1 in av.NomAvionClase)
                {
                    sf +="  " + v1.Clase.nombre;
                }
            }
            
            return sf;
        }

        public string TimeConvert(DateTime time)
        {
            string sf = "";
            sf = time.ToString("D");
            return sf;
        }

        public JsonResult flightsjson(string destiny1, string destiny2)
        {
            var vuelos = new List<Object>();
            var hotel = new FlightModel();
            if (!destiny2.Equals("All") && destiny1.Equals("All"))
            {
                var vs = db.Avion.Select(s => s).Where(s => s.DestinoVuelo.nombre == destiny2);
                foreach (var tipo in vs)
                {
                    string pr = hotel.Min(tipo.idVuelo, DateTime.Now);
                    string cl = ClasesSplit(tipo);
                    string ts = tipo.tsalida.ToString("D");
                    string tll = tipo.tllegada.ToString("D");
                    string num = tipo.idVuelo.ToString();
                    vuelos.Add(new
                    {
                        FotoEmpresa = tipo.Empresa.foto_empresa,
                        NombreEmpresa = tipo.Empresa.nombre_empresa,
                        HoraSalida = tipo.horasalida,
                        Tsalida = ts,
                        OrigenVuelo = tipo.OrigenVuelo.nombre,
                        Horallegada = tipo.horallegada,
                        Tllegada = tll,
                        DestinoVuelo = tipo.DestinoVuelo.nombre,
                        Precio = pr,
                        Clases = cl,
                        Nombre = tipo.nombre,
                        NumVuelo = tipo.numVuelo,
                        IdVuelo = num,
                        TipoAvion = tipo.TipoAvion.nombre
                    });
                }
            }
            else
                if (!destiny2.Equals("All") && !destiny1.Equals("All"))
                {
                    var vs = db.Avion.Select(s => s).Where(s => s.DestinoVuelo.nombre == destiny2 && s.OrigenVuelo.nombre == destiny1);
                    foreach (var tipo in vs)
                    {
                        string pr = hotel.Min(tipo.idVuelo, DateTime.Now);
                        string cl = ClasesSplit(tipo);
                        string ts = tipo.tsalida.ToString("D");
                        string tll = tipo.tllegada.ToString("D");
                        string num = tipo.idVuelo.ToString();
                        vuelos.Add(new
                        {
                            FotoEmpresa = tipo.Empresa.foto_empresa,
                            NombreEmpresa = tipo.Empresa.nombre_empresa,
                            HoraSalida = tipo.horasalida,
                            Tsalida = ts,
                            OrigenVuelo = tipo.OrigenVuelo.nombre,
                            Horallegada = tipo.horallegada,
                            Tllegada = tll,
                            DestinoVuelo = tipo.DestinoVuelo.nombre,
                            Precio = pr,
                            Clases = cl,
                            Nombre = tipo.nombre,
                            NumVuelo = tipo.numVuelo,
                            IdVuelo = num,
                            TipoAvion = tipo.TipoAvion.nombre
                        });
                    }
                }
                else
                    if (destiny2.Equals("All") && destiny1.Equals("All"))
                    {
                        var vs = db.Avion;
                        foreach (var tipo in vs)
                        {
                            string pr = hotel.Min(tipo.idVuelo, DateTime.Now);
                            string cl = ClasesSplit(tipo);
                            string ts = tipo.tsalida.ToString("D");
                            string tll = tipo.tllegada.ToString("D");
                            string num = tipo.idVuelo.ToString();
                            vuelos.Add(new
                            {
                                FotoEmpresa = tipo.Empresa.foto_empresa,
                                NombreEmpresa = tipo.Empresa.nombre_empresa,
                                HoraSalida = tipo.horasalida,
                                Tsalida = ts,
                                OrigenVuelo = tipo.OrigenVuelo.nombre,
                                Horallegada = tipo.horallegada,
                                Tllegada = tll,
                                DestinoVuelo = tipo.DestinoVuelo.nombre,
                                Precio = pr,
                                Clases = cl,
                                Nombre = tipo.nombre,
                                NumVuelo = tipo.numVuelo,
                                IdVuelo = num,
                                TipoAvion = tipo.TipoAvion.nombre
                            });
                        }
                    }
                    else
                        if (destiny2.Equals("All") && !destiny1.Equals("All"))
                        {
                            var vs = db.Avion.Select(s => s).Where(s => s.OrigenVuelo.nombre == destiny1);
                            foreach (var tipo in vs)
                            {
                                string pr = hotel.Min(tipo.idVuelo, DateTime.Now);
                                string cl = ClasesSplit(tipo);
                                string ts = tipo.tsalida.ToString("D");
                                string tll = tipo.tllegada.ToString("D");
                                string num = tipo.idVuelo.ToString();
                                vuelos.Add(new
                                {
                                    FotoEmpresa = tipo.Empresa.foto_empresa,
                                    NombreEmpresa = tipo.Empresa.nombre_empresa,
                                    HoraSalida = tipo.horasalida,
                                    Tsalida = ts,
                                    OrigenVuelo = tipo.OrigenVuelo.nombre,
                                    Horallegada = tipo.horallegada,
                                    Tllegada = tll,
                                    DestinoVuelo = tipo.DestinoVuelo.nombre,
                                    Precio = pr,
                                    Clases = cl,
                                    Nombre = tipo.nombre,
                                    NumVuelo = tipo.numVuelo,
                                    IdVuelo = num,
                                    TipoAvion = tipo.TipoAvion.nombre
                                });
                            }
                        }
            return Json(vuelos, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult flightsclassjson(string idclass)
        {
            var vuelos = new List<Object>();
            var hotel = new FlightModel();
            if (!idclass.Equals("All"))
            {
                var vs = db.Avion;
                foreach (var tipo in vs)
                {
                    foreach (var clase in tipo.NomAvionClase)
                    {
                        if (clase.Clase.nombre.Equals(idclass))
                        {
                            string pr = hotel.Min(tipo.idVuelo, DateTime.Now);
                            string cl = ClasesSplit(tipo);
                            string ts = tipo.tsalida.ToString("D");
                            string tll = tipo.tllegada.ToString("D");
                            string num = tipo.idVuelo.ToString();
                            vuelos.Add(new
                            {
                                FotoEmpresa = tipo.Empresa.foto_empresa,
                                NombreEmpresa = tipo.Empresa.nombre_empresa,
                                HoraSalida = tipo.horasalida,
                                Tsalida = ts,
                                OrigenVuelo = tipo.OrigenVuelo.nombre,
                                Horallegada = tipo.horallegada,
                                Tllegada = tll,
                                DestinoVuelo = tipo.DestinoVuelo.nombre,
                                Precio = pr,
                                Clases = cl,
                                Nombre = tipo.nombre,
                                NumVuelo = tipo.numVuelo,
                                IdVuelo = num,
                                TipoAvion = tipo.TipoAvion.nombre
                            });
                        }
                    }
                }
            }else
                if (idclass.Equals("All"))
                {
                    var vs = db.Avion;
                    foreach (var tipo in vs)
                    {
                       string pr = hotel.Min(tipo.idVuelo, DateTime.Now);
                       string cl = ClasesSplit(tipo);
                       string ts = tipo.tsalida.ToString("D");
                       string tll = tipo.tllegada.ToString("D");
                       string num = tipo.idVuelo.ToString();
                       vuelos.Add(new
                       {
                          FotoEmpresa = tipo.Empresa.foto_empresa,
                          NombreEmpresa = tipo.Empresa.nombre_empresa,
                          HoraSalida = tipo.horasalida,
                          Tsalida = ts,
                          OrigenVuelo = tipo.OrigenVuelo.nombre,
                          Horallegada = tipo.horallegada,
                          Tllegada = tll,
                          DestinoVuelo = tipo.DestinoVuelo.nombre,
                          Precio = pr,
                          Clases = cl,
                          Nombre = tipo.nombre,
                          NumVuelo = tipo.numVuelo,
                          IdVuelo = num,
                          TipoAvion = tipo.TipoAvion.nombre
                       });
                    }
                }
         return Json(vuelos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult flightsairlinejson(string idairline)
        {
            var vuelos = new List<Object>();
            var hotel = new FlightModel();
            if (!idairline.Equals("AllAirLine"))
            {
                var vs = db.Avion;
                foreach (var tipo in vs)
                {
                    if (tipo.Empresa.nombre_empresa.Equals(idairline))
                    {
                        string pr = hotel.Min(tipo.idVuelo, DateTime.Now);
                        string cl = ClasesSplit(tipo);
                        string ts = tipo.tsalida.ToString("D");
                        string tll = tipo.tllegada.ToString("D");
                        string num = tipo.idVuelo.ToString();
                        vuelos.Add(new
                        {
                            FotoEmpresa = tipo.Empresa.foto_empresa,
                            NombreEmpresa = tipo.Empresa.nombre_empresa,
                            HoraSalida = tipo.horasalida,
                            Tsalida = ts,
                            OrigenVuelo = tipo.OrigenVuelo.nombre,
                            Horallegada = tipo.horallegada,
                            Tllegada = tll,
                            DestinoVuelo = tipo.DestinoVuelo.nombre,
                            Precio = pr,
                            Clases = cl,
                            Nombre = tipo.nombre,
                            NumVuelo = tipo.numVuelo,
                            IdVuelo = num,
                            TipoAvion = tipo.TipoAvion.nombre
                        });
                    }
                }
            }
            else
                if (idairline.Equals("AllAirLine"))
                {
                    var vs = db.Avion;
                    foreach (var tipo in vs)
                    {
                        string pr = hotel.Min(tipo.idVuelo, DateTime.Now);
                        string cl = ClasesSplit(tipo);
                        string ts = tipo.tsalida.ToString("D");
                        string tll = tipo.tllegada.ToString("D");
                        string num = tipo.idVuelo.ToString();
                        vuelos.Add(new
                        {
                            FotoEmpresa = tipo.Empresa.foto_empresa,
                            NombreEmpresa = tipo.Empresa.nombre_empresa,
                            HoraSalida = tipo.horasalida,
                            Tsalida = ts,
                            OrigenVuelo = tipo.OrigenVuelo.nombre,
                            Horallegada = tipo.horallegada,
                            Tllegada = tll,
                            DestinoVuelo = tipo.DestinoVuelo.nombre,
                            Precio = pr,
                            Clases = cl,
                            Nombre = tipo.nombre,
                            NumVuelo = tipo.numVuelo,
                            IdVuelo = num,
                            TipoAvion = tipo.TipoAvion.nombre
                        });
                    }
                }
            return Json(vuelos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult flightstimejson(string idtime)
        {
            var vuelos = new List<Object>();
            var hotel = new FlightModel();
            if (idtime.Equals("Morning"))
            {
                var vs = db.Avion;
                foreach (var tipo in vs)
                {
                    string time = tipo.horasalida;
                    if (time.Contains("AM"))
                    {
                        string pr = hotel.Min(tipo.idVuelo, DateTime.Now);
                        string cl = ClasesSplit(tipo);
                        string ts = tipo.tsalida.ToString("D");
                        string tll = tipo.tllegada.ToString("D");
                        string num = tipo.idVuelo.ToString();
                        vuelos.Add(new
                        {
                            FotoEmpresa = tipo.Empresa.foto_empresa,
                            NombreEmpresa = tipo.Empresa.nombre_empresa,
                            HoraSalida = tipo.horasalida,
                            Tsalida = ts,
                            OrigenVuelo = tipo.OrigenVuelo.nombre,
                            Horallegada = tipo.horallegada,
                            Tllegada = tll,
                            DestinoVuelo = tipo.DestinoVuelo.nombre,
                            Precio = pr,
                            Clases = cl,
                            Nombre = tipo.nombre,
                            NumVuelo = tipo.numVuelo,
                            IdVuelo = num,
                            TipoAvion = tipo.TipoAvion.nombre
                        });
                    }
                }
            }
            else
                if (idtime.Equals("Afternoon"))
                {
                    var vs = db.Avion;
                    foreach (var tipo in vs)
                    {
                        string time = tipo.horasalida;
                        int hour = int.Parse(tipo.horasalida.ElementAt(0).ToString());
                        int hour2 = int.Parse(tipo.horasalida.ElementAt(1).ToString());
                        if ((time.Contains("PM") && hour == 0 && hour2 < 6) || (time.Contains("PM") && hour == 1 && hour2 ==2))
                        {
                            string pr = hotel.Min(tipo.idVuelo, DateTime.Now);
                            string cl = ClasesSplit(tipo);
                            string ts = tipo.tsalida.ToString("D");
                            string tll = tipo.tllegada.ToString("D");
                            string num = tipo.idVuelo.ToString();
                            vuelos.Add(new
                            {
                                FotoEmpresa = tipo.Empresa.foto_empresa,
                                NombreEmpresa = tipo.Empresa.nombre_empresa,
                                HoraSalida = tipo.horasalida,
                                Tsalida = ts,
                                OrigenVuelo = tipo.OrigenVuelo.nombre,
                                Horallegada = tipo.horallegada,
                                Tllegada = tll,
                                DestinoVuelo = tipo.DestinoVuelo.nombre,
                                Precio = pr,
                                Clases = cl,
                                Nombre = tipo.nombre,
                                NumVuelo = tipo.numVuelo,
                                IdVuelo = num,
                                TipoAvion = tipo.TipoAvion.nombre
                            });
                        }
                    }
                }
                else
                    if (idtime.Equals("Evening"))
                    {
                        var vs = db.Avion;
                        foreach (var tipo in vs)
                        {
                            string time = tipo.horasalida;
                            int hour = int.Parse(tipo.horasalida.ElementAt(0).ToString());
                            int hour2 = int.Parse(tipo.horasalida.ElementAt(1).ToString());
                            if ((time.Contains("PM") && hour == 1 && hour2 == 0) || (time.Contains("PM") && hour == 1 && hour2 == 1) || (time.Contains("PM") && hour == 0 && hour2 >= 6))
                            {
                                string pr = hotel.Min(tipo.idVuelo, DateTime.Now);
                                string cl = ClasesSplit(tipo);
                                string ts = tipo.tsalida.ToString("D");
                                string tll = tipo.tllegada.ToString("D");
                                string num = tipo.idVuelo.ToString();
                                vuelos.Add(new
                                {
                                    FotoEmpresa = tipo.Empresa.foto_empresa,
                                    NombreEmpresa = tipo.Empresa.nombre_empresa,
                                    HoraSalida = tipo.horasalida,
                                    Tsalida = ts,
                                    OrigenVuelo = tipo.OrigenVuelo.nombre,
                                    Horallegada = tipo.horallegada,
                                    Tllegada = tll,
                                    DestinoVuelo = tipo.DestinoVuelo.nombre,
                                    Precio = pr,
                                    Clases = cl,
                                    Nombre = tipo.nombre,
                                    NumVuelo = tipo.numVuelo,
                                    IdVuelo = num,
                                    TipoAvion = tipo.TipoAvion.nombre
                                });
                            }
                        }
                    }
            return Json(vuelos, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Menu Transfer

        public ActionResult Transfers(string origin)
        {
            try { 
            var subdests = new List<Rental>();
            if (origin == null)
            {
                var destProv = db.OrigenRental.ToList().FirstOrDefault(i => i.nombre == destin1);
                subdests = destProv.Rental.ToList();
                ViewBag.destiny = destProv.nombre;
                ViewBag.desc = destProv.descripcion;
            }
            else if (origin != null)
            {
                var destProv = db.OrigenRental.ToList().FirstOrDefault(i => i.nombre == origin);
                subdests = destProv.Rental.ToList();
                ViewBag.destiny = destProv.nombre;
                ViewBag.desc = destProv.descripcion;
            }

            return View(subdests);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        [HttpPost]
        public ActionResult Transfers(string origin, string dest1)
        {
            try 
            {
                var exc = db.Rental.ToList().FirstOrDefault(i => i.DestinoRental.nombre.Equals(dest1) && i.OrigenRental.nombre.Equals(origin));
                if (exc != null)
                {
                    return RedirectToAction("PaymentTransfer","Payment",exc);
                }
                return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public JsonResult destinTrans(string destiny1)
        {
            List<string> list = new List<string>();
            foreach (var v1 in db.Rental)
            {
                if (v1.DestinoRental.nombre.Equals(destiny1))
                {
                    list.Add(v1.OrigenRental.nombre);
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Menu Packages

        public ActionResult Packages(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            try { 
            var Size_Of_Page = 0;
            var No_Of_Page = 0;
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Paquetes" : "";
            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }
            ViewBag.FilterValue = Search_Data;
            var vs = db.Paquete.Select(s => s);
            if (!String.IsNullOrEmpty(Search_Data))
            {
                vs = vs.Where(v => v.nombre_paquete.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Paquetes":
                    vs = vs.OrderByDescending(v => v.Empresa.nombre_empresa);
                    break;
                default:
                    vs = vs.OrderBy(v => v.Empresa.nombre_empresa);
                    break;
            }
            Size_Of_Page = 6;
            No_Of_Page = (Page_No ?? 1);
            return View(vs.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }

        }

        public ActionResult Package(string namehD)
        {
            try { 
            Paquete paq = null;
            if (namehD == null)
            {
                paq = ptemp;
                ViewBag.nameh = paq.nombre_paquete;
            }
            else if (namehD != null)
            {
                paq = db.Paquete.ToList().FirstOrDefault(i => i.nombre_paquete == namehD);
                ViewBag.nameh = paq.nombre_paquete;
            }
            return View(paq);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        [HttpPost]
        public ActionResult Packages(string nameh)
        {
            try
            {
                var hotel = db.Paquete.ToList().FirstOrDefault(i => i.nombre_paquete == nameh);
            
            if (hotel != null)
            {
                ptemp = hotel;
                return RedirectToAction("Package");
            }
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        #endregion

        #endregion

        #region Buscador de la Pagina Principal

        #region Hotel

        public JsonResult sub(string destiny)
        {
            List<string> list = new SubModel().GetSubXDestinosN(destiny);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult hot(string destiny)
        {
            List<string> list = new SubModel().GetSubHotels(destiny);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult hottips(string hot)
        {
            List<string> list = new List<string>();
            var id = int.Parse(hot);
            var hotel = db.Hotel.Single(p => p.idHotel == id);
            foreach (var v in hotel.Tipol_HotelTipol)
            {
                list.Add(v.Tipologia.tipologia1);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult calc(string hotel, string tipology1, string dates, string datee)
        {
            string list = this.HotelPrice(hotel, tipology1, dates, datee);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public string HotelPrice(string hotel, string tipology, string d1, string d2)
        {
            double? result = 0;
            var ds = Convert.ToDateTime(d1);
            var de = Convert.ToDateTime(d2);
            TimeSpan dif = de - ds;
            int days = dif.Days;
            int i = 0;
            while (i < days)
            {
                foreach (var v1 in db.Temporada_Hotel)
                {
                    if (v1.Hotel.nombre_hotel.Equals(hotel))
                    {
                        if (v1.inicio.Date < ds && v1.fin.Date > ds && v1.Anno.anno1.Equals(ds.Year.ToString()))
                        {
                            foreach (var v2 in v1.NomTempTipHotel.ToList())
                            {
                                if (v2.Tipologia.tipologia1 == tipology)
                                {
                                    result += v2.tarifa;
                                    if (ds.Day != 30 && ds.Day != 31)
                                    {
                                        var time = new DateTime(ds.Year, ds.Month, ds.Day + 1);
                                        ds = time;
                                        i++;
                                    }
                                    else
                                        if (ds.Day == 30 || ds.Day == 31)
                                        {
                                            var time = new DateTime(ds.Year, ds.Month + 1, 1);
                                            ds = time;
                                            i++;
                                        }
                                }
                            }
                        }
                    }
                }
                if (i == 0)
                {
                    return result.ToString();
                }
            }
            return result.ToString();
        }

        public JsonResult hotelstarsjson(string idstars, string destiny1)
        {
            var vuelos = new List<Object>();
            var hotel = new HotelModel();
            var submodel = new DestinoModel();
            var vs = db.Hotel;
            if (idstars.Equals("three"))
            {
                foreach (var tipo in vs)
                {
                    if (tipo.Categoria.categoria1.Equals("3") && tipo.Destino.destino1.Equals(destiny1))
                    {
                        string pr = hotel.Min(tipo.idHotel, DateTime.Now);
                        string num = tipo.idHotel.ToString();
                        string word = submodel.GetDescSplitingHotel(tipo.descripcion_hotel);
                        string tipos = tipo.Tipol_HotelTipol.Count.ToString();
                        string foto = tipo.foto_hotel1;
                        vuelos.Add(new
                        {
                            Precio = pr,
                            Nombre = tipo.nombre_hotel,
                            IdHotel = num,
                            Descripcion = word,
                            Cadena = tipo.Cadena.cadena_,
                            Categoria = tipo.Categoria.categoria1,
                            Destino = tipo.Destino.destino1,
                            Tipologias = tipos,
                            Foto = foto
                        });
                    }
                }
            }
            else
            if (idstars.Equals("four"))
            {
                foreach (var tipo in vs)
                {
                    if (tipo.Categoria.categoria1.Equals("4") && tipo.Destino.destino1.Equals(destiny1))
                    {
                        string pr = hotel.Min(tipo.idHotel, DateTime.Now);
                        string num = tipo.idHotel.ToString();
                        string word = submodel.GetDescSplitingHotel(tipo.descripcion_hotel);
                        string tipos = tipo.Tipol_HotelTipol.Count.ToString();
                        string foto = tipo.foto_hotel1;
                        vuelos.Add(new
                        {
                            Precio = pr,
                            Nombre = tipo.nombre_hotel,
                            IdHotel = num,
                            Descripcion = word,
                            Cadena = tipo.Cadena.cadena_,
                            Categoria = tipo.Categoria.categoria1,
                            Destino = tipo.Destino.destino1,
                            Tipologias = tipos,
                            Foto = foto
                        });
                    }
                }
            }
            else
            if (idstars.Equals("five"))
            {
                foreach (var tipo in vs)
                {
                    if (tipo.Categoria.categoria1.Equals("5") && tipo.Destino.destino1.Equals(destiny1))
                    {
                        string pr = hotel.Min(tipo.idHotel, DateTime.Now);
                        string num = tipo.idHotel.ToString();
                        string word = submodel.GetDescSplitingHotel(tipo.descripcion_hotel);
                        string tipos = tipo.Tipol_HotelTipol.Count.ToString();
                        string foto = tipo.foto_hotel1;
                        vuelos.Add(new
                        {
                            Precio = pr,
                            Nombre = tipo.nombre_hotel,
                            IdHotel = num,
                            Descripcion = word,
                            Cadena = tipo.Cadena.cadena_,
                            Categoria = tipo.Categoria.categoria1,
                            Destino = tipo.Destino.destino1,
                            Tipologias = tipos,
                            Foto = foto
                        });
                    }
                }
            }
            return Json(vuelos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Hotels(string destiny, string Sorting_Order, string Search_Data, string Filter_Value,int? Page_No)
        {
            try { 
            int Size_Of_Page = 0;
            int No_Of_Page = 0;
            var subdests = new List<Hotel>();
            if (destiny == null)
            {
                var destProv = db.Destino.ToList().FirstOrDefault(i => i.destino1 == destin1);
                if (destProv != null)
                {
                    subdests = destProv.Hotel.ToList();
                    ViewBag.destiny = destProv.destino1;
                    ViewBag.photo = destProv.foto_destino2;
                    ViewBag.desc = destProv.decripcion_destino.ToString();
                    Size_Of_Page = 10;
                    No_Of_Page = (Page_No ?? 1);
                }
            }
            else if (destiny != null)
            {
                var destProv = db.Destino.ToList().FirstOrDefault(i => i.destino1 == destiny);
                if (destProv != null)
                {
                    destin1 = destiny;
                    subdests = destProv.Hotel.ToList();
                    ViewBag.destiny = destProv.destino1;
                    ViewBag.photo = destProv.foto_destino2;
                    ViewBag.desc = destProv.decripcion_destino.ToString();
                    Size_Of_Page = 10;
                    No_Of_Page = (Page_No ?? 1);
                }
            }
               var list= subdests.OrderByDescending(p => p.Categoria.categoria1);
               return View(list.ToPagedList(No_Of_Page, Size_Of_Page));
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult Hotel(string namehD)
        {
            try { 
            Hotel hotel = null;
            if (namehD == null)
            {
                hotel = htemp;
                ViewBag.destiny = hotel.Destino.destino1;
                ViewBag.nameh = hotel.nombre_hotel;
                DateTime dS = DateTime.Now;
                ViewBag.dE = GetDateTime(dS);
            }
            else if (namehD != null)
            {
                hotel = db.Hotel.ToList().FirstOrDefault(i => i.nombre_hotel == namehD);
                ViewBag.destiny = hotel.Destino.destino1;
                ViewBag.nameh = hotel.nombre_hotel;
                DateTime dS = DateTime.Now;
                ViewBag.dE = GetDateTime(dS);
            }
            return View(hotel);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public DateTime GetDateTime(DateTime dS)
        {
           
            DateTime dE = new DateTime();
            if (dS.Month == 1 || dS.Month == 3 || dS.Month == 5 || dS.Month == 7 || dS.Month == 8 || dS.Month == 10 || dS.Month == 12)
            {
                if (dS.Day == 25)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 1);
                }
                else if (dS.Day == 26)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 2);
                }
                else if (dS.Day == 27)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 3);
                }
                else if (dS.Day == 28)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 4);
                }
                else if (dS.Day == 29)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 5);
                }
                else if (dS.Day == 30)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 6);
                }
                else if (dS.Day == 31)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 7);
                }
                if (dS.Day == 1 || dS.Day == 2 || dS.Day == 3 || dS.Day == 4 || dS.Day == 5 || dS.Day == 6 ||
                    dS.Day == 7 || dS.Day == 8 || dS.Day == 9 || dS.Day == 10 || dS.Day == 11 || dS.Day == 12 || dS.Day == 13 ||
                    dS.Day == 14 || dS.Day == 15 || dS.Day == 16 || dS.Day == 17 || dS.Day == 18 || dS.Day == 19 || dS.Day == 20 ||
                    dS.Day == 21 || dS.Day == 22 || dS.Day == 23 || dS.Day == 24)
                    {
                        dE = new DateTime(dS.Year, dS.Month,dS.Day + 7);
                    }
            }
            if (dS.Month == 4 || dS.Month == 6 || dS.Month == 9 || dS.Month == 11)
            {
                if (dS.Day == 24)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 1);
                }
                else if (dS.Day == 25)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 2);
                }
                else if (dS.Day == 26)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 3);
                }
                else if (dS.Day == 27)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 4);
                }
                else if (dS.Day == 28)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 5);
                }
                else if (dS.Day == 29)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 6);
                }
                else if (dS.Day == 30)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 7);
                }
                if (dS.Day == 1 || dS.Day == 2 || dS.Day == 3 || dS.Day == 4 || dS.Day == 5 || dS.Day == 6 ||
                    dS.Day == 7 || dS.Day == 8 || dS.Day == 9 || dS.Day == 10 || dS.Day == 11 || dS.Day == 12 || dS.Day == 13 ||
                    dS.Day == 14 || dS.Day == 15 || dS.Day == 16 || dS.Day == 17 || dS.Day == 18 || dS.Day == 19 || dS.Day == 20 ||
                    dS.Day == 21 || dS.Day == 22 || dS.Day == 23)
                {
                    dE = new DateTime(dS.Year, dS.Month, dS.Day + 7);
                }
            }
            if (dS.Month == 2)
            {
                if (dS.Day == 22)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 1);
                }
                else if (dS.Day == 23)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 2);
                }
                else if (dS.Day == 24)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 3);
                }
                else if (dS.Day == 25)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 4);
                }
                else if (dS.Day == 26)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 5);
                }
                else if (dS.Day == 27)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 6);
                }
                else if (dS.Day == 28)
                {
                    dE = new DateTime(dS.Year, dS.Month + 1, 7);
                }
                if (dS.Day == 1 || dS.Day == 2 || dS.Day == 3 || dS.Day == 4 || dS.Day == 5 || dS.Day == 6 ||
                    dS.Day == 7 || dS.Day == 8 || dS.Day == 9 || dS.Day == 10 || dS.Day == 11 || dS.Day == 12 || dS.Day == 13 ||
                    dS.Day == 14 || dS.Day == 15 || dS.Day == 16 || dS.Day == 17 || dS.Day == 18 || dS.Day == 19 || dS.Day == 20 ||
                    dS.Day == 21)
                {
                    dE = new DateTime(dS.Year, dS.Month, dS.Day + 7);
                }
            }
            return dE;
          
        }

        [HttpPost]
        public ActionResult Hotels(string subdestiny, string nameh)
        {
            try { 
            var hotel = db.Hotel.ToList().FirstOrDefault(i => i.nombre_hotel == nameh);
            if (hotel != null)
            {
                htemp = hotel;
                return RedirectToAction("Hotel");
            }
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }
        #endregion

        #region Car

        public JsonResult mod(string category)
        {
            List<string> list = new SubModel().GetSubXModeles(category);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult carscatrgjson(string id, string categor)
        {
            var vuelos = new List<Object>();
            var hotel = new TarifaCarroModel();
            var submodel = new DestinoModel();
            var vs = db.Carro;
            if (id.Equals("tow"))
            {
                foreach (var tipo in vs)
                {
                    if (tipo.pasajeros==2 && tipo.Tipologia_Carro.nombre.Equals(categor))
                    {
                        string pr = hotel.price(tipo.Empresa.idEmpresa, DateTime.Now);
                        string num = tipo.idCarro.ToString();
                        string word = submodel.GetDescSplitingHotel(tipo.descripcion_carro);
                        string foto = tipo.foto_carro1;
                        string pasa = tipo.pasajeros.ToString();
                        string list = "";
                        foreach (var a in db.OficinaRenta)
                        {
                            list += "<option>" + a.nombre + "</option>";
                        }
                        vuelos.Add(new
                        {
                            Precio = pr,
                            Nombre = tipo.name,
                            Oficinas=list,
                            IdCarro = num,
                            Descripcion = word,
                            Empresa = tipo.Empresa.nombre_empresa,
                            Categoria = tipo.Tipologia_Carro.nombre,
                            Pasajeros=pasa,
                            Transmision=tipo.transmision,
                            Foto = foto
                        });
                    }
                }
            }
            else
                if (id.Equals("three"))
                {
                    foreach (var tipo in vs)
                    {
                        if (tipo.pasajeros == 3 && tipo.Tipologia_Carro.nombre.Equals(categor))
                        {
                            string pr = hotel.price(tipo.Empresa.idEmpresa, DateTime.Now);
                            string num = tipo.idCarro.ToString();
                            string word = submodel.GetDescSplitingHotel(tipo.descripcion_carro);
                            string foto = tipo.foto_carro1;
                            string pasa = tipo.pasajeros.ToString();
                            string list = "";
                            foreach (var a in db.OficinaRenta)
                            {
                                list += "<option>" + a.nombre + "</option>";
                            }
                            vuelos.Add(new
                            {
                                Precio = pr,
                                Nombre = tipo.name,
                                Oficinas = list,
                                IdCarro = num,
                                Descripcion = word,
                                Empresa = tipo.Empresa.nombre_empresa,
                                Categoria = tipo.Tipologia_Carro.nombre,
                                Pasajeros = pasa,
                                Transmision = tipo.transmision,
                                Foto = foto
                            });
                        }
                    }
                }
                else
                    if (id.Equals("four"))
                    {
                        foreach (var tipo in vs)
                        {
                            if (tipo.pasajeros == 4 && tipo.Tipologia_Carro.nombre.Equals(categor))
                            {
                                string pr = hotel.price(tipo.Empresa.idEmpresa, DateTime.Now);
                                string num = tipo.idCarro.ToString();
                                string word = submodel.GetDescSplitingHotel(tipo.descripcion_carro);
                                string foto = tipo.foto_carro1;
                                string pasa = tipo.pasajeros.ToString();
                                string list = "";
                                foreach (var a in db.OficinaRenta)
                                {
                                    list += "<option>" + a.nombre + "</option>";
                                }
                                vuelos.Add(new
                                {
                                    Precio = pr,
                                    Nombre = tipo.name,
                                    Oficinas = list,
                                    IdCarro = num,
                                    Descripcion = word,
                                    Empresa = tipo.Empresa.nombre_empresa,
                                    Categoria = tipo.Tipologia_Carro.nombre,
                                    Pasajeros = pasa,
                                    Transmision = tipo.transmision,
                                    Foto = foto
                                });
                            }
                        }
                    }
                    else
                        if (id.Equals("more"))
                        {
                            foreach (var tipo in vs)
                            {
                                if (tipo.pasajeros > 4 && tipo.Tipologia_Carro.nombre.Equals(categor))
                                {
                                    string pr = hotel.price(tipo.Empresa.idEmpresa, DateTime.Now);
                                    string num = tipo.idCarro.ToString();
                                    string word = submodel.GetDescSplitingHotel(tipo.descripcion_carro);
                                    string foto = tipo.foto_carro1;
                                    string pasa = tipo.pasajeros.ToString();
                                    string list = "";
                                    foreach (var a in db.OficinaRenta)
                                    {
                                        list += "<option>" + a.nombre + "</option>";
                                    }
                                    vuelos.Add(new
                                    {
                                        Precio = pr,
                                        Nombre = tipo.name,
                                        Oficinas = list,
                                        IdCarro = num,
                                        Descripcion = word,
                                        Empresa = tipo.Empresa.nombre_empresa,
                                        Categoria = tipo.Tipologia_Carro.nombre,
                                        Pasajeros = pasa,
                                        Transmision = tipo.transmision,
                                        Foto = foto
                                    });
                                }
                            }
                        }
            return Json(vuelos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarCategory()
        {
            try { 
            var car = new Carro();
            foreach (var car2 in db.Carro.ToList())
            {
                if (car2.Tipologia_Carro.nombre.Equals(cat1) && car2.name.Equals(model1))
                {
                    car = car2;
                }
            }
            ViewBag.ofice1 = alojCarro.oficini;
            ViewBag.ofice2 = alojCarro.oficfin;
            ViewBag.countDay = alojCarro.catDias();
            ViewBag.DS = alojCarro.fechD1();
            ViewBag.DE = alojCarro.fechD2();
            return View(car);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        [HttpPost]
        public ActionResult CarCategory(string namec, string ofice1, string ofice2, string start, string end)
        {
            try { 
            var car = db.Carro.ToList().FirstOrDefault(i => i.name == namec);
            alojCarro = new AlojamientoCarModel(ofice1, ofice2, start, end);
            ViewBag.ofice1 = alojCarro.oficini;
            ViewBag.ofice2 = alojCarro.oficfin;
            ViewBag.countDay = alojCarro.catDias();
            ViewBag.DS = alojCarro.fechD1();
            ViewBag.DE = alojCarro.fechD2();
            return View(car);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public ActionResult Car()
        {
            try { 
            var car = ctemp;
            ViewBag.ofice1 = alojCarro.oficini;
            ViewBag.ofice2 = alojCarro.oficfin;
            ViewBag.countDay = alojCarro.catDias();
            ViewBag.DS = alojCarro.fechD1();
            ViewBag.DE = alojCarro.fechD2();
            return View(car);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        [HttpPost]
        public ActionResult Car(string namec, string ofice1, string ofice2, string start, string end)
        {
            try { 
            var car = db.Carro.ToList().FirstOrDefault(i => i.name == namec);
            alojCarro = new AlojamientoCarModel(ofice1, ofice2, start, end);
            ViewBag.ofice1 = alojCarro.oficini;
            ViewBag.ofice2 = alojCarro.oficfin;
            ViewBag.countDay = alojCarro.catDias();
            ViewBag.DS = alojCarro.fechD1();
            ViewBag.DE = alojCarro.fechD2();
            return View(car);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        #endregion

        #endregion
    }
}