using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Data
{
    class Program
    {
        static void Main(string[] args)
        {
            string response = Utility.GetData("http://www.lazada.vn/link-cac-san-pham/?page=3");
        }
    }
}
