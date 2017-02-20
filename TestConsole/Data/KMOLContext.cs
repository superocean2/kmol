using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Data.Data
{
    public class KMOLContext:DbContext
    {
        public KMOLContext(): base(new SQLiteConnection() { ConnectionString = "Data Source=" + Environment.CurrentDirectory +"\\kmol.db" + ";Version=3;" }, true)
        {
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<KMOLContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<ProductInfo> Products { get; set; }

        public DbSet<WebsiteInfo> Websites { get; set; }
    }
}
