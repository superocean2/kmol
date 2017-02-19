using KMOL.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KMOL.Data.Sites
{
    public class Adayroi : ISite
    {
        public string SiteName => "Adayroi";
        public string SiteUrl => "https://www.adayroi.com";
        const string regexMain = "class=\"col-lg-3.*?href=\"(?<url>.*?)\"\\stitle=\"(?<title>.*?)\">.*?data-src=\"(?<imageurl>.*?)\".*?class=\"amount-1\">(?<price>[\\d,.]+).*?(?>class=\"amount-2\">(?<oldprice>[\\d,.]+).*?class=\"sale-off\">-(?<discount>\\d+)|</div>)";
        const string regexPageCount = "data-paging=[\"']last[\"'].*?href=[\"'].*?\\?p=(?<pagecount>\\d+)";
        string[] urls = new string[]
        {
            "/thuc-pham-r591?p={i}",
            "/dien-may-cong-nghe-r321?p={i}",
            "/thoi-trang-nu-m2?p={i}",
            "/thoi-trang-nam-m81?p={i}",
            "/thoi-trang-tre-em-m120?p={i}",
            "/cham-soc-mat-m173?p={i}",
            "/trang-diem-m140?p={i}",
            "/nuoc-hoa-m169?p={i}",
            "/cham-soc-toc-m197?p={i}",
            "/cham-soc-toan-than-m211?p={i}",
            "/my-pham-danh-cho-nam-m225?p={i}",
            "/cham-soc-ca-nhan-suc-khoe-m242?p={i}",
            "/tinh-dau-spa-m270?p={i}",
            "/vitamin-thuc-pham-chuc-nang-m287?p={i}",
            "/dung-cu-the-thao-m2067?p={i}",
            "/me-be-r714?p={i}",
            "/nha-cua-doi-song-r861?p={i}",
            "/sach-van-phong-pham-r1383?p={i}",
            "/o-to-xe-may-m1077?p={i}"
        };
        public List<LinkInfo> LinkInfos
        {
            get
            {
                var list = new List<LinkInfo>();
                for (int i = 0; i < urls.Length; i++)
                {
                    list.Add(new LinkInfo(SiteUrl + urls[i], regexMain,48,regexPageCount,true));
                }
                return list;
            }
        }
    }
}
