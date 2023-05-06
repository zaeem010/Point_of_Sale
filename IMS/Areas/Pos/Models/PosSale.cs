using IMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Areas.Pos.Models
{
    public class PosSaleMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime InvDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public int ThirdLevelId { get; set; }
        [MaxLength(455)]
        public string Remarks { get; set; }
        public double NetTotal { get; set; }
        public double Discount { get; set; }
        public double GrandTotal { get; set; }
        public double ReceivedTotal { get; set; }
        public double ReturnPrice { get; set; }
        [MaxLength(55)]
        public string VType { get; set; } = "POSSINV";
        [MaxLength(55)]
        public string Status { get; set; }
        public virtual ThirdLevel ThirdLevel { get; set; }
        public List<PosSaleDetail> PosSaleDetails { get; set; }
        public PosSaleMaster()
        {
            PosSaleDetails = new List<PosSaleDetail>();
        }
    }
    public class PosSaleDetail
    {
        [Key]
        public long Id { get; set; }
        public long PosSaleMasterId { get; set; }
        public long CategoryId { get; set; }
        public long ProductId { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
        [MaxLength(55)]
        public string Unit { get; set; }
        public double Qty { get; set; }
        public double SalePrice { get; set; }
        public double Total { get; set; }
        //public int Status { get; set; }
    }
}