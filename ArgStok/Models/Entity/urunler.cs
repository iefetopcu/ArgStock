//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArgStok.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class urunler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public urunler()
        {
            this.stoks = new HashSet<stok>();
        }
    
        public long urunid { get; set; }
        public string urunkodu { get; set; }
        public string urunadi { get; set; }
        public string urundegeri { get; set; }
        public Nullable<long> urunkilifi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<stok> stoks { get; set; }
    }
}
