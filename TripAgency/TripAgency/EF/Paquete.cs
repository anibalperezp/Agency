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
    
    public partial class Paquete
    {
        public Paquete()
        {
            this.TemporadaPaquete = new HashSet<TemporadaPaquete>();
        }
    
        public int idPaquete { get; set; }
        public string nombre_paquete { get; set; }
        public string descripcion_paquete { get; set; }
        public int idEmpresa { get; set; }
        public string foto { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<TemporadaPaquete> TemporadaPaquete { get; set; }
    }
}
