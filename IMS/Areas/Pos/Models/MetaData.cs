using IMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Areas.Pos.Models
{
    public class Stock
    {
        [Key]
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public long ProductId { get; set; }
        public long PosPurchaseMasterId { get; set; }
        public int UserId { get; set; }
        public double StockIn { get; set; }
        public double StockOut { get; set; }
        public DateTime Date { get; set; }
        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
    public class Category
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [MaxLength(455)]
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
    public class SubCategory
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public long CategoryId { get; set; }
        [Required(ErrorMessage = "Required")]
        [MaxLength(455)]
        public string Name { get; set; }
        public virtual Category Category { get; set; }
    }
}
