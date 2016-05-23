using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripAgency.Models
{
    public class Tipo_Empresa_Asigned
    {
        public int idTipoEmpresa { get; set; }
        public string tipoempresa { get; set; }
        public bool assigned { get; set; }
    }
}