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
        static string database = Environment.CurrentDirectory + "\\data_"+ DateTime.Now.ToString("dd-MM-yyyy") + ".db";
        public KMOLContext(): base(new SQLiteConnection() { ConnectionString = new DB(database).ConnectionString }, true)
        {
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<KMOLContext>(null);
            if (!System.IO.File.Exists(database))
            {
                DB db = new DB(database);
                db.Create();
                db.CreateTable(typeof(WebsiteInfo));
                db.CreateTable(typeof(ProductInfo));
            }
        }
        public void ExecuteCommandNonQuery(string command)
        {
            DB db = new DB(database);
            db.ExecuteCommandNonQuery(command);
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
