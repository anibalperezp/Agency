using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class TarifaExcModel
    {
         private static List<int> _llavesTarifaExc1 = new List<int>();
         private static List<string> per = new List<string>();

         private static TarifaExcModel instance;

        private TarifaExcModel()
        {
        }

        public static TarifaExcModel getInstance()
        {
            if(instance == null)
            {
                instance = new TarifaExcModel();
            }
            return instance; 
        }

        public List<string> GetPeriodosExc(string destino, string idanno, string idEstacion)
        {
            _llavesTarifaExc1 = new List<int>();
            per = new List<string>();
            TripAgencyEntities db = new TripAgencyEntities();
            int idDestino = Convert.ToInt32(destino);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listexc = new List<string>();
            if (db.TemporadaExcursion.ToList().Count != 0)
            {
                foreach (var h1 in db.TemporadaExcursion.ToList())
                {
                    if (h1.Excursion.Destino.idDestino == idDestino && h1.idanno == anno && h1.idEstacion == estacion)
                    {
                        if (!listexc.Contains(h1.periodo))
                        {
                            listexc.Add(h1.periodo);
                        }
                    }
                }
            }
            per = listexc;
            return listexc;
        }

        //****************
        public List<string> Getexctarifa(string destino, string idanno, string idEstacion)
        {
            TripAgencyEntities db = new TripAgencyEntities();
            int idDestino = Convert.ToInt32(destino);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listtarifas = new List<string>();
            if (db.TemporadaExcursion.ToList().Count != 0)
            {
                foreach (var h2 in db.TemporadaExcursion.ToList())
                {

                    if (h2.Excursion.Destino.idDestino == idDestino && h2.idanno == anno && h2.idEstacion == estacion)
                    {
                        if (listtarifas.Contains(h2.Excursion.nombre_excursion) == false)
                        {
                            listtarifas.Add(h2.Excursion.nombre_excursion);
                        }
                        listtarifas.Add(h2.precio_tarifa.ToString());
                        _llavesTarifaExc1.Add(h2.idtemporadaexcursion);
                    }
                }
            }
            return listtarifas;
        }
        //****************
        public bool updateExcTarifa(string[] tarifas)
        {
            bool found = false;
            TripAgencyEntities db = new TripAgencyEntities();
            int counttar = 0;
            string[] list = Tipos(tarifas);
            List<TemporadaExcursion> listTempTip = db.TemporadaExcursion.ToList();
            for (int i = 1; i < tarifas.Length; i++)
            {
                string[] lista = PicarLista(tarifas, i);
                i += lista.Length;
                for (int m = 0; m < per.Count; m++)
                {
                    for (int k = 0; k < _llavesTarifaExc1.Count; k++)
                    {
                        for (int j = 0; j < listTempTip.Count; j++)
                        {
                            TemporadaExcursion h2 = listTempTip[j];
                            if (h2.idtemporadaexcursion == _llavesTarifaExc1[k] &&
                                h2.Excursion.nombre_excursion.Equals(list[counttar]))
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
            _llavesTarifaExc1 = new List<int>();
            per=new List<string>();
            return found;
        }

        public string[] PicarLista(string[] tarifas, int num)
        {
            List<string> listtar = tarifas.ToList();
            var list = new List<string>();
            for (int i = num; i < listtar.Count; i++)
            {
                if (listtar[i].Length <= 7)
                {
                    list.Add(listtar[i]);
                }
                if (listtar[i].Length > 7)
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
                if (listtar[i].Length > 7)
                {
                    list.Add(listtar[i]);
                }
            }
            return list.ToArray();
        }

    }
}