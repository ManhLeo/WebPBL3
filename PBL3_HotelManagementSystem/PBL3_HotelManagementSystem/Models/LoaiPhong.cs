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
    
    public partial class LoaiPhong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiPhong()
        {
            this.Phongs = new HashSet<Phong>();
        }
    
        public string IDLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; }
        public byte[] HinhAnh { get; set; }
        public Nullable<double> DonGia { get; set; }
        public Nullable<int> SoNguoi { get; set; }
        public Nullable<int> SoGiuong { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phong> Phongs { get; set; }
    }
}