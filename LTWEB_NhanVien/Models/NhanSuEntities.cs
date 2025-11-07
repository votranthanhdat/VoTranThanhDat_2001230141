using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LTWEB_NhanVien.Models
{
    public partial class NhanSuEntities : DbContext
    {
        public NhanSuEntities()
            : base("name=Model1")
        {
        }

        public virtual DbSet<tbl_Deparment> tbl_Deparment { get; set; }
        public virtual DbSet<tbl_Employee> tbl_Employee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
