using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Data.Sites
{
    public class LinkInfo
    {
        public string Url { get; set; }
        public string Regex { get; set; }
        public Func<int> PageCount;

    }
}
