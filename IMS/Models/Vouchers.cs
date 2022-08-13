using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class VoucherMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public long GeneralId { get; set; }
        public DateTime VoucherDate { get; set; }
        [MaxLength(455)]
        public string Remarks { get; set; }
        public double TAmount { get; set; }
        [MaxLength(100)]
        public string Vtype { get; set; }
        [Required(ErrorMessage = "Required")]
        public int ThirdLevelId { get; set; }
        public virtual ThirdLevel ThirdLevel { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public List<VoucherDetail> VoucherDetail { get; set; }
        public VoucherMaster()
        {
            VoucherDetail = new List<VoucherDetail>();
        }
    }
    public class VoucherDetail 
    {
        [Key]
        public long Id { get; set; }
        public long VoucherMasterId { get; set; }
        public double Amount { get; set; }
        [MaxLength(455)]
        public string Narration { get; set; }
        //Bank Voucher
        public DateTime CleDate { get; set; }
        [MaxLength(155)]
        public string CheckNo { get; set; }
        //Bank Voucher
        public int ThirdLevelId { get; set; }
        public virtual ThirdLevel ThirdLevel { get; set; }
    }
}
