//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project1.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class Accessory_
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accessory_()
        {
            this.AccToBoats_ = new HashSet<AccToBoats_>();
            this.OrderDetails_ = new HashSet<OrderDetails_>();
        }
    
        public int Accessory_ID { get; set; }
        public string AccName { get; set; }
        public string DescriptionOfAccessory { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public Nullable<int> Inventory { get; set; }
        public Nullable<int> OrderLevel { get; set; }
        public Nullable<int> OrderBatch { get; set; }
        public Nullable<int> Partner_ID { get; set; }
    
        public virtual Partner_ Partner_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccToBoats_> AccToBoats_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails_> OrderDetails_ { get; set; }
    }
}
