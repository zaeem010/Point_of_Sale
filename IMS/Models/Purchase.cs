using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class PurchaseMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [Required(ErrorMessage ="Invoice Date Is Required")]
        public DateTime InvDate { get; set; }
        [Required(ErrorMessage = "Supplier Is Required")]
        public int ThirdLevelId { get; set; }
        public int? CargoId { get; set; }
        [MaxLength(455)]
        public string Remarks { get; set; }
        public double SubTotal { get; set; }
        public double CargoTotal { get; set; }
        public double NetTotal { get; set; }
        public double PaidTotal { get; set; }
        public double BalanceTotal { get; set; }
        public virtual ThirdLevel ThirdLevel { get; set; }
        public virtual Cargo Cargo { get; set; }
        public List<PurchaseDetail> PurchaseDetailList { get; set; }
        public PurchaseMaster()
        {
            PurchaseDetailList = new List<PurchaseDetail>();
        }
    }
    public class PurchaseDetail 
    {
        [Key]
        public long Id { get; set; }
        public long PurchaseMasterId { get; set; }
        [MaxLength(255)]
        public string VName { get; set; }
        [MaxLength(155)]
        public string ModelNo { get; set; }
        [MaxLength(455)]
        public string ChassisNo { get; set; }
        [MaxLength(455)]
        public string EngineNo { get; set; }
        [MaxLength(255)]
        public string KeyNo { get; set; }
        [MaxLength(255)]
        public string BikeNo { get; set; }
        [MaxLength(255)]
        public string Color { get; set; }

        public double Rate { get; set; }
        public double CargoRate { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }

        public virtual PurchaseMaster PurchaseMaster { get; set; }
    }
}
