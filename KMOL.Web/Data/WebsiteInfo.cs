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
    [Table("Websites")]
    public class WebsiteInfo
    {
        [Key]
        [JsonProperty("id")]
        public int WebsiteId { get; set; }
        [JsonProperty("n")]
        public string Name { get; set; }
        [JsonProperty("u")]
        public string Url { get; set; }
    }
}
