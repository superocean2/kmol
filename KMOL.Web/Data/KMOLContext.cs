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
using System.Web;

namespace KMOL.Web.Data
{
    public class KMOLContext : DbContext
    {
        DB db = null;
        public KMOLContext(bool isHomeLinks, DateTime usedDate) : base(new SQLiteConnection() { ConnectionString = GetLastestDB(isHomeLinks, usedDate).ConnectionString }, true)
        {
            db = GetLastestDB(isHomeLinks, usedDate);
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<KMOLContext>(null);
        }
        public KMOLContext(bool isHomeLinks, DateTime usedDate,ref DateTime realUsedDate) : base(new SQLiteConnection() { ConnectionString = GetLastestDB(isHomeLinks,usedDate).ConnectionString }, true)
        {
            db = GetLastestDB(isHomeLinks,usedDate,ref realUsedDate);
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<KMOLContext>(null);
        }
        private static string GetDatabaseString(DateTime currentDate, bool isHomeLinks)
        {
            string s = new DirectoryInfo(HttpRuntime.AppDomainAppPath).Parent.FullName + "\\KMOL.Data\\SiteDatas" + "\\data_" + (isHomeLinks ? "home_" : string.Empty) + currentDate.ToString("dd-MM-yyyy") + ".db";
            return s;
        }

        private static DB GetLastestDB(bool isHomeLinks, DateTime usedDate,ref DateTime realUsedDate)
        {
            realUsedDate = GetLastestDatabaseDate(usedDate, isHomeLinks);
            return new DB(GetDatabaseString(realUsedDate, isHomeLinks));
        }
        private static DB GetLastestDB(bool isHomeLinks, DateTime usedDate)
        {
            DateTime currentDate = GetLastestDatabaseDate(usedDate, isHomeLinks);
            return new DB(GetDatabaseString(currentDate, isHomeLinks));
        }
        private static DateTime GetLastestDatabaseDate(DateTime currentDate, bool isHomeLinks)
        {
            DateTime olderDate = currentDate;

            if (!System.IO.File.Exists(GetDatabaseString(currentDate, isHomeLinks)))
            {
                olderDate = currentDate.AddDays(-1);
                olderDate = GetLastestDatabaseDate(olderDate, isHomeLinks);
            }
            return olderDate;
        }
        public void ExecuteCommandNonQuery(string command)
        {
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
