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
    
    public partial class OficinaRenta
    {
        public OficinaRenta()
        {
            this.OficPerCar = new HashSet<OficPerCar>();
        }
    
        public int idOficina { get; set; }
        public string nombre { get; set; }
        public string imagen { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> idDestino { get; set; }
        public Nullable<int> idEmpresa { get; set; }
    
        public virtual Destino Destino { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<OficPerCar> OficPerCar { get; set; }
    }
}
