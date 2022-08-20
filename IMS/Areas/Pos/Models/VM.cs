using IMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Areas.Pos.Models
{
    public class PurchaseVM
    {
        public PosPurchaseMaster PosPurchaseMaster { get; set; }
        public Supplier Supplier { get; set; }
        public IEnumerable<SelectListItem> SupplierList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> SubCategoryList { get; set; }
    }
    public class PosPurchaseSavemodel
    {
        public PosPurchaseMaster PosPurchaseMaster { get; set; }
        public List<TranscationDetails> TranscationDetailsList { get; set; }
        public List<Stock> StockList { get; set; }
        public long dynamicId { get; set; }
    }
}
