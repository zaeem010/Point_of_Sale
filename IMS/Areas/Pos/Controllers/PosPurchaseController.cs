using IMS.Areas.Pos.Models;
using IMS.Controllers;
using IMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Areas.Pos.Controllers
{
    [Area("Pos")]
    public class PosPurchaseController : BaseController
    {
        private readonly ApplicationDbContext _db;
        public PosPurchaseController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("/Pos/Purchase/All-Purchases")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.PosPurchaseMaster
                .Include(c => c.ThirdLevel)
                .ToListAsync();
            return View(Li);
        }
        [Route("/Pos/Purchase/Create-New-Purchases")]
        [HttpGet]
        public IActionResult create(PosPurchaseMaster PosPurchaseMaster)
        {
            IEnumerable<SelectListItem> SupplierList = _db.Supplier.Select(c => new SelectListItem
            {
                Value = c.ThirdLevelId.ToString(),
                Text = c.Name
            });
            IEnumerable<SelectListItem> ProductList = _db.Product.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Name} || {c.Sku}"
            });
            IEnumerable<SelectListItem> CategoryList = _db.Category.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            IEnumerable<SelectListItem> SubCategoryList = _db.SubCategory.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            PosPurchaseMaster.InvDate = DateTime.Now;
            var VM = new PurchaseVM
            {
                SupplierList = SupplierList,
                PosPurchaseMaster = PosPurchaseMaster,
                CategoryList = CategoryList,
                SubCategoryList = SubCategoryList,
                ProductList = ProductList
            };
            return View(VM);
        }
        [Route("/Pos/PosPurchase/GetProductBysubList")]
        [HttpGet]
        public async Task<JsonResult> GetProductBysubList(long Id)
        {
            var Li = await _db.Product.Where(x => x.SubCategoryId.Equals(Id))
                .ToListAsync();
            return Json(Li);
        }
        [Route("/Pos/PosPurchase/GetProductByCatList")]
        [HttpGet]
        public async Task<JsonResult> GetProductByCatList(long Id)
        {
            var Li = await _db.Product.Where(x => x.CategoryId.Equals(Id))
                .ToListAsync();
            return Json(Li);
        }
        [Route("/Pos/PosPurchase/GetProduct")]
        [HttpGet]
        public async Task<JsonResult> GetProduct(long Id)
        {
            var Li = await _db.Product.SingleOrDefaultAsync(x => x.Id.Equals(Id));
            return Json(Li);
        }
        [Route("/Pos/PosPurchase/Save")]
        [HttpPost]
        public async Task<IActionResult> Save(PosPurchaseSavemodel PosPurchaseSavemodel)
        {
            string message;
            try
            {
                if (PosPurchaseSavemodel.PosPurchaseMaster.Id == 0)
                {
                    PosPurchaseSavemodel.PosPurchaseMaster.Id = _db.PosPurchaseMaster.Where(p => p != null).Select(p => p.Id).DefaultIfEmpty().Max() + 1;
                    PosPurchaseSavemodel.dynamicId = PosPurchaseSavemodel.PosPurchaseMaster.Id;
                    //
                    var TranscationDetails = AddPosMasterintransactionid(PosPurchaseSavemodel);
                    var Purchasedetails = AddMasterinpurchasedetail(PosPurchaseSavemodel);
                    var Stock = AddMasterinstock(PosPurchaseSavemodel);
                    await _db.PosPurchaseMaster.AddAsync(PosPurchaseSavemodel.PosPurchaseMaster);
                    await _db.TranscationDetails.AddRangeAsync(TranscationDetails);
                    await _db.Stock.AddRangeAsync(Stock);
                    AddNotificationToView("Transaction created Successfully !", true);
                    message = "Registerd Successfully";
                }
                else
                {
                    string[] _vtype = new string[] { "POSPINV", "CPVPOSPINV" };

                    var D = await _db.PosPurchaseDetail.Where(c => c.PosPurchaseMasterId == PosPurchaseSavemodel.PosPurchaseMaster.Id).ToListAsync();
                    var S = await _db.Stock.Where(c => c.PosMasterId == PosPurchaseSavemodel.PosPurchaseMaster.Id).ToListAsync();
                    var T = await _db.TranscationDetails.Where(c => c.Invid == PosPurchaseSavemodel.PosPurchaseMaster.Id && _vtype.Contains(c.Vtype)).ToListAsync();
                    _db.PosPurchaseDetail.RemoveRange(D);
                    _db.TranscationDetails.RemoveRange(T);
                    _db.Stock.RemoveRange(S);

                    await _db.PosPurchaseDetail.AddRangeAsync(PosPurchaseSavemodel.PosPurchaseMaster.PosPurchaseDetailList);
                    await _db.TranscationDetails.AddRangeAsync(PosPurchaseSavemodel.TranscationDetailsList);
                    await _db.Stock.AddRangeAsync(PosPurchaseSavemodel.StockList);
                    _db.PosPurchaseMaster.Update(PosPurchaseSavemodel.PosPurchaseMaster);
                    AddNotificationToView("Transaction Updated Successfully !", true);
                    message = "Updated Successfully";
                }
                await _db.SaveChangesAsync();

            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { message = message });
        }
        [Route("/Pos/PosPurchase/Edit-Purchases/{Id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            var PosPurchaseMaster = await _db.PosPurchaseMaster
                .Include(z => z.PosPurchaseDetailList)
                .SingleOrDefaultAsync(c => c.Id.Equals(Id));

            IEnumerable<SelectListItem> SupplierList = _db.Supplier.Select(c => new SelectListItem
            {
                Value = c.ThirdLevelId.ToString(),
                Text = c.Name
            });
            IEnumerable<SelectListItem> ProductList = _db.Product.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Name} || {c.Sku}"
            });
            IEnumerable<SelectListItem> CategoryList = _db.Category.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            IEnumerable<SelectListItem> SubCategoryList = _db.SubCategory.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            var VM = new PurchaseVM
            {
                SupplierList = SupplierList,
                PosPurchaseMaster = PosPurchaseMaster,
                CategoryList = CategoryList,
                SubCategoryList = SubCategoryList,
                ProductList = ProductList
            };
            return View("Create", VM);
        }
        [Route("/Pos/PosPurchase/Delete/{Id}")]
        [HttpGet]
        public async Task<IActionResult> Delete(long Id)
        {
            var PosPurchaseMaster = await _db.PosPurchaseMaster
                .SingleOrDefaultAsync(c => c.Id.Equals(Id));
            string[] _vtype = new string[] { "POSPINV", "CPVPOSPINV" };

            var D = await _db.PosPurchaseDetail.Where(c => c.PosPurchaseMasterId == Id).ToListAsync();
            var S = await _db.Stock.Where(c => c.PosMasterId == Id).ToListAsync();
            var T = await _db.TranscationDetails.Where(c => c.Invid == Id && _vtype.Contains(c.Vtype)).ToListAsync();
            _db.PosPurchaseDetail.RemoveRange(D);
            _db.TranscationDetails.RemoveRange(T);
            _db.Stock.RemoveRange(S);
            _db.PosPurchaseMaster.RemoveRange(PosPurchaseMaster);
            await _db.SaveChangesAsync();
            AddNotificationToView("Transaction Deleted Successfully !", true);
            return RedirectToAction("Index");
        }

        public List<PosPurchaseDetail> AddMasterinpurchasedetail(PosPurchaseSavemodel PosPurchaseSavemodel)
        {
            var Purchasedetails = new List<PosPurchaseDetail>();
            foreach (var t in PosPurchaseSavemodel.PosPurchaseMaster.PosPurchaseDetailList)
            {
                Purchasedetails.Add(new PosPurchaseDetail
                {
                    Id = t.Id,
                    PosPurchaseMasterId = PosPurchaseSavemodel.dynamicId,
                    ProductId = t.ProductId,
                    ProductName = t.ProductName,
                    Unit = t.Unit,
                    Qty = t.Qty,
                    PurchasePrice = t.PurchasePrice,
                    Total = t.Total,
                });
            }
            return Purchasedetails;
        }
        public List<Stock> AddMasterinstock(PosPurchaseSavemodel PosPurchaseSavemodel)
        {
            var Stock = new List<Stock>();
            foreach (var t in PosPurchaseSavemodel.StockList)
            {
                Stock.Add(new Stock
                {
                    Id = t.Id,
                    PosMasterId = PosPurchaseSavemodel.dynamicId,
                    ProductId = t.ProductId,
                    CategoryId = t.CategoryId,
                    StockIn = t.StockIn,
                    StockOut = 0,
                    UserId = t.UserId,
                    Date = t.Date,
                });
            }
            return Stock;
        }
    }
}
