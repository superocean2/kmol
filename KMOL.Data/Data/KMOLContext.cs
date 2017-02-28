﻿using DatabaseContext;
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
        static string database = new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName + "\\SiteDatas" + "\\data_" + DateTime.Now.ToString("dd-MM-yyyy") + ".db";
        static string databaseHomeLinks = new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName + "\\SiteDatas" + "\\data_home_" + DateTime.Now.ToString("dd-MM-yyyy") + ".db";
        DB db = null;
        public KMOLContext(bool isHomeLinks) : base(new SQLiteConnection() { ConnectionString = new DB(isHomeLinks ? databaseHomeLinks : database).ConnectionString }, true)
        {
            db = new DB(isHomeLinks ? databaseHomeLinks : database);
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<KMOLContext>(null);
            if (!System.IO.File.Exists(isHomeLinks ? databaseHomeLinks : database))
            {
                db.Create();
                db.CreateTable(typeof(WebsiteInfo));
                db.CreateTable(typeof(ProductInfo));
            }
        }
        public void ExecuteCommandNonQuery(string command)
        {
            db.ExecuteCommandNonQuery(command);
        }
        public string DatabaseAllPath => database;
        public string DatabaseHomePath => databaseHomeLinks;
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<ProductInfo> Products { get; set; }

        public DbSet<WebsiteInfo> Websites { get; set; }
    }
}
