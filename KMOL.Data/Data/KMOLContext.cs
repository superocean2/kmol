using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KMOL.Data.Data
{
    public class KMOLContext : DbContext
    {
        DB db = null;
        public KMOLContext(bool isHomeLinks) : base(new SQLiteConnection() { ConnectionString = GetLastestDB(isHomeLinks,DateTime.Now).ConnectionString }, true)
        {
            db = GetLastestDB(isHomeLinks,DateTime.Now);
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<KMOLContext>(null);
            if (!System.IO.File.Exists(GetDatabaseString(DateTime.Now, isHomeLinks)))
            {
                db.Create();
                db.CreateTable(typeof(WebsiteInfo));
                db.CreateTable(typeof(ProductInfo));
            }
        }
        public KMOLContext(bool isHomeLinks,DateTime usedDate) : base(new SQLiteConnection() { ConnectionString = GetLastestDB(isHomeLinks, usedDate).ConnectionString }, true)
        {
            db = GetLastestDB(isHomeLinks, usedDate);
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<KMOLContext>(null);
            if (!System.IO.File.Exists(GetDatabaseString(usedDate, isHomeLinks)))
            {
                db.Create();
                db.CreateTable(typeof(WebsiteInfo));
                db.CreateTable(typeof(ProductInfo));
            }
        }
        private static string GetDatabaseString(DateTime currentDate, bool isHomeLinks)
        {
            string s = new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName + "\\SiteDatas" + "\\data_" + (isHomeLinks ? "home_" : string.Empty) + currentDate.ToString("dd-MM-yyyy") + ".db";
            return s;
        }
        private static DB GetLastestDB(bool isHomeLinks, DateTime usedDate)
        {
            return new DB(GetDatabaseString(usedDate, isHomeLinks));
        }

        public void ExecuteCommandNonQuery(string command)
        {
            db.ExecuteCommandNonQuery(command);
        }
        public string DatabaseAllPath => GetDatabaseString(DateTime.Now,false);
        public string DatabaseHomePath => GetDatabaseString(DateTime.Now,true);
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<ProductInfo> Products { get; set; }

        public DbSet<WebsiteInfo> Websites { get; set; }
    }
}
