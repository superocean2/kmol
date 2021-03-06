﻿using KMOL.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KMOL.Data.Sites
{
    public class Tiki : ISite
    {
        public string SiteName => "Tiki";
        public string SiteUrl => "https://www.tiki.vn";

        const string regexMain ="class=\"product-item.*?href=\"(?<url>.*?)\"\\s+title=\"(?<title>.*?)\".*?src=\"(?<imageurl>[^=]*?\\.(jpg|gif|png)).*?class=\"price-sale\">(?<price>[\\d.,]+).*?(class=\"sale-tag.*?>-(?<discount>\\d+).*?class=\"price-regular\">(?<oldprice>[\\d.,]+)|</p>)";
        const string regexPageCount = "class=['\"]filter-list-box['\"].+?\\((?<pagecount>\\d+)";
        string[] urls = new string[] 
        {
            "/sach-truyen-tieng-viet/c316?src=tree&order=top_seller&page={i}",
            "/sach-tieng-anh/c320?src=tree&order=top_seller&page={i}",
            "/van-phong-pham/c7741?src=tree&order=top_seller&page={i}",
            "/qua-luu-niem/c4545?src=tree&order=top_seller&page={i}",
            "/dien-gia-dung/c1882?src=tree&order=top_seller&page={i}",
            "/thiet-bi-kts-phu-kien-so/c1815?src=tree&order=top_seller&page={i}",
            "/may-anh/c1801?src=tree&order=top_seller&page={i}",
            "/dien-thoai-may-tinh-bang/c1789?src=tree&order=top_seller&page={i}",
            "/laptop-thiet-bi-it/c1846?src=tree&order=top_seller&page={i}",
            "/tivi-thiet-bi-nghe-nhin/c4221?src=tree&order=top_seller&page={i}",
            "/lam-dep-suc-khoe/c1520?src=tree&order=top_seller&page={i}",
            "/thoi-trang/c914?src=tree&order=top_seller&page={i}",
            "/nha-cua-doi-song/c1883?src=tree&order=top_seller&page={i}",
            "/bach-hoa-online/c4384?src=tree&order=top_seller&page={i}",
            "/do-choi-qua-tang/c1929?src=tree&order=top_seller&page={i}",
            "/me-va-be/c2549?src=tree&order=top_seller&page={i}",
            "/the-thao/c1975?src=tree&order=top_seller&page={i}",
            "/thiet-bi-van-phong-pham/c1857?src=tree&order=top_seller&page={i}"
        };
        public List<LinkInfo> LinkInfos
        {
            get
            {
                var list = new List<LinkInfo>();
                for (int i = 0; i < urls.Length; i++)
                {
                    list.Add(new LinkInfo(SiteUrl + urls[i], regexMain, 24,regexPageCount,false));
                }
                return list;
            }
        }
        
    }
}
