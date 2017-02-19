using KMOL.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Data
{
    public interface ISite
    {
        List<LinkInfo> LinkInfos { get;}
        string SiteName { get;}
        string SiteUrl { get;}
    }
}
