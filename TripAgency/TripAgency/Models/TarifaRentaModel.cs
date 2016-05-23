using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class TarifaRentaModel
    {
        private static List<int> llavesTarifaRent = new List<int>();
        private static List<string> per = new List<string>();
        private static TarifaRentaModel instance;

        private TarifaRentaModel()
        {
        }

         public static TarifaRentaModel getInstance()
        {
            if(instance == null)
            {
                instance = new TarifaRentaModel();
            }
            return instance; 
        }

        public List<string> GetPeriodosExc(string renta, string idanno, string idEstacion)
        {
            llavesTarifaRent = new List<int>();
            TripAgencyEntities db = new TripAgencyEntities();
            int idRenta = Convert.ToInt32(renta);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listpaquetes = new List<string>();
            if (db.TemporadaRental.ToList().Count != 0)
            {
                foreach (var h1 in db.TemporadaRental.ToList())
                {
                    if (h1.idRental == idRenta && h1.idanno == anno && h1.idEstacion == estacion)
                    {
                        listpaquetes.Add(h1.periodo);
                    }
                }
            }
            per = listpaquetes;
            return listpaquetes;
        }

        //****************
        public List<string> Getexctarifa(string renta, string idanno, string idEstacion)
        {
            TripAgencyEntities db = new TripAgencyEntities();
            int idRental = Convert.ToInt32(renta);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listtarifas = new List<string>();
            if (db.TemporadaRental.ToList().Count != 0)
            {
                foreach (var h2 in db.TemporadaRental.ToList())
                {
                    if (h2.Rental.idRental == idRental && h2.idanno == anno && h2.idEstacion == estacion)
                    {
                        if (listtarifas.Contains(h2.Rental.nombre) == false)
                        {
                            listtarifas.Add(h2.Rental.nombre);
                        }
                        listtarifas.Add(h2.precio_tarifa.ToString());
                        llavesTarifaRent.Add(h2.idTemporadaRental);
                    }
                }
            }
            return listtarifas;
        }

        //****************

        public string[] PicarLista(string[] tarifas, int num)
        {
            List<string> listtar = tarifas.ToList();
            var list = new List<string>();
            for (int i = num; i < listtar.Count; i++)
            {
                if (listtar[i].Length <= 6)
                {
                    list.Add(listtar[i]);
                }
                if (listtar[i].Length > 6)
                {
                    return list.ToArray();
                }
            }
            return list.ToArray();
        }

        public string[] Tipos(string[] tarifas)
        {
            List<string> listtar = tarifas.ToList();
            var list = new List<string>();
            for (int i = 0; i < listtar.Count; i++)
            {
                if (listtar[i].Length > 6)
                {
                    list.Add(listtar[i]);
                }
            }
            return list.ToArray();
        }


        public bool updateExcTarifa(string[] tarifas)
        {
            bool found = false;
            TripAgencyEntities db = new TripAgencyEntities();
            int counttar = 0;
            string[] list = Tipos(tarifas);
            List<TemporadaRental> listTempTip = db.TemporadaRental.ToList();
            for (int i = 1; i < tarifas.Length; i++)
            {
                string[] lista = PicarLista(tarifas, i);
                i += lista.Length;
                for (int m = 0; m < per.Count; m++)
                {
                    for (int k = 0; k < llavesTarifaRent.Count; k++)
                    {
                        for (int j = 0; j < listTempTip.Count; j++)
                        {
                            TemporadaRental h2 = listTempTip[j];
                            if (h2.idTemporadaRental == llavesTarifaRent[k] && h2.Rental.nombre.Equals(list[counttar]))
                            {
                                h2.precio_tarifa = Convert.ToDouble(lista[m]);
                                found = true;
                                db.Entry(h2).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    counttar++;
                }
            }
            llavesTarifaRent = new List<int>();
            per= new List<string>(); 
            return found;
        }
    }
}