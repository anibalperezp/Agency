using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripAgency.Models
{
    public class Util
    {
        public string name(string stringMappath)
        {
            var ruta = "";
            string[] code = stringMappath.Split('\\');
            List<string> list = code.ToList();
            var re = list.FindIndex(l => l.Equals("TripAgency")) + 1;
            for (int i = re; i < list.Count; i++)
            {
                ruta += "/" + list.ElementAt(i);
            }
            //var pa = Directory.GetFiles(codigo); // me da el nombre de los ficheros en una ruta.
            //listName.Add(pa[0]);
            /* name.Add(pa[1]);
             name.Add(pa[2]);*/
            return ruta;
        }
    }
}