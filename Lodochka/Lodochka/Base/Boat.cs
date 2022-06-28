//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lodochka.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class Boat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Boat()
        {
            this.AccessoriesToBoat = new HashSet<AccessoriesToBoat>();
            this.Order = new HashSet<Order>();
        }
    
        public int boat_ID { get; set; }
        public string Model { get; set; }
        public string BoatType { get; set; }
        public Nullable<int> NumberOfRowers { get; set; }
        public Nullable<bool> Mast { get; set; }
        public string Colour { get; set; }
        public string Wood { get; set; }
        public Nullable<decimal> BasePrice { get; set; }
        public Nullable<decimal> VAT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccessoriesToBoat> AccessoriesToBoat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}