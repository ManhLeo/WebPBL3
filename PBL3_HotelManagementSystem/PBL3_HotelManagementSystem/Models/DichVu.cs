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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    public partial class DichVu
    {
        public DichVu()
        {
            this.DatDichVus = new HashSet<DatDichVu>();
        }

        [Key]
        public string IDDV { get; set; }
        public string TenDV { get; set; }

        [ForeignKey("LoaiDV")]
        public string IDLoaiDV { get; set; }
        public virtual LoaiDV LoaiDV { get; set; }

        public virtual ICollection<DatDichVu> DatDichVus { get; set; }
    }
}
