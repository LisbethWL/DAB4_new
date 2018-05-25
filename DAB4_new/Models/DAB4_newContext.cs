using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DAB4_new.Models
{
    public class DAB4_newContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public DAB4_newContext() : base("name=DAB4_newContext")
        {
        }

        public System.Data.Entity.DbSet<DAB4_new.Models.ProsumerInfo> ProsumerInfoes { get; set; }

        public System.Data.Entity.DbSet<DAB4_new.Models.TraderInfo> TraderInfoes { get; set; }
    }
}
