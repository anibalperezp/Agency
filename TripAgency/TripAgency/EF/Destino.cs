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
    
    public partial class Destino
    {
        public Destino()
        {
            this.Excursion = new HashSet<Excursion>();
            this.Hotel = new HashSet<Hotel>();
            this.OficinaRenta = new HashSet<OficinaRenta>();
            this.Subdestino = new HashSet<Subdestino>();
        }
    
        public int idDestino { get; set; }
        public string destino1 { get; set; }
        public string decripcion_destino { get; set; }
        public string foto_destino1 { get; set; }
        public string foto_destino2 { get; set; }
        public int idpais { get; set; }
    
        public virtual Pai Pai { get; set; }
        public virtual ICollection<Excursion> Excursion { get; set; }
        public virtual ICollection<Hotel> Hotel { get; set; }
        public virtual ICollection<OficinaRenta> OficinaRenta { get; set; }
        public virtual ICollection<Subdestino> Subdestino { get; set; }
    }
}