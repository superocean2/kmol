using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Data.Sites
{
    public class Lazada : ISite
    {
        public string SiteName => "Lazada";
        public string SiteUrl => "http://www.lazada.vn";
        public List<LinkInfo> LinkInfos => new List<LinkInfo>()
        {
            new LinkInfo()
            {
                Url=url,
                Regex =regex,
                PageCount=()=> {return 100;}
            }
        };

        string url = "http://www.lazada.vn/link-cac-san-pham/?itemperpage=120&page={i}";
        string regex = "class=[\"']product-card\\s+.+?<a\\s+href=[\"'](?<url>.+?)[\"'].+?<img.+?data-original=[\"'](?<imageurl>.+?)[\"'].+?class=[\"']product-card__name-wrap.+?title=[\"'](?<title>.+?)[\"'].+?class=[\"']product-card__price['\"]>(?<price>[\\d\\.,]+).+?(class=[\"']product-card__sale[\"']>(?<discount>.+?)<.+?class=[\"']product-card__old-price[\"']>(?<oldprice>[\\d\\.,]+)|class=[\"']product-card__rating[\"'])";
    }
}
