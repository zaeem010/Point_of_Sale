using IMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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
    public class PosSaleSavemodel
    {
        public PosSaleMaster PosSaleMaster { get; set; }
        public List<TranscationDetails> TranscationDetailsList { get; set; }
        public List<Stock> StockList { get; set; }
        public long dynamicId { get; set; }
    }
    public class SaleScreenVM
    {
        public List<Category> Categorys { get; set; }
        public List<SubCategory> SubCategorys { get; set; }
        public List<Product> Products { get; set; }
        public List<PosSaleMaster> PosSaleMasters { get; set; }
    }
}
