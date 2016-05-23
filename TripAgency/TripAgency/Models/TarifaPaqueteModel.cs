using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class TarifaPaqueteModel
    {
        private static List<int> llavesTarifaPaq = new List<int>();
        private static List<string> per = new List<string>();
        private static TarifaPaqueteModel instance;

        private TarifaPaqueteModel()
        {
        }

        public static TarifaPaqueteModel getInstance () {
            if(instance == null)
            {
                instance= new TarifaPaqueteModel();
            }
            return instance; 
        }

        public List<string> GetPeriodosPaquete(string empresa, string idanno, string idEstacion)
        {
            llavesTarifaPaq = new List<int>();
            TripAgencyEntities db = new TripAgencyEntities();
            int idEmpresa = Convert.ToInt32(empresa);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listpaquetes = new List<string>();
            if (db.TemporadaPaquete.ToList().Count != 0)
            {
                foreach (var h1 in db.TemporadaPaquete.ToList())
                {
                    if (h1.Paquete.Empresa.idEmpresa == idEmpresa && h1.idanno == anno && h1.idEstacion == estacion)
                    {
                        listpaquetes.Add(h1.periodo);
                    }
                }
            }
            per = listpaquetes;
            return listpaquetes;
        }

        //****************
        public List<string> Getpaquetetarifa(string empresa, string idanno, string idEstacion)
        {
            TripAgencyEntities db = new TripAgencyEntities();
            int idEmpresa = Convert.ToInt32(empresa);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listtarifas = new List<string>();
            if (db.TemporadaPaquete.ToList().Count != 0)
            {
                foreach (var h2 in db.TemporadaPaquete.ToList())
                {

                    if (h2.Paquete.Empresa.idEmpresa == idEmpresa && h2.idanno == anno && h2.idEstacion == estacion)
                    {
                        if (listtarifas.Contains(h2.Paquete.nombre_paquete) == false)
                        {
                            listtarifas.Add(h2.Paquete.nombre_paquete);
                        }
                        listtarifas.Add(h2.precio_tarifa.ToString());
                        llavesTarifaPaq.Add(h2.idtemporadapaquete);
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

        public bool updatePaqueteTarifa(string[] tarifas)
        {
            bool found = false;
            TripAgencyEntities db = new TripAgencyEntities();
            int counttar = 0;
            string[] list = Tipos(tarifas);
            List<TemporadaPaquete> listTempTip = db.TemporadaPaquete.ToList();
                for (int i = 1; i < tarifas.Length; i++)
                {
                    string[] lista = PicarLista(tarifas, i);
                    i += lista.Length;
                    for (int m = 0; m < per.Count; m++)
                    {
                        for (int k = 0; k < llavesTarifaPaq.Count; k++)
                        {
                            for (int j = 0; j < listTempTip.Count; j++)
                            {
                                TemporadaPaquete h2 = listTempTip[j];
                                if (h2.idtemporadapaquete == llavesTarifaPaq[k] && h2.Paquete.nombre_paquete.Equals(list[counttar]))
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
            llavesTarifaPaq = new List<int>();
            per=new List<string>();
            return found;
        }
    }
}