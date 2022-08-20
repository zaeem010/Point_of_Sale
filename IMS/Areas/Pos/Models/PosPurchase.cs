using IMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Areas.Pos.Models
{
    public class PosPurchaseMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [Required(ErrorMessage ="Required")]
        public DateTime InvDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public int ThirdLevelId { get; set; }
        [MaxLength(455)]
        public string Remarks { get; set; }
        public double NetTotal { get; set; }
        public double PaidTotal { get; set; }
        [MaxLength(55)]
        public string VType { get; set; } = "POSPINV";
        public virtual ThirdLevel ThirdLevel { get; set; }
        public List<PosPurchaseDetail> PosPurchaseDetailList { get; set; }
        public PosPurchaseMaster()
        {
            PosPurchaseDetailList = new List<PosPurchaseDetail>();
        }
        [NotMapped]
        public double BalanceTotal => NetTotal - PaidTotal;
    }
    public class PosPurchaseDetail
    {
        [Key]
        public long Id { get; set; }
        public long PosPurchaseMasterId { get; set; }
        public long CategoryId { get; set; }
        public long ProductId { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
        [MaxLength(55)]
        public string Unit { get; set; }
        public double Qty { get; set; }
        public double PurchasePrice { get; set; }
        public double Total { get; set; }
    }
}
