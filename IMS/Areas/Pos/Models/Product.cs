using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Areas.Pos.Models
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        [Required (ErrorMessage ="Required")]
        [MaxLength(455)]
        public string Name { get; set; }
        [MaxLength(455)]
        public string Sku { get; set; }
        public double SalePrice { get; set; }
        public double PurchasePrice { get; set; }
        public double OpeningStock { get; set; }
        [MaxLength(45)]
        public string Unit { get; set; }
        [MaxLength(455)]
        public string ImagePath { get; set; }
        //
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
    
}
