using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class TarifaVueloModel
    {
        private static List<int> llavesTarifaVuelos = new List<int>();
        private static List<string> per = new List<string>();
        private static TarifaVueloModel instance;

         private TarifaVueloModel()
        {
        }

         public static TarifaVueloModel getInstance()
        {
            if(instance == null)
            {
                instance = new TarifaVueloModel();
            }
            return instance; 
        }

        public List<string> GetPeriodosVuelos(string empresa, string idanno, string idEstacion)
        {
            llavesTarifaVuelos=new List<int>();
            TripAgencyEntities db = new TripAgencyEntities();
            int idEmpresa = Convert.ToInt32(empresa);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listvuelos = new List<string>();
            var list = new List<int>();
            if (db.TemporadaAvion.ToList().Count != 0)
            {
                foreach (var h1 in db.TemporadaAvion.ToList())
                {
                    if (h1.Empresa.idEmpresa == idEmpresa && h1.idanno == anno && h1.idEstacion == estacion)
                    {
                        listvuelos.Add(h1.periodo);
                        list.Add(h1.idTemporadaVuelo);
                    }
                }
            }
            llavesTarifaVuelos = list;
            per = listvuelos;
            return listvuelos;
        }

        //****************
        public List<string> Getvuelotarifa(string empresa, string idanno, string idEstacion)
        {
            TripAgencyEntities db = new TripAgencyEntities();
            int idEmpresa = Convert.ToInt32(empresa);
            int anno = Convert.ToInt32(idanno);
            int estacion = Convert.ToInt32(idEstacion);
            var listtarifas = new List<string>();
            int k = 0;
            int i = 0;
            int posk = -1;

            if (db.TemporadaAvion.ToList().Count != 0)
            {
                int cantTemp = cantTVuelos(idEmpresa, anno, estacion);
                foreach (var h2 in db.TemporadaAvion.ToList())
                {
                    if (h2.Empresa.idEmpresa == idEmpresa && h2.idanno == anno && h2.idEstacion == estacion)
                    {
                        foreach (var h3 in h2.NomTempAvionClase)
                        {
                            string clasevuelo = h3.Avion.nombre + " : " + h3.Clase.nombre;
                            if (listtarifas.Contains(clasevuelo))
                            {
                                int j = 0; 
                                // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
                                while (j < listtarifas.Count)
                                {
                                    if (listtarifas[j].Equals(clasevuelo))
                                    {
                                        if (k<2)
                                        {
                                            listtarifas.Insert(j + 2, h3.tarifa.ToString());
                                            //int pos = (llavesTarifaVuelos.Count - cantTemp) + i;
                                            //llavesTarifaVuelos.Insert(pos, h3.idNomTempAvionClase);
                                            i++;
                                        }
                                        else
                                            if (k >= 2)
                                            {
                                                posk += k + 1;
                                                listtarifas.Insert(j + k + 1, h3.tarifa.ToString());
                                                //llavesTarifaVuelos.Insert(posk, h3.idNomTempAvionClase);
                                            }
                                    }
                                    j++;
                                }
                            }
                            if (!listtarifas.Contains(clasevuelo))
                            {
                                listtarifas.Add(clasevuelo);
                                listtarifas.Add(h3.tarifa.ToString());
                                //llavesTarifaVuelos.Add(h3.idNomTempAvionClase);
                            }
                        }
                    }
                    k++;
                    i = k;
                    //t = k;
                    posk = -1;
                }
            }
            return listtarifas;
        }

        private int cantTVuelos(int idEmpresa, int anno, int estacion)
        {
            TripAgencyEntities db = new TripAgencyEntities();
            int count = 0;
            foreach (var h2 in db.TemporadaAvion.ToList())
            {
                if (h2.Empresa.idEmpresa == idEmpresa && h2.idanno == anno && h2.idEstacion == estacion)
                {
                    count++;
                }
            }
            return count;
        }


        //****************
        public bool updateVueloTarifa(string[] tarifas)
        {
            bool found = false;
            TripAgencyEntities db = new TripAgencyEntities();
            int counttar = 0;
            int countnam = 1;
            string[] list = Tipos(tarifas);
            List<NomTempAvionClase> listTempTip = db.NomTempAvionClase.ToList();
            for (int i = 1; i < tarifas.Length; i++)
            {
                string[] lista = PicarLista(tarifas, i);
                i += lista.Length;
                for (int m = 0; m < per.Count; m++)
                {
                    for (int k = 0; k < llavesTarifaVuelos.Count; k++)
                    {
                        string[] sw = list[counttar].Split(':');
                        string[] sc = sw[countnam].Split(' ');
                        for (int j = 0; j < listTempTip.Count; j++)
                        {
                            NomTempAvionClase h2 = listTempTip[j];
                            if (h2.idTemporadaVuelo == llavesTarifaVuelos[k] && sw[0].Contains(h2.Avion.nombre) == true &&
                                h2.Clase.nombre.Equals(sc[1]) == true)
                            {
                                h2.tarifa = Convert.ToDouble(lista[m]);
                                found = true;
                                db.Entry(h2).State = EntityState.Modified;
                                db.SaveChanges();
                                countnam++;
                            }
                        }
                    }
                    counttar++;
                    countnam = 1;
                }

            }
            llavesTarifaVuelos = new List<int>();
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