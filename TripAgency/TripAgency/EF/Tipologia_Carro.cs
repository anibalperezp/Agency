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
    
    public partial class Tipologia_Carro
    {
        public Tipologia_Carro()
        {
            this.Carro = new HashSet<Carro>();
            this.TarifaCarro = new HashSet<TarifaCarro>();
        }
    
        public int idtipologia_carro { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
    
        public virtual ICollection<Carro> Carro { get; set; }
        public virtual ICollection<TarifaCarro> TarifaCarro { get; set; }
    }
}
