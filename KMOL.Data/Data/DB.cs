using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseContext
{
    /// <summary>
    /// Klasse for forbindelse til databasen
    /// </summary>
    /// <remarks></remarks>
    public class DB
    {
        public DB(string database)
        {
            Database = database;
        }
        public string Database { get; set; }

        // Get or set the connection string to the SQLite database
        public string ConnectionString
        {
            get
            {
                return "Data Source=" + Database + ";Version=3;";
                //"PRAGMA journal_mode=WAL;Pooling=false;";
            }
        }

        public bool Create()
        {
            if (System.IO.File.Exists(Database)) return false;
            System.Data.SQLite.SQLiteConnection.CreateFile(Database);
            return true;
        }

        private bool IsTableExists(string tablename)
        {
            Int32 count = 0;
            string SQL = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = '" + tablename + "'";
            try
            {
                using (System.Data.SQLite.SQLiteConnection connection = new System.Data.SQLite.SQLiteConnection(ConnectionString, true))
                {
                    connection.Open();
                    using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(SQL, connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if ((reader != null) && (reader.Read())) count = reader.GetInt32(0);
                        }
                    }
                    connection.Close();
                }
                return count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void ExecuteCommandNonQuery(string sql)
        {
            try
            {
                using (System.Data.SQLite.SQLiteConnection connection = new System.Data.SQLite.SQLiteConnection(ConnectionString, true))
                {
                    connection.Open();
                    using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public void CreateTable(Type table)
        {
            var tableName = table.Name;
            var customTableName = Attribute.GetCustomAttribute(table, typeof(TableAttribute));
            if (customTableName != null) tableName = ((TableAttribute)customTableName).Name;
            StringBuilder sb = new StringBuilder();
            sb.Append($"CREATE TABLE {tableName}( ");
            bool isKeySet = false;
            foreach (var pro in table.GetProperties())
            {
                var name = pro.Name;
                bool isKey = false;
                if (name.Trim().ToLower().EndsWith("id") && isKeySet == false) { isKey = true; isKeySet = true; }
                if (isKey) sb.Append($"{name} {GetDataType(pro.PropertyType)} PRIMARY KEY AUTOINCREMENT,");
                else sb.Append($"{name} {GetDataType(pro.PropertyType)} ,");
            }
            string s = sb.ToString().TrimEnd(',');
            s = s + ");";
            ExecuteCommandNonQuery(s);
        }

        private string GetDataType(Type type)
        {
            switch (type.Name)
            {
                case "Int32":
                    return "INTEGER";
                case "String":
                    return "TEXT";
                default:
                    return "TEXT";
            }
        }

    }
}