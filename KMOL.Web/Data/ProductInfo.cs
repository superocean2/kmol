using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KMOL.Web.Data
{
    [Table("Products")]
    public class ProductInfo
    {
        [Key]
        [JsonProperty("id")]
        public int ProductId { get; set; }
        [JsonProperty("n")]
        public string Name { get; set; }
        [JsonProperty("u")]
        public string Url { get; set; }
        [JsonProperty("i")]
        public string ImageUrl { get; set; }
        [JsonProperty("p")]
        public decimal Price { get; set; }
        [JsonProperty("s")]
        public decimal PercentSale { get; set; }
        [JsonProperty("o")]
        public decimal OldPrice { get; set; }
        [JsonIgnore]
        public int WebsiteId { get; set; }
    }
}
