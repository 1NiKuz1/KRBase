//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KR2.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class KR2
    {
        public int id_KR2 { get; set; }
        public Nullable<int> id_doc { get; set; }
        public string number_KR2 { get; set; }
        public Nullable<System.DateTime> date_KR2 { get; set; }
    
        public virtual Doc Doc { get; set; }
    }
}
