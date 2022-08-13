using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class CustomerVM
    {
        public Customer Customer { get; set; }
        public Gurantor Gurantor { get; set; }
        public List<Gurantor> GurantorList { get; set; }
    }
    public class HomeVM 
    {
        public int Customers { get; set; }
    }
    public class PurchaseVM 
    {
        public PurchaseMaster PurchaseMaster { get; set; }
        public Supplier Supplier { get; set; }
        public ModelNo ModelNo { get; set; }
        public Cargo Cargo { get; set; }
        public IEnumerable<SelectListItem> SupplierList { get; set; }
        public IEnumerable<SelectListItem> CargoList { get; set; }
        public IEnumerable<SelectListItem> ModelList { get; set; }
    }
    public class purchaseSavemodel
    {
        public PurchaseMaster PurchaseMaster { get; set; }
        public List<TranscationDetails> TranscationDetailList { get; set; }
    }
    public class VoucherSavemodel
    {
        public VoucherMaster VoucherMaster { get; set; }
        public List<TranscationDetails> TranscationDetailList { get; set; }
        public long dynamicid { get; set; }
    }
   
    public class VoucherVM 
    {
        public VoucherMaster VoucherMaster { get; set; }
        public IEnumerable<SelectListItem> AccountList { get; set; }
        public IEnumerable<SelectListItem> BankList { get; set; }
        public string InWords { get; set; }

    }
    public class LedgerVM
    {
        public IEnumerable<SelectListItem> AccountList { get; set; }
        [Required(ErrorMessage ="Required")]
        public long LedgerAccountNo { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime StDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime EnDate { get; set; }
        public double OpBalance { get; set; }
        public List<LedgerDTO> Transcations { get; set; }
        public double T_Cr => Transcations == null ? 0 : Transcations.Select(c => c.Cr).Sum();
        public double T_Dr => Transcations == null ? 0 : Transcations.Select(c => c.Dr).Sum();
        public double Balance => Transcations == null ? 0 : Transcations.Select(c => c.HeadId).FirstOrDefault() == 1 || Transcations.Select(c => c.HeadId).FirstOrDefault() == 5 ? OpBalance + (T_Dr -T_Cr) : OpBalance + (T_Cr - T_Dr);
    }
    public class LedgerDTO 
    {
        public int ThirdLevelId { get; set; }
        public DateTime TransDate { get; set; }
        public string Vtype { get; set; }
        public string TransDesc { get; set; }
        public double Dr { get; set; }
        public double Cr { get; set; }
        public long HeadId { get; set; }
        //public double Balance => HeadId == 1 || HeadId == 5 ? Dr - Cr : Cr - Dr;
        public virtual ThirdLevel ThirdLevel { get; set; }
    }
    public class TrailVM
    {
        [Required(ErrorMessage = "Required")]
        public DateTime StDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime EnDate { get; set; }
        public List<TrailDTO> Balances { get; set; }
        
        public double NADr => Balances ==null? 0: Balances.Select(c => c.ADr).Sum();
        public double NACr => Balances == null ? 0 : Balances.Select(c => c.ACr).Sum();
        public double NTCr => Balances == null ? 0 : Balances.Select(c => c.TCr).Sum();
        public double NTDr => Balances == null ? 0 : Balances.Select(c => c.TDr).Sum();
        public double GDr => NADr + NTDr;
        public double GCr => NACr + NTCr;
    }
    public class TrailDTO
    {
        public long ThirdlevelId { get; set; }
        public string AccountName { get; set; }
        public double ADr { get; set; }
        public double ACr { get; set; }
        public double TCr { get; set; }
        public double TDr { get; set; }
        public long HeadId { get; set; }
        public double TotalDr => ADr + TDr;
        public double TotalCr => ACr + TCr;
        public double Balance => HeadId == 1 || HeadId == 5 ? TotalDr - TotalCr : TotalCr - TotalDr;
    }
    public class BalanceVM
    {
        [Required(ErrorMessage = "Required")]
        public DateTime StDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime EnDate { get; set; }
        public List<BalanceDTO> Assets { get; set; }
        public List<BalanceDTO> Liability { get; set; }
        public List<BalanceDTO> Capital { get; set; }
    }
    public class BalanceDTO
    {
        public int ThirdLevelId { get; set; }
        public string AccountName { get; set; }
        public double TDr { get; set; }
        public double TCr { get; set; }
        public double Balance => HeadId == 1 || HeadId == 5 ? TDr - TCr : TCr- TDr;
        public long HeadId { get; set; }
    }
}
