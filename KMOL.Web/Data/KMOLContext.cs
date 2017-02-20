using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Web.Data
{
    public class KMOLContext : DbContext
    {
        DB db = null;
        public KMOLContext(bool isHomeLinks) : base(new SQLiteConnection() { ConnectionString = GetLastestDB(isHomeLinks).ConnectionString }, true)
        {
            db = GetLastestDB(isHomeLinks);
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<KMOLContext>(null);
        }
        private static DB GetLastestDB(bool isHomeLinks)
        {
            DateTime currentDate = GetLastestDatabaseDate(DateTime.Now, isHomeLinks);
            string database = Environment.CurrentDirectory + "\\SiteDatas" + "\\data_" + currentDate.ToString("dd-MM-yyyy") + ".db";
            string databaseHomeLinks = Environment.CurrentDirectory + "\\SiteDatas" + "\\data_home_" + currentDate.ToString("dd-MM-yyyy") + ".db";
            return new DB(isHomeLinks ? databaseHomeLinks : database);
        }
        private static DateTime GetLastestDatabaseDate(DateTime currentDate, bool isHomeLinks)
        {
            DateTime olderDate;
            string database = Environment.CurrentDirectory + "\\SiteDatas" + "\\data_" + currentDate.ToString("dd-MM-yyyy") + ".db";
            string databaseHomeLinks = Environment.CurrentDirectory + "\\SiteDatas" + "\\data_home_" + currentDate.ToString("dd-MM-yyyy") + ".db";
            if (!System.IO.File.Exists(isHomeLinks ? databaseHomeLinks : database))
            {
                olderDate = currentDate.AddDays(-1);
                GetLastestDatabaseDate(olderDate, isHomeLinks);
            }
            else olderDate = currentDate;
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
