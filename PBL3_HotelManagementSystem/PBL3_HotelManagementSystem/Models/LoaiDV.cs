//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PBL3_HotelManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoaiDV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiDV()
        {
            this.DichVus = new HashSet<DichVu>();
        }
    
        public string IDLoaiDV { get; set; }
        public string TenLoaiDV { get; set; }
        public Nullable<double> DonGia { get; set; }
        public Nullable<int> SoNguoi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DichVu> DichVus { get; set; }
    }
}
