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
    
    public partial class Tipo_Tarifa
    {
        public Tipo_Tarifa()
        {
            this.NomTempRedHotel = new HashSet<NomTempRedHotel>();
            this.NomTempSupHotel = new HashSet<NomTempSupHotel>();
            this.NomTempTipHotel = new HashSet<NomTempTipHotel>();
        }
    
        public int idTipoTarifa { get; set; }
        public string tipo_tarifa1 { get; set; }
    
        public virtual ICollection<NomTempRedHotel> NomTempRedHotel { get; set; }
        public virtual ICollection<NomTempSupHotel> NomTempSupHotel { get; set; }
        public virtual ICollection<NomTempTipHotel> NomTempTipHotel { get; set; }
    }
}
