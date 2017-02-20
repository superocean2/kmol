using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Data.Data
{
    public class LinkInfo
    {
        public string Url { get; set; }
        public string Regex { get; set; }
        public string RegexPageCount { get; set; }
        public int Pagesize { get; set; }
        public bool IsHasPageCount { get; set; }
        public LinkInfo(string url,string regex,int pagesize,string regexPageCount,bool isHasPageCount)
        {
            this.Url = url;
            this.Regex = regex;
            this.Pagesize = pagesize;
            this.RegexPageCount = regexPageCount;
            this.IsHasPageCount = isHasPageCount;
        }
    }
}
