using KMOL.Data.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data
{
    public class EnContext:DbContext
    {

        public DbSet<ProductInfo> Products { get; set; }

        public DbSet<WebsiteInfo> Websites { get; set; }
    }
}
