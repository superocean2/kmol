using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Data.Sites
{
    public interface ISite
    {
        List<LinkInfo> LinkInfos { get;}
        string SiteName { get;}
        string SiteUrl { get;}
    }
}
