using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class Log
    {
        public static void Debug(string message)
        {
            string path = Environment.CurrentDirectory + "Log\\log_debug.txt";
            StreamWriter writer = new StreamWriter(File.OpenWrite(path));
            writer.Write(DateTime.Now.ToString() + "--------" + message);
            writer.WriteLine("------------------------------------------------------------------------------------");
            writer.Close();
        }

        public static void Err(string message,Exception ex)
        {
            string path = Environment.CurrentDirectory + "Log\\log_error.txt";
            StreamWriter writer = new StreamWriter(File.OpenWrite(path));
            writer.Write(DateTime.Now.ToString()+ "--------" + message);
            writer.WriteLine(ex.Message);
            writer.WriteLine(ex.StackTrace);
            var subEx = ex.InnerException;
            while (subEx!=null)
            {
                writer.WriteLine(subEx.Message);
                if (subEx.StackTrace != null) writer.WriteLine(subEx.StackTrace);
                subEx = subEx.InnerException;
            }
            writer.WriteLine("------------------------------------------------------------------------------------");
            writer.Close();
        }
    }
}
