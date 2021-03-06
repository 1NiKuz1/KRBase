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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Contract = new HashSet<Contract>();
            this.OrderDetails = new HashSet<OrderDetails>();
        }
    
        public int Order_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Salesperson_ID { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public Nullable<int> Boat_ID { get; set; }
        public string DeliveryAddress { get; set; }
        public string City { get; set; }
    
        public virtual Boat Boat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contract { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual Sales_Person Sales_Person { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
