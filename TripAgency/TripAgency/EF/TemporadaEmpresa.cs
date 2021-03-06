//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace TripAgency.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class TemporadaEmpresa
    {
        public TemporadaEmpresa()
        {
            this.TarifaCarro = new HashSet<TarifaCarro>();
        }
    
        public int idTemporadaEmpresa { get; set; }
        public string periodo { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime inicio { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime fin { get; set; }
        public string descripcion_periodo { get; set; }
        public int idanno { get; set; }
        public int idEmpresa { get; set; }
        public int idEstacion { get; set; }
        public int idTemporada { get; set; }
    
        public virtual Anno Anno { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Estacion Estacion { get; set; }
        public virtual ICollection<TarifaCarro> TarifaCarro { get; set; }
        public virtual Temporada Temporada { get; set; }
    }
}
