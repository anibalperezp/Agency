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
    
    public partial class Tipologia
    {
        public Tipologia()
        {
            this.NomTempTipHotel = new HashSet<NomTempTipHotel>();
            this.Tipol_HotelTipol = new HashSet<Tipol_HotelTipol>();
        }
    
        public int idTiologia { get; set; }
        public string tipologia1 { get; set; }
        public Nullable<int> idTarificacion { get; set; }
    
        public virtual ICollection<NomTempTipHotel> NomTempTipHotel { get; set; }
        public virtual Tarificacion Tarificacion { get; set; }
        public virtual ICollection<Tipol_HotelTipol> Tipol_HotelTipol { get; set; }
    }
}