﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KMOL.Data.Data
{
    public class Products
    {
        [JsonProperty("w")]
        public WebsiteInfo Website { get; set; }
        [JsonProperty("l")]
        public List<ProductInfo> ListProducts { get; set; }
    }
}