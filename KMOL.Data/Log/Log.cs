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
        private static object _lockDebug = new object();
        private static object _lockError = new object();
        public static string ReadAllDebug()
        {
            string path = Environment.CurrentDirectory + $"\\Log\\log_debug_{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
            if (System.IO.File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return "NO DEBUG FILE!";
        }
        public static string ReadAllError()
        {
            string path = Environment.CurrentDirectory + $"\\Log\\log_error_{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return "NO ERROR FILE";
        }
        public static void Debug(string message, bool indent)
        {
            string path = Environment.CurrentDirectory + $"\\Log\\log_debug_{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
            lock (_lockDebug)
            {
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine((indent?"      ":"") + DateTime.Now.ToString() + "--" + message);
                writer.WriteLine((indent?"      ":"")+"--------------------");
                writer.WriteLine((indent ? "      " : "") + "--------------------");
                writer.Close();

            }

        }

        public static void Err(string message, Exception ex)
        {
            string path = Environment.CurrentDirectory + $"\\Log\\log_error_{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
            lock (_lockError)
            {
                StreamWriter writer = new StreamWriter(path, true);
                writer.Write(DateTime.Now.ToString() + "--------" + message);
                writer.WriteLine(ex.Message);
                writer.WriteLine(ex.StackTrace);
                var subEx = ex.InnerException;
                while (subEx != null)
                {
                    writer.WriteLine(subEx.Message);
                    if (subEx.StackTrace != null) writer.WriteLine(subEx.StackTrace);
                    subEx = subEx.InnerException;
                }
                writer.WriteLine("--------------------");
                writer.WriteLine("--------------------");
                writer.Close();
            }

        }
    }
}
