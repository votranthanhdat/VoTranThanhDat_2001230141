namespace LTWEB_NhanVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        public int? DeptId { get; set; }

        public virtual tbl_Deparment tbl_Deparment { get; set; }
    }
}
