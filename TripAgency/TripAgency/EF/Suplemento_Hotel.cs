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
    
    public partial class Suplemento_Hotel
    {
        public int idsuplementohotel { get; set; }
        public Nullable<int> idhotel { get; set; }
        public Nullable<int> idSuplemento { get; set; }
        public Nullable<bool> asignado { get; set; }
        public Nullable<double> precio { get; set; }
        public Nullable<double> porciento { get; set; }
    
        public virtual Hotel Hotel { get; set; }
        public virtual Suplemento Suplemento { get; set; }
    }
}