using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TripAgency.EF;
using TripAgency.Models;

namespace TripAgency.Controllers
{
    public class HotelController : Controller
    {
        private TripAgencyEntities db = new TripAgencyEntities();

        // GET: /Hotel/
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
            var cadenas = from cadena in db.Hotel select cadena;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                cadenas = cadenas.Where(cadena => cadena.Cadena.cadena_.ToUpper().Contains(Search_Data.ToUpper()) || cadena.nombre_hotel.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Nombre":
                    cadenas = cadenas.OrderByDescending(cadena => cadena.Cadena.cadena_);
                    break;
                default:
                    cadenas = cadenas.OrderBy(cadena => cadena.Cadena.cadena_);
                    break;
            }
            int Size_Of_Page = 15;
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

        // GET: /Hotel/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotel.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
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

        private void PopulateAssignedCourseData(Hotel hotel)
        {
            var alltipol = db.Tipologia;
            var hotelTipologias = new HashSet<int?>(hotel.Tipol_HotelTipol.Select(c => c.idTiologia));
            var viewModel = new List<Tipol_HotelTipoAsigned>();
            foreach (var tipo in alltipol)
            {
                if (hotelTipologias.Contains(tipo.idTiologia))
                {
                    viewModel.Add(new Tipol_HotelTipoAsigned
                    {
                        idTipologia = tipo.idTiologia,
                        tipologia1 = tipo.tipologia1,
                        assigned = true,
                        desc = (hotel.Tipol_HotelTipol.Single(i => i.idTiologia == tipo.idTiologia)).descripc
                        
                    });
                }
                else
                    {
                        viewModel.Add(new Tipol_HotelTipoAsigned
                        {
                            idTipologia = tipo.idTiologia,
                            tipologia1 = tipo.tipologia1,
                            assigned = false,
                            desc = "Choose One"
                        });
                    }
                
            }
            ViewBag.Courses = viewModel;
        }

        private void PopulateAssignedSuplementData(Hotel hotel)
        {
            var allsuple = db.Suplemento;
            var hotelSuplementos = new HashSet<int?>(hotel.Suplemento_Hotel.Select(c => c.idSuplemento));
            var viewModelsupl = new List<Suplemento_Hotel_Asigned>();

            foreach (var tipo in allsuple)
            {
                viewModelsupl.Add(new Suplemento_Hotel_Asigned
                {
                    idSuplemento = tipo.idSuplemento,
                    suplemento1 = tipo.suplemento1,
                    assigned = hotelSuplementos.Contains(tipo.idSuplemento)
                });
            }
            ViewBag.Suplementos = viewModelsupl;
        }

        private void PopulateAssignedReduccionData(Hotel hotel)
        {
            var allreducc = db.Reduccion;
            var hotelReducciones = new HashSet<int?>(hotel.Reduccion_Hotel.Select(c => c.idReduccion));
            var viewModelreducc = new List<Reduccion_Hotel_Asigned>();

            foreach (var tipo in allreducc)
            {
                viewModelreducc.Add(new Reduccion_Hotel_Asigned
                {
                    idReduccion = tipo.idReduccion,
                    reduccion1 = tipo.reduccion1,
                    assigned = hotelReducciones.Contains(tipo.idReduccion)
                });
            }
            ViewBag.Reduccion = viewModelreducc;
        }


        // GET: /Hotel/Create
        public ActionResult Create()
        {
            try { 
            var hotel = new Hotel();
            hotel.Tipol_HotelTipol = new List<Tipol_HotelTipol>();
            PopulateAssignedCourseData(hotel);
            PopulateAssignedSuplementData(hotel);
            PopulateAssignedReduccionData(hotel);
            ViewBag.idCadena = new SelectList(db.Cadena, "idCadena", "cadena_");
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "categoria1");
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1");
            return View();
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        public string[] Incredible(string[] list, string condition)
        {
            var sf=new List<string>();
            for (int i=0; i<list.Length;i++)
            {
                if (!list[i].Equals(condition))
                {
                    sf.Add(i + list[i]);
                }
            }
            return sf.ToArray();
        }

        // POST: /Hotel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idHotel,nombre_hotel,descripcion_hotel,direccion,idDestino,foto_hotel1,foto_hote2,foto_hotel3,idCategoria,idCadena,idSubdetino,telefono,fax,sitioWeb,facilidades")] Hotel hotel, HttpPostedFileBase picture, HttpPostedFileBase picture2, HttpPostedFileBase picture3, string[] selectedCourses, string[] selectedSuplements, string[] selectedReductions, string State, string[] selectedDesc, string[] selectedDescNew)
        {
            try { 
            var listDesc=new List<string>();
            var listDescComb = Incredible(selectedDesc, "Choose One");
            var listDescNew = Incredible(selectedDescNew, "");
            listDesc.InsertRange(listDesc.Count, listDescComb.ToList());
            listDesc.InsertRange(listDesc.Count, listDescNew.ToList());
            listDesc.Sort();
            var strMappath = "/TripAgency/TripAgency/Content/Upload/Hotels/" + "Hotel" + " " + hotel.nombre_hotel;
            DirectoryInfo di=null;
            string rutaimg = "";
            string rutaimg2 = "";
            string rutaimg3 = "";
            if (selectedCourses != null)
            {
                hotel.Tipol_HotelTipol = new List<Tipol_HotelTipol>();
                for(int i=0;i< selectedCourses.Length;i++)
                {
                    var tph=new Tipol_HotelTipol();
                    tph.idTiologia = int.Parse(selectedCourses[i]);
                    tph.idhotel = hotel.idHotel;
                    tph.asignado = true;
                    tph.descripc = listDesc[i].Remove(0,1);
                    hotel.Tipol_HotelTipol.Add(tph);
                }
            }

            if (selectedSuplements != null)
            {
                hotel.Suplemento_Hotel = new List<Suplemento_Hotel>();
                foreach (var course in selectedSuplements)
                {
                    var tph = new Suplemento_Hotel();
                    tph.idSuplemento = int.Parse(course);
                    tph.idhotel = hotel.idHotel;
                    tph.asignado = true;
                    hotel.Suplemento_Hotel.Add(tph);
                }
            }

            if (selectedReductions != null)
            {
                hotel.Reduccion_Hotel = new List<Reduccion_Hotel>();
                foreach (var course in selectedReductions)
                {
                    var tph = new Reduccion_Hotel();
                    tph.idReduccion = int.Parse(course);
                    tph.idhotel = hotel.idHotel;
                    tph.asignado = true;
                    hotel.Reduccion_Hotel.Add(tph);
                }
            }

            if (picture == null)
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            else if (picture.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
                if (!extensions.Contains(picture.FileName.Substring(picture.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                }
                else if (picture.ContentLength > maxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                }
                else
                {
                   
                    string fileName;
                    fileName = Path.GetFileName(picture.FileName);
                    var exten = picture.FileName.Substring(picture.FileName.LastIndexOf('.'));
                    if (Directory.Exists(strMappath))
                    {
                        di = Directory.GetParent(strMappath);
                        var acces = di.GetAccessControl(AccessControlSections.Access);
                    }
                    if (!Directory.Exists(strMappath))
                    {
                        di = Directory.CreateDirectory(strMappath);
                        var acces = di.GetAccessControl(AccessControlSections.Access);
                    }
                    var name_file = di.FullName + "\\" + fileName;

                    var path = Directory.GetFiles(strMappath);
                    var fullPath = strMappath + picture.FileName;

                    foreach (var p in path.ToList())
                    {
                        if (p.Equals(fullPath))
                        {
                            ModelState.AddModelError("",
                                "El archivo " + picture.FileName + " " + "ya existe");
                            return View(hotel);
                        }
                    }
                    var destinationPath = Path.Combine(Server.MapPath(di.Name), fileName);
                    picture.SaveAs(name_file);
                    Util ddd=new Util();
                    rutaimg = ddd.name(name_file);
                }
            }

            if (picture2 == null)
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            else if (picture2.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
                if (!extensions.Contains(picture2.FileName.Substring(picture2.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                }
                else if (picture2.ContentLength > maxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                }
                else
                {
                    string fileName = Path.GetFileName(picture2.FileName);
                    var exten = picture2.FileName.Substring(picture2.FileName.LastIndexOf('.'));
                    if (Directory.Exists(strMappath))
                    {
                        di = Directory.GetParent(strMappath);
                        var acces = di.GetAccessControl(AccessControlSections.Access);
                    }
                    var file = strMappath + "//" + fileName;
                    di = Directory.GetParent(file);
                    var name_file2 = di.FullName + "\\" + fileName;
                    var path = Directory.GetFiles(strMappath);
                    var fullPath = strMappath + picture2.FileName;
                    
                    foreach (var p in path.ToList())
                    {
                        if (p.Equals(fullPath))
                        {
                            ModelState.AddModelError("","El archivo " + picture2.FileName + " " + "ya existe");
                            return View(hotel);
                        }
                    }
                    var destinationPath = Path.Combine(Server.MapPath(di.Name), fileName);
                    picture2.SaveAs(name_file2);
                    Util ddd = new Util();
                    rutaimg2 = ddd.name(name_file2);
                }
            }

            if (picture3 == null)
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            else if (picture3.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
                if (!extensions.Contains(picture3.FileName.Substring(picture3.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                }
                else if (picture3.ContentLength > maxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                }
                else
                {
                    string fileName = Path.GetFileName(picture3.FileName);
                    var exten = picture3.FileName.Substring(picture3.FileName.LastIndexOf('.'));
                    if (Directory.Exists(strMappath))
                    {
                        di = Directory.GetParent(strMappath);
                        var acces = di.GetAccessControl(AccessControlSections.Access);
                    }
                    if (!Directory.Exists(strMappath))
                    {
                        di = Directory.CreateDirectory(strMappath);
                        var acces = di.GetAccessControl(AccessControlSections.Access);
                    }
                    var file = strMappath + "//" + fileName;
                    di = Directory.GetParent(file);
                    var name_file3 = di.FullName + "\\" + fileName;
                    var path = Directory.GetFiles(strMappath);
                    var fullPath = strMappath + picture3.FileName;

                    foreach (var p in path.ToList())
                    {
                        if (p.Equals(fullPath))
                        {
                            ModelState.AddModelError("",
                                "El archivo " + picture3.FileName + " " + "ya existe");
                            return View(hotel);
                        }
                    }
                    var destinationPath = Path.Combine(Server.MapPath(di.Name), fileName);
                    picture3.SaveAs(name_file3);
                    Util ddd = new Util();
                    rutaimg3 = ddd.name(name_file3);
                }
            }

            if (ModelState.IsValid)
            {
                hotel.foto_hotel1 = rutaimg;
                hotel.foto_hote2 = rutaimg2;
                hotel.foto_hotel3 = rutaimg3;
                if (!hotel.nombre_hotel.StartsWith("Hotel"))
                {
                    hotel.nombre_hotel = "Hotel " + hotel.nombre_hotel;
                }       
                db.Hotel.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCadena = new SelectList(db.Cadena, "idCadena", "cadena_", hotel.idCadena);
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "categoria1", hotel.idCategoria);
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1");
            PopulateAssignedCourseData(hotel);
            PopulateAssignedSuplementData(hotel);
            PopulateAssignedReduccionData(hotel);
            return View(hotel);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        //----------------------------

        private void UpdateInstructorCourses(string[] selectedHotels,string[] selectedDesc, Hotel hotelToUpdate)
        {
            List<int?> listint=new List<int?>();
            if (selectedHotels == null)
            {
                hotelToUpdate.Tipol_HotelTipol = new List<Tipol_HotelTipol>(); 
                return;
            }
            var selectedTiologiasHS = selectedHotels.ToList();
            foreach (var tenp in selectedTiologiasHS)
            {
                listint.Add(Convert.ToInt32(tenp));
            }
            var instructorCourses = new HashSet<int?>(hotelToUpdate.Tipol_HotelTipol.Select(c => c.idTiologia));
            var listHotelTipologias = instructorCourses.ToList();
            for (int i=0;i<listint.Count;i++)
            {
                if (listHotelTipologias.Contains(listint[i]) == false)
                {
                    var tph = new Tipol_HotelTipol();
                    tph.idTiologia = listint[i];
                    tph.idhotel = hotelToUpdate.idHotel;
                    tph.asignado = true;
                    tph.descripc = selectedDesc[i].Remove(0, 1);
                    hotelToUpdate.Tipol_HotelTipol.Add(tph);
                    foreach (var temphot in db.Temporada_Hotel)
                    {
                        if (temphot.Hotel.idHotel == hotelToUpdate.idHotel)
                        {
                            var tempnom1 = new NomTempTipHotel();
                            var elemtipo = db.Tipologia.Single(p => p.idTiologia == listint[i]);
                            tempnom1.idTiologia = elemtipo.idTiologia;
                            tempnom1.Tipologia = elemtipo;
                            tempnom1.idTemporadaHotel = temphot.idTemporadaHotel;
                            tempnom1.tarifa = 0;
                            tempnom1.maxninno = 2;
                            tempnom1.maxadult = 3;
                            tempnom1.minninno = 0;
                            tempnom1.minadult = 1;
                            tempnom1.idtipotarifa = 7;
                            temphot.NomTempTipHotel.Add(tempnom1);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    foreach (var aa in hotelToUpdate.Tipol_HotelTipol)
                    {
                        if (aa.idTiologia == listint[i])
                        {
                            aa.descripc = selectedDesc[i].Remove(0, 1);
                            db.SaveChanges();
                        }
                    }
                }
            }
            var listHTs = hotelToUpdate.Tipol_HotelTipol.ToList();
            foreach (var tipo in listHTs)
            {
                if (listint.Contains(tipo.idTiologia) == false)
                {
                    hotelToUpdate.Tipol_HotelTipol.Remove(tipo);
                    db.SaveChanges();
                }
            } 
        }

        private void UpdateInstructorSuplementos(string[] selectedSuplements, Hotel hotelToUpdate)
        {
            List<int?> listint = new List<int?>();
            if (selectedSuplements == null)
            {
                hotelToUpdate.Suplemento_Hotel = new List<Suplemento_Hotel>();
                return;
            }
            var selectedSuplHS = selectedSuplements.ToList();
            foreach (var tenp in selectedSuplHS)
            {
                listint.Add(Convert.ToInt32(tenp));
            }
            var instructorCourses = new HashSet<int?>(hotelToUpdate.Suplemento_Hotel.Select(c => c.idSuplemento));
            var listHotelSuplementos = instructorCourses.ToList();
            foreach (var course in listint)
            {
                if (listHotelSuplementos.Contains(course) == false)
                {
                    var tph = new Suplemento_Hotel();
                    tph.idSuplemento = course;
                    tph.idhotel = hotelToUpdate.idHotel;
                    tph.asignado = true;
                    hotelToUpdate.Suplemento_Hotel.Add(tph);
                    foreach (var temphot in db.Temporada_Suplemento)
                    {
                        if (temphot.Hotel.idHotel == hotelToUpdate.idHotel)
                        {
                            var elemtipo = db.Suplemento.Single(p => p.idSuplemento == course);
                            var tempnom2 = new NomTempSupHotel();
                            tempnom2.idsuplemento = elemtipo.idSuplemento;
                            tempnom2.Suplemento = elemtipo;
                            tempnom2.idSuplementoHotel = temphot.idSuplementoHotel;
                            tempnom2.tarifa = 0;
                            tempnom2.idtipotarifa = 7;
                            temphot.NomTempSupHotel.Add(tempnom2);
                            db.SaveChanges();
                        }
                    }
                }
            }
            var listHTs = hotelToUpdate.Suplemento_Hotel.ToList();
            foreach (var tipo in listHTs)
            {
                if (listint.Contains(tipo.idSuplemento) == false)
                {
                    hotelToUpdate.Suplemento_Hotel.Remove(tipo);
                    db.SaveChanges();
                }
            }
        }

        private void UpdateInstructorReduccipon(string[] selectedReductions, Hotel hotelToUpdate)
        {
            List<int?> listint = new List<int?>();
            if (selectedReductions == null)
            {
                hotelToUpdate.Reduccion_Hotel = new List<Reduccion_Hotel>();
                return;
            }
            var selectedRedHS = selectedReductions.ToList();
            foreach (var tenp in selectedRedHS)
            {
                listint.Add(Convert.ToInt32(tenp));
            }
            var instructorCourses = new HashSet<int?>(hotelToUpdate.Reduccion_Hotel.Select(c => c.idReduccion));
            var listHotelReducciones = instructorCourses.ToList();
            foreach (var course in listint)
            {
                if (listHotelReducciones.Contains(course) == false)
                {
                    var tph = new Reduccion_Hotel();
                    tph.idReduccion = course;
                    tph.idhotel = hotelToUpdate.idHotel;
                    tph.asignado = true;
                    hotelToUpdate.Reduccion_Hotel.Add(tph);
                    foreach (var temphot in db.Temporada_Reduccion)
                    {
                        if (temphot.Hotel.idHotel == hotelToUpdate.idHotel)
                        {
                            var elemtipo = db.Reduccion.Single(p => p.idReduccion == course);
                            var tempnom3 = new NomTempRedHotel();
                            tempnom3.idreduccion = elemtipo.idReduccion;
                            tempnom3.Reduccion = elemtipo;
                            tempnom3.idTemporada_Reduccion = temphot.idTemporada_Reduccion;
                            tempnom3.tarifa = 0;
                            tempnom3.idtipotarifa = 7;
                            temphot.NomTempRedHotel.Add(tempnom3);
                            db.SaveChanges();
                        }
                    }
                }
            }
            var listHTs = hotelToUpdate.Reduccion_Hotel.ToList();
            foreach (var tipo in listHTs)
            {
                if (listint.Contains(tipo.idReduccion) == false)
                {
                    hotelToUpdate.Reduccion_Hotel.Remove(tipo);
                    db.SaveChanges();
                }
            }
        }

        //----------------------------

        // GET: /Hotel/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotel.Include(i => i.Tipol_HotelTipol).Single(i => i.idHotel == id);
            PopulateAssignedCourseData(hotel);
            PopulateAssignedSuplementData(hotel);
            PopulateAssignedReduccionData(hotel);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCadena = new SelectList(db.Cadena, "idCadena", "cadena_", hotel.idCadena);
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "categoria1", hotel.idCategoria);
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1", hotel.idDestino);
            return View(hotel);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // POST: /Hotel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? idHotel, [Bind(Include = "idHotel,nombre_hotel,idDestino,descripcion_hotel,direccion,foto_hotel1,foto_hote2,foto_hotel3,idCategoria,idCadena,idSubdetino,telefono,fax,sitioWeb,facilidades")] Hotel hotel, HttpPostedFileBase picture, HttpPostedFileBase picture2, HttpPostedFileBase picture3, string[] selectedCourses, string[] selectedSuplements, string[] selectedReductions, string State, string[] selectedDesc, string[] selectedDescNew)
        {
            try
            {
            var listDesc = new List<string>();
            var listDescComb = Incredible(selectedDesc, "Choose One");
            var listDescNew = Incredible(selectedDescNew, "");
            listDesc.InsertRange(listDesc.Count, listDescComb.ToList());
            listDesc.InsertRange(listDesc.Count, listDescNew.ToList());
            listDesc.Sort();
            var hotelToUpdate = db.Hotel.Include(i => i.Tipol_HotelTipol).Single(i => i.idHotel == idHotel);
            string rutaimg = "";
            string rutaimg2 = "";
            string rutaimg3 = "";

            if (picture == null)
            {
                if (hotelToUpdate.foto_hotel1 != null)
                {
                    rutaimg = hotelToUpdate.foto_hotel1;
                }
            }
            else if (picture.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
                if (!extensions.Contains(picture.FileName.Substring(picture.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                }
                else if (picture.ContentLength > maxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                }
                else
                {
                    //TO:DO
                    var fileName = System.IO.Path.GetFileName(picture.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/hotel"), fileName);
                    picture.SaveAs(path);
                    rutaimg = "/TripAgency/Content/upload/hotel/" + fileName;
                }
            }

            if (picture2 == null)
            {
                if (hotelToUpdate.foto_hote2 != null)
                {
                    rutaimg2 = hotelToUpdate.foto_hote2;
                }
            }
            else if (picture2.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
                if (!extensions.Contains(picture2.FileName.Substring(picture2.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                }
                else if (picture2.ContentLength > maxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                }
                else
                {
                    //TO:DO
                    var fileName = System.IO.Path.GetFileName(picture2.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/hotel"), fileName);
                    picture2.SaveAs(path);
                    rutaimg2 = "/TripAgency/Content/upload/hotel/" + fileName;
                }
            }

            if (picture3 == null)
            {
                if (hotelToUpdate.foto_hotel3 != null)
                {
                    rutaimg3 = hotelToUpdate.foto_hotel3;
                }
            }
            else if (picture3.ContentLength > 0)
            {
                const int maxContentLength = 1024 * 1024 * 5; //5 MB
                var extensions = new string[] { ".jpg", ".JPG", ".PNG", ".gif", ".GIF", ".png", ".pdf", ".ico", ".ICO" };
                if (!extensions.Contains(picture3.FileName.Substring(picture3.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", extensions));
                }
                else if (picture3.ContentLength > maxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                }
                else
                {
                    //TO:DO
                    var fileName = System.IO.Path.GetFileName(picture3.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath(@"~/Content/upload/hotel"), fileName);
                    picture3.SaveAs(path);
                    rutaimg3 = "/TripAgency/Content/upload/hotel/" + fileName;
                }
            }
           
                if (ModelState.IsValid)
                {
                    hotelToUpdate.foto_hotel1 = rutaimg;
                    hotelToUpdate.foto_hote2 = rutaimg2;
                    hotelToUpdate.foto_hotel3 = rutaimg3;
                    hotelToUpdate.nombre_hotel = hotel.nombre_hotel;
                    hotelToUpdate.descripcion_hotel = hotel.descripcion_hotel;
                    hotelToUpdate.direccion = hotel.direccion;
                    hotelToUpdate.idCategoria = hotel.idCategoria;
                    hotelToUpdate.idCadena = hotel.idCadena;
                    hotelToUpdate.idDestino = hotel.idDestino;
                    hotelToUpdate.telefono = hotel.telefono;
                    hotelToUpdate.sitioWeb = hotel.sitioWeb;
                    hotelToUpdate.fax = hotel.fax;
                    hotelToUpdate.facilidades = hotel.facilidades;
                    UpdateInstructorCourses(selectedCourses, listDesc.ToArray(), hotelToUpdate);
                    UpdateInstructorSuplementos(selectedSuplements, hotelToUpdate);
                    UpdateInstructorReduccipon(selectedReductions, hotelToUpdate);
                    db.Entry(hotelToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            ViewBag.idCadena = new SelectList(db.Cadena, "idCadena", "cadena_", hotel.idCadena);
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "categoria1", hotel.idCategoria);
            ViewBag.idDestino = new SelectList(db.Destino, "idDestino", "destino1");
            PopulateAssignedCourseData(hotel);
            PopulateAssignedSuplementData(hotel);
            PopulateAssignedReduccionData(hotel);
            return View(hotel);
            }
            catch (Exception e1)
            {
                string sms = e1.Message;
                return View("Error");
                throw;
            }
        }

        // GET: /Hotel/Delete/5
        public ActionResult Delete(int? id)
        {
            try{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotel.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
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

        // POST: /Hotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Hotel hotel = db.Hotel.Find(id);
            int cant = db.Tipol_HotelTipol.ToList().Count;
            if (cant != 0)
            {
                foreach (var tipo in db.Tipol_HotelTipol.ToList())
                {
                    if (tipo.idhotel == hotel.idHotel)
                    {
                        db.Tipol_HotelTipol.Remove(tipo);
                        db.SaveChanges();
                    }
                }
            }
            else
                if (cant == 0)
                {
                
                }
            int cantsupl = db.Suplemento_Hotel.ToList().Count;
            if (cantsupl != 0)
            {
                foreach (var tipo in db.Suplemento_Hotel.ToList())
                {
                    if (tipo.idhotel == hotel.idHotel)
                    {
                        db.Suplemento_Hotel.Remove(tipo);
                        db.SaveChanges();
                    }
                }
            }
            else
                if (cantsupl == 0)
                {

                }
            int cantred = db.Reduccion_Hotel.ToList().Count;
            if (cantsupl != 0)
            {
                foreach (var tipo in db.Reduccion_Hotel.ToList())
                {
                    if (tipo.idhotel == hotel.idHotel)
                    {
                        db.Reduccion_Hotel.Remove(tipo);
                        db.SaveChanges();
                    }
                }
            }
            else
                if (cantsupl == 0)
                {

                }
            
            db.Hotel.Remove(hotel);
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
