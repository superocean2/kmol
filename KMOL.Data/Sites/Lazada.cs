using KMOL.Data.Data;
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

        const string regexMain = "class=\"c-product-card\\s+.+?<a\\s+href=\"(?<url>.+?)\".+?\"src\":\\s+\"(?<imageurl>.*?)\".*?class=\"c-product-card__description\".*?<a.*?>\\s*(?<title>.*?)\\s*<.*?class=\"c-product-card__price-final\">\\s*(?<price>[\\d\\.,]+).*?(class=\"c-product-card__discount\">(?<discount>-\\d+).*?class=\"c-product-card__old-price\">(?<oldprice>[\\d\\.,]+)|class=\"c-quick-buy)";
        const string regexPageCount = "class=\"c-catalog-title__quantity\".*?>.*?(?<pagecount>\\d+).*?<";

        string[] urls = new string[]
        {
            "/dien-thoai-may-tinh-bang/?itemperpage=120&page={i}",
            "/may-vi-tinh-laptop/?itemperpage=120&page={i}",
            "/may-anh-may-quay-phim/?itemperpage=120&page={i}",
            "/tv-video-am-thanh-thiet-bi-deo-cong-nghe/?itemperpage=120&page={i}",
            "/thoi-trang-nu/?itemperpage=120&page={i}",
            "/dong-ho-mat-kinh-trang-suc/?itemperpage=120&page={i}",
            "/thoi-trang-nam/?itemperpage=120&page={i}",
            "/do-gia-dung/?itemperpage=120&page={i}",
            "/do-dung-bep-phong-an/?itemperpage=120&page={i}",
            "/san-pham-noi-that/?itemperpage=120&page={i}",
            "/dung-cu-thiet-bi-gia-dinh/?itemperpage=120&page={i}",
            "/tu-dung-va-sap-xep-do/?itemperpage=120&page={i}",
            "/van-phong-pham-gia-dinh/?itemperpage=120&page={i}",
            "/tan-trang-nha-cua/?itemperpage=120&page={i}",
            "/cham-soc-suc-khoe-va-lam-dep/?itemperpage=120&page={i}",
            "/tre-so-sinh-tre-nho/?itemperpage=120&page={i}",
            "/do-choi-tro-choi/?itemperpage=120&page={i}",
            "/the-thao-da-ngoai/?itemperpage=120&page={i}",
            "/vali-ba-lo-tui-du-lich/?itemperpage=120&page={i}",
            "/thiet-bi-phu-kien-o-to-xe-may/?itemperpage=120&page={i}",
            "/bach-hoa-online/?itemperpage=120&page={i}",
            "/sach/?itemperpage=120&page={i}",
            "/nhac-cu-moi/?itemperpage=120&page={i}"
        };
        public List<LinkInfo> LinkInfos
        {
            get
            {
                var list = new List<LinkInfo>();
                for (int i = 0; i < urls.Length; i++)
                {
                    list.Add(new LinkInfo(SiteUrl + urls[i], regexMain, 120,regexPageCount,false));
                }
                return list;
            }
        }
    }
}
