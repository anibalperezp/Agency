using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class TarifaHotelModel
    {
        private static List<int> tipolist = new List<int>();
        private static List<int> suplist = new List<int>();
        private static List<int> redlist = new List<int>();
        private static TarifaHotelModel instance;

        private TarifaHotelModel()
        {
        }

        public static TarifaHotelModel getInstance()
        {
            if(instance == null)
            {
                instance = new TarifaHotelModel();
            }
            return instance; 
        }

        //**************************************Tipologias***************************************************
        public List<string> GetPeriodosHotel(string hotel, string idanno, string idEstacion)
        {
            tipolist = new List<int>();
            TripAgencyEntities db = new TripAgencyEntities();
            int idhotel = Convert.ToInt32(hotel);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listperiodos = new List<string>();
            var list = new List<int>();
            if (db.Temporada_Hotel.ToList().Count != 0)
            {
                foreach (var h1 in db.Temporada_Hotel.ToList())
                {
                    if (h1.idhotel == idhotel && h1.idanno == anno && h1.idEstacion == estacion)
                    {
                        listperiodos.Add(h1.periodo);
                        list.Add(h1.idTemporadaHotel);
                    }
                }
            }
            tipolist = list;
            return listperiodos;
        }

        

        public List<string> GetPeriodosHotelR(string hotel, string idanno, string idEstacion)
        {
            redlist=new List<int>();
            TripAgencyEntities db = new TripAgencyEntities();
            int idhotel = Convert.ToInt32(hotel);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listperiodos = new List<string>();
            var list = new List<int>();
            if (db.Temporada_Reduccion.ToList().Count != 0)
            {
                foreach (var h1 in db.Temporada_Reduccion.ToList())
                {
                    if (h1.idhotel == idhotel && h1.idanno == anno && h1.idEstacion == estacion)
                    {
                        listperiodos.Add(h1.periodo);
                        list.Add(h1.idTemporada_Reduccion);
                    }
                }
            }
            redlist = list;
            return listperiodos;
        }

        public List<string> GetPeriodosHotelS(string hotel, string idanno, string idEstacion)
        {
            suplist=new List<int>();
            TripAgencyEntities db = new TripAgencyEntities();
            int idhotel = Convert.ToInt32(hotel);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listperiodos = new List<string>();
            var list = new List<int>();
            if (db.Temporada_Suplemento.ToList().Count != 0)
            {
                foreach (var h1 in db.Temporada_Suplemento.ToList())
                {
                    if (h1.idhotel == idhotel && h1.idanno == anno && h1.idEstacion == estacion)
                    {
                        listperiodos.Add(h1.periodo);
                        list.Add(h1.idSuplementoHotel);
                    }
                }
            }
            suplist = list;
            return listperiodos;
        }

        //**************************************Suplemento***************************************************

        public List<string> Gettipotarifa(string hotel, string idanno, string idEstacion)
        {
            var db = new TripAgencyEntities();
            int idHotel = Convert.ToInt32(hotel);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listtarifas = new List<string>();
            int k = 0;
            int i = 0;
            int posk = -1;

            if (db.Temporada_Hotel.ToList().Count != 0)
            {
                foreach (var h2 in db.Temporada_Hotel.ToList())
                {
                    if (h2.Hotel.idHotel == idHotel && h2.idanno == anno && h2.idEstacion == estacion)
                    {
                        if (h2.NomTempTipHotel.Count != 0)
                        {
                            foreach (var h3 in h2.NomTempTipHotel)
                            {
                                string tipologia = h3.Tipologia.tipologia1;
                                if (listtarifas.Contains(tipologia))
                                {
                                    int j = 0;
                                    // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
                                    while (j < listtarifas.Count)
                                    {
                                        if (listtarifas[j].Equals(tipologia))
                                        {
                                            if (k == 1)
                                            {
                                                listtarifas.Insert(j + 6, h3.tarifa.ToString());
                                                posk += k + 1;
                                                i++;
                                            }
                                            else if (k == 2)
                                            {
                                                listtarifas.Insert(j + 6, h3.tarifa.ToString());
                                                posk += k + 1;
                                                i++;
                                            }
                                            else if (k > 2)
                                            {
                                                posk += k + 1;
                                                listtarifas.Insert(j + k + 5, h3.tarifa.ToString());
                                            }
                                        }
                                        j++;
                                    }
                                }
                                if (!listtarifas.Contains(tipologia))
                                {
                                    listtarifas.Add(tipologia);
                                    listtarifas.Add(h3.maxninno.ToString());
                                    listtarifas.Add(h3.maxadult.ToString());
                                    listtarifas.Add(h3.minninno.ToString());
                                    listtarifas.Add(h3.minadult.ToString());
                                    listtarifas.Add(h3.tarifa.ToString());
                                }
                            }
                            k++;
                            i = k;
                            posk = -1;
                        }
                    }
                }
            }
            return listtarifas;
        }

        public List<string> GettipotarifaS(string hotel, string idanno, string idEstacion)
        {
            TripAgencyEntities db = new TripAgencyEntities();
            int idhotel = Convert.ToInt32(hotel);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listtarifas = new List<string>();
            if (db.Suplemento_Hotel.ToList().Count != 0)
            {
                foreach (var h1 in db.Suplemento_Hotel.ToList())
                {
                    if (h1.idhotel == idhotel)
                    {
                        if (h1.Hotel.Temporada_Suplemento.ToList().Count != 0)
                        {
                            listtarifas.Add(h1.Suplemento.suplemento1);
                            foreach (var h2 in db.Temporada_Suplemento.ToList())
                            {
                                if (h2.idhotel == idhotel && h2.idanno == anno && h2.idEstacion == estacion)
                                {
                                    foreach (var h3 in db.NomTempSupHotel.ToList())
                                    {
                                        if (h3.idsuplemento == h1.idSuplemento && h3.idSuplementoHotel == h2.idSuplementoHotel)
                                        {
                                            listtarifas.Add(h3.tarifa.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return listtarifas;
        }

        public List<string> GettipotarifaR(string hotel, string idanno, string idEstacion)
        {
            TripAgencyEntities db = new TripAgencyEntities();
            int idhotel = Convert.ToInt32(hotel);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listtarifas = new List<string>();
            if (db.Reduccion_Hotel.ToList().Count != 0)
            {
                foreach (var h1 in db.Reduccion_Hotel.ToList())
                {
                    if (h1.idhotel == idhotel)
                    {
                        if (h1.Hotel.Temporada_Reduccion.ToList().Count != 0)
                        {
                            listtarifas.Add(h1.Reduccion.reduccion1);
                            foreach (var h2 in db.Temporada_Reduccion.ToList())
                            {
                                if (h2.idhotel == idhotel && h2.idanno == anno && h2.idEstacion == estacion)
                                {
                                    foreach (var h3 in db.NomTempRedHotel.ToList())
                                    {
                                        if (h3.idreduccion == h1.idReduccion && h3.idTemporada_Reduccion == h2.idTemporada_Reduccion)
                                        {
                                            listtarifas.Add(h3.tarifa.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return listtarifas;
        }
        //**************************************Reducción***************************************************

        public string[] PicarLista(string[] tarifas,int num)
        {
            List<string> listtar = tarifas.ToList();
            var list = new List<string>();
            for (int i = num; i < listtar.Count; i++)
            {
                var su = listtar[i];
                if (su[0] == '0' || su[0] == '1' || su[0] == '2' || su[0] == '3' || su[0] == '4' || su[0] == '5' || su[0] == '6' || su[0] == '7' || su[0] == '8' || su[0] == '9')
                {
                    list.Add(listtar[i]);
                }else
                    {
                        return list.ToArray();
                    }
            }
            return list.ToArray();
        }

        public string[] Tipos(string[] tarifas)
        {
            var list = new List<string>();
            if (tarifas != null)
            {
                List<string> listtar = tarifas.ToList();
                for (int i = 0; i < listtar.Count; i++)
                {
                    var su = listtar[i];
                    if (su[0] != '0' && su[0] != '1' && su[0] != '2' && su[0] != '3' && su[0] != '4' && su[0] != '5' && su[0] != '6' && su[0] != '7' && su[0] != '8' && su[0] != '9')
                    {
                        list.Add(listtar[i]);
                    }
                }
            }
            return list.ToArray();
        }

        public string[] TiposRed(string[] tarifas)
        {
            var list = new List<string>();
            if (tarifas != null)
            {
                List<string> listtar = tarifas.ToList();
                for (int i = 0; i < listtar.Count; i++)
                {
                    var su = listtar[i];
                    if (su[0] != '0' && su[0] != '1' && su[0] != '2' && su[0] != '3' && su[0] != '4' && su[0] != '5' && su[0] != '6' && su[0] != '7' && su[0] != '8' && su[0] != '9')
                    {
                        list.Add(listtar[i]);
                    }
                }
            }
            return list.ToArray();
        }

        public string[] TiposSup(string[] tarifas)
        {
            var list = new List<string>();
            if (tarifas!=null)
            {
                List<string> listtar = tarifas.ToList();
                for (int i = 0; i < listtar.Count; i++)
                {
                    var su = listtar[i];
                    if (su[0] != '0' && su[0] != '1' && su[0] != '2' && su[0] != '3' && su[0] != '4' && su[0] != '5' && su[0] != '6' && su[0] != '7' && su[0] != '8' && su[0] != '9')
                    {
                        list.Add(listtar[i]);
                    }
                }
            }
            
            return list.ToArray();
        }
        //----------------------------------------

        public bool updateTipolTarifa(string[] tarifas, int idTipoTarifa)
        {
            bool found = false;
            TripAgencyEntities db = new TripAgencyEntities();
            int counttar = 0;
            string[] list = Tipos(tarifas);
            List<NomTempTipHotel> listTempTip= db.NomTempTipHotel.ToList();
            if (idTipoTarifa != null)
            {
                for (int i = 1; i < tarifas.Length; i++)
                {
                    string[] lista = PicarLista(tarifas, i);
                    i += lista.Length;
                    for (int k = 0; k < tipolist.Count; k++)
                    {
                        for (int j = 0; j < listTempTip.Count; j++)
                        {
                           NomTempTipHotel h2 = listTempTip[j];
                           if (h2.idTemporadaHotel == tipolist[k] && h2.Tipologia.tipologia1.Equals(list[counttar]))
                           {
                              h2.maxninno = Convert.ToInt32(lista[0]);
                              h2.maxadult = Convert.ToInt32(lista[1]);
                              h2.minninno = Convert.ToInt32(lista[2]);
                              h2.minadult = Convert.ToInt32(lista[3]);
                              h2.tarifa = Convert.ToDouble(lista[k + 4]);
                              h2.idtipotarifa = idTipoTarifa;
                              found = true;
                              db.Entry(h2).State = EntityState.Modified;
                              db.SaveChanges();
                           }
                        }
                    }
                    counttar++;
                }
            }
            tipolist = new List<int>();
            return found;
        }

        public bool updateReduTarifa(string[] tarifas, int idTipoTarifa)
        {
            bool found = false;
            TripAgencyEntities db = new TripAgencyEntities();
            int counttar = 0;
            string[] list = TiposRed(tarifas);
            List<NomTempRedHotel> listTempTip = db.NomTempRedHotel.ToList();
            if (idTipoTarifa != null)
            {
                for (int i = 1; i < tarifas.Length; i++)
                {
                    string[] lista = PicarLista(tarifas, i);
                    i += lista.Length;
                    for (int k = 0; k < redlist.Count; k++)
                    {
                        for (int j = 0; j < listTempTip.Count; j++)
                        {
                            NomTempRedHotel h2 = listTempTip[j];
                            if (h2.idTemporada_Reduccion == redlist[k] && h2.Reduccion.reduccion1.Equals(list[counttar]))
                            {
                                h2.tarifa = Convert.ToDouble(lista[k]);
                                h2.idtipotarifa = idTipoTarifa;
                                found = true;
                                db.Entry(h2).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    counttar++;
                }
            }
            redlist = new List<int>();
            return found;
        }

        public bool updateSupleTarifa(string[] tarifas, int idTipoTarifa)
        {
            bool found = false;
            TripAgencyEntities db = new TripAgencyEntities();
            int counttar = 0;
            string[] list = TiposSup(tarifas);
            List<NomTempSupHotel> listTempTip = db.NomTempSupHotel.ToList();
            if (idTipoTarifa != null)
            {
                for (int i = 1; i < tarifas.Length; i++)
                {
                    string[] lista = PicarLista(tarifas, i);
                    i += lista.Length;
                    for (int k = 0; k < suplist.Count; k++)
                    {
                        for (int j = 0; j < listTempTip.Count; j++)
                        {
                            NomTempSupHotel h2 = listTempTip[j];
                            if (h2.idSuplementoHotel == suplist[k] && h2.Suplemento.suplemento1.Equals(list[counttar]))
                            {
                                h2.tarifa = Convert.ToDouble(lista[k]);
                                h2.idtipotarifa = idTipoTarifa;
                                found = true;
                                db.Entry(h2).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    counttar++;
                }
            }
            
            suplist = new List<int>();
            return found;
        }

        //*************************************************************************************

        public string GetPlanBase(string hotel, string idanno, string idEstacion)
        {
            TripAgencyEntities db = new TripAgencyEntities();
            int idhotel = Convert.ToInt32(hotel);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            string planbase = "";
            if (db.Reduccion_Hotel.ToList().Count != 0)
            {
                foreach (var h1 in db.Reduccion_Hotel.ToList())
                {
                    if (h1.idhotel == idhotel)
                    {
                        if (h1.Hotel.Temporada_Reduccion.ToList().Count != 0)
                        {
                            foreach (var h2 in db.Temporada_Reduccion.ToList())
                            {
                                if (h2.idhotel == idhotel && h2.idanno == anno && h2.idEstacion == estacion)
                                {
                                    foreach (var h3 in db.NomTempRedHotel.ToList())
                                    {
                                        if (h3.idreduccion == h1.idReduccion && h3.idTemporada_Reduccion == h2.idTemporada_Reduccion)
                                        {
                                             var hotelToUpdate = db.Tipo_Tarifa.Single(i => i.idTipoTarifa == h3.idtipotarifa);
                                             planbase = hotelToUpdate.tipo_tarifa1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return planbase;
        }
    }
}