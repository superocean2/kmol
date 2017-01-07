using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Data.Data
{
    [Table("Products")]
    public class ProductInfo
    {
        [Key]
        public long ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal PercentSale { get; set; }
        public decimal OldPrice { get; set; }
        public int WebsiteId { get; set; }

    }
}
