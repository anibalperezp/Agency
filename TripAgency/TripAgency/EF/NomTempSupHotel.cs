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
    
    public partial class NomTempSupHotel
    {
        public int idtemphotsup { get; set; }
        public int idSuplementoHotel { get; set; }
        public int idsuplemento { get; set; }
        public Nullable<double> tarifa { get; set; }
        public Nullable<int> idtipotarifa { get; set; }
    
        public virtual Temporada_Suplemento Temporada_Suplemento { get; set; }
        public virtual Suplemento Suplemento { get; set; }
        public virtual Tipo_Tarifa Tipo_Tarifa { get; set; }
    }
}