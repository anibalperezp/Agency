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
    
    public partial class NomTempAvionClase
    {
        public int idNomTempAvionClase { get; set; }
        public Nullable<int> idVuelo { get; set; }
        public Nullable<int> idTemporadaVuelo { get; set; }
        public Nullable<double> tarifa { get; set; }
        public Nullable<int> idClase { get; set; }
    
        public virtual Avion Avion { get; set; }
        public virtual Clase Clase { get; set; }
        public virtual TemporadaAvion TemporadaAvion { get; set; }
    }
}
