using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class TarifaCarroModel
    {

        public List<string> GetPeriodosDeTarifaCarro(string idanno, string idTemporada, string idempresa)
        {
            var db = new TripAgencyEntities();
            int idAnno = Convert.ToInt32(idanno);
            int idtemporada = Convert.ToInt32(idTemporada);
            int idEmpresa = Convert.ToInt32(idempresa);
            var listcars = new List<string>();

            if (db.TemporadaEmpresa.ToList().Count != 0)
            {
                foreach (var car1 in db.TemporadaEmpresa.ToList())
                {
                    if (car1.idanno == idAnno && car1.idTemporada == idtemporada && car1.idEmpresa == idEmpresa)
                    {
                        listcars.Add(car1.periodo);
                    }
                }
            }
            return listcars;
        }

        public string price(int idEmpresa, DateTime d)
        {
            var db = new TripAgencyEntities();
            List<TemporadaEmpresa> list = db.TemporadaEmpresa.ToList();
            double? min = 0;
            foreach (var v1 in list)
            {
                if (v1.inicio <= d && v1.fin >= d)
                {
                    foreach (var v2 in v1.TarifaCarro)
                    {
                        if (idEmpresa == v2.Empresa.idEmpresa)
                        {
                            if (v2.Rangos_Tarifa.Count != 0)
                            {
                                min = v2.Rangos_Tarifa.ElementAt(0).tarifa;
                            }
                            else
                                if (v2.Rangos_Tarifa.Count == 0)
                                {
                                    min = v2.valor;
                                }
                        }
                    }
                }
            }
            return min.ToString();
        }

        public string priceInADay(int idEmpresa, DateTime d,int pnum)
        {
            var db = new TripAgencyEntities();
            List<TemporadaEmpresa> list = db.TemporadaEmpresa.ToList();
            double? min = 0;
            foreach (var v1 in list)
            {
                if (v1.inicio <= d && v1.fin >= d)
                {
                    foreach (var v2 in v1.TarifaCarro)
                    {
                        if (idEmpresa == v2.Empresa.idEmpresa)
                        {
                            if (v2.Rangos_Tarifa.Count != 0)
                            {
                                int a= isTheNumber(pnum);
                                foreach (var v3 in v2.Rangos_Tarifa)
                                {
                                    if (v3.idrango==a)
                                    {
                                        min = v3.tarifa;
                                    }

                                }
                            }
                            else
                                if (v2.Rangos_Tarifa.Count == 0)
                                {
                                    min = v2.valor;
                                }
                        }
                    }
                }
            }
            return min.ToString();
        }

        public int isTheNumber(int pnum)
        {
            int found = 0;
            var db = new TripAgencyEntities();
            foreach (var r in db.Rango)
            {
                if (!r.rango1.Contains("a"))
                {
                    string[] numbers = r.rango1.Split('-');
                    int number1 = int.Parse(numbers[0]);
                    int number2 = int.Parse(numbers[1]);
                    if (number1 == pnum)
                        {
                            found = r.idrango;
                        }
                        else
                            if (number1 < pnum && number2 > pnum)
                            {
                                found = r.idrango;
                            }
                            else
                                if (number2 == pnum)
                                {
                                    found = r.idrango;
                                }
                }
                else
                    if (r.rango1.Contains("a"))
                    {
                        found = r.idrango;
                    }
            }
            return found;
        }
    }
}