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
    
    public partial class log
    {
        public long id { get; set; }
        public string urunadi { get; set; }
        public string urunkodu { get; set; }
        public string urundegeri { get; set; }
        public string depo { get; set; }
        public string raf { get; set; }
        public Nullable<long> girdicikti { get; set; }
        public string islemyapan { get; set; }
        public Nullable<long> miktar { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public Nullable<long> urunkilifi { get; set; }
    }
}