using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAB4_new.Models
{
    /// <summary>
    /// Author - Tue Jensen
    /// </summary>
    public partial class VillageSmartGridContext : DbContext
    {
        public VillageSmartGridContext() : base("Name=VillageSmartGridContext")
        {
            // This seems giga mega emtpy. hm.
        }

        public virtual DbSet<VillageSmartGrid> VillageSmartGrid { get; set; }

        /// <summary>
        /// Do this when Model is Created
        /// </summary>
        /// <param name="mb"></param>
        protected override void OnModelCreating(DbModelBuilder mb)
        {

            mb.Entity<VillageSmartGrid>().Property(a => a.Type).IsUnicode(false);

            mb.Entity<VillageSmartGrid>().Property(a => a.SmartMeter).IsUnicode(false);

            mb.Entity<VillageSmartGrid>().Property(a => a.ProductType).IsUnicode(false);
        }
    }
}