//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TripAgency.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Excursion
    {
        public Excursion()
        {
            this.TemporadaExcursion = new HashSet<TemporadaExcursion>();
        }
    
        public int idExcursion { get; set; }
        public string nombre_excursion { get; set; }
        public string descrpcion_excursion { get; set; }
        public int idempresa { get; set; }
        public Nullable<int> idDestino { get; set; }
        public string foto1 { get; set; }
        public string foto2 { get; set; }
    
        public virtual Destino Destino { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<TemporadaExcursion> TemporadaExcursion { get; set; }
    }
}
