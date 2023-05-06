using IMS.Areas.Pos.Models;
using IMS.Controllers;
using IMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Areas.Pos.Controllers
{
    [Area("Pos")]
    public class SaleController : BaseController
    {
        private readonly ApplicationDbContext _db;
        public SaleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/Pos/Sale/Screen")]
        public async Task<IActionResult> Salescreen()
        {
            var SalesList = await _db.PosSaleMaster
                .Include(x => x.ThirdLevel)
                .Take(20).OrderByDescending(x => x.Id).ToListAsync();
            var pro = await _db.Product
               .Include(z => z.Category)
               .Select(x => new Product
               {
                   Id = x.Id,
                   Name = x.Name,
                   Sku = x.Sku,
                   SalePrice = x.SalePrice,
                   PurchasePrice = x.PurchasePrice,
                   OpeningStock = x.OpeningStock,
                   Unit = x.Unit,
                   ImagePath = x.ImagePath,
                   CategoryId = x.CategoryId,
                   SubCategoryId = x.SubCategoryId,
                   Status = x.Status,
                   Category = x.Category,
                   Stock = _db.Stock.Where(z => z.ProductId.Equals(x.Id)).ToList(),
               }).ToListAsync();
            var VM = new SaleScreenVM
            {
                Categorys = await _db.Category.ToListAsync(),
                SubCategorys = await _db.SubCategory.ToListAsync(),
                Products = pro,
                PosSaleMasters = SalesList
            };
            return View(VM);
        }
        [Route("/Pos/Sale/Screen-2")]
        public async Task<IActionResult> Salescreen_2()
        {
            var SalesList = await _db.PosSaleMaster.Take(20).OrderByDescending(x => x.Id).ToListAsync();
            var pro = await _db.Product
               .Include(z => z.Category)
               .Select(x => new Product
               {
                   Id = x.Id,
                   Name = x.Name,
                   Sku = x.Sku,
                   SalePrice = x.SalePrice,
                   PurchasePrice = x.PurchasePrice,
                   OpeningStock = x.OpeningStock,
                   Unit = x.Unit,
                   ImagePath = x.ImagePath,
                   CategoryId = x.CategoryId,
                   SubCategoryId = x.SubCategoryId,
                   Status = x.Status,
                   Category = x.Category,
                   Stock = _db.Stock.Where(z => z.ProductId.Equals(x.Id)).ToList(),
               }).ToListAsync();
            var VM = new SaleScreenVM
            {
                Categorys = await _db.Category.ToListAsync(),
                SubCategorys = await _db.SubCategory.ToListAsync(),
                Products = pro,
                PosSaleMasters = SalesList
            };
            return View(VM);
        }
        [HttpGet, Route("/Pos/Sale/GetProductbyCatId")]
        public async Task<IActionResult> GetProductbyCatId(long Id)
        {
            var pro = await _db.Product
                .Where(x => x.CategoryId.Equals(Id))
                .Include(z => z.Category)
                .Select(x => new Product
                {
                    Id = x.Id,
                    Name = x.Name,
                    Sku = x.Sku,
                    SalePrice = x.SalePrice,
                    PurchasePrice = x.PurchasePrice,
                    OpeningStock = x.OpeningStock,
                    Unit = x.Unit,
                    ImagePath = x.ImagePath,
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    Category = x.Category,
                    Stock = _db.Stock.Where(z => z.ProductId.Equals(x.Id)).ToList(),
                }).ToListAsync();
            return Json(pro);
        }
        [HttpGet, Route("/Pos/Sale/GetProductbyName")]
        public async Task<IActionResult> GetProductbyName(string searchterm)
        {
            var pro = await _db.Product.Where(x => x.Name.Contains(searchterm))
                .Include(z => z.Category)
                .Select(x => new Product
                {
                    Id = x.Id,
                    Name = x.Name,
                    Sku = x.Sku,
                    SalePrice = x.SalePrice,
                    PurchasePrice = x.PurchasePrice,
                    OpeningStock = x.OpeningStock,
                    Unit = x.Unit,
                    ImagePath = x.ImagePath,
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    Category = x.Category,
                    Stock = _db.Stock.Where(z => z.ProductId.Equals(x.Id)).ToList(),
                }).ToListAsync();
            return Json(pro);
        }
        [HttpGet, Route("/Pos/Sale/GetProductbyPrice")]
        public async Task<IActionResult> GetProductbyPrice(string searchterm)
        {
            var pro = await _db.Product.Where(x => x.SalePrice.ToString().Contains(searchterm))
                .Include(z => z.Category)
                .Select(x => new Product
                {
                    Id = x.Id,
                    Name = x.Name,
                    Sku = x.Sku,
                    SalePrice = x.SalePrice,
                    PurchasePrice = x.PurchasePrice,
                    OpeningStock = x.OpeningStock,
                    Unit = x.Unit,
                    ImagePath = x.ImagePath,
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    Category = x.Category,
                    Stock = _db.Stock.Where(z => z.ProductId.Equals(x.Id)).ToList(),
                }).ToListAsync();
            return Json(pro);
        }
        [HttpGet, Route("/Pos/Sale/GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var pro = await _db.Product
                .Include(z => z.Category)
                .Select(x => new Product
                {
                    Id = x.Id,
                    Name = x.Name,
                    Sku = x.Sku,
                    SalePrice = x.SalePrice,
                    PurchasePrice = x.PurchasePrice,
                    OpeningStock = x.OpeningStock,
                    Unit = x.Unit,
                    ImagePath = x.ImagePath,
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    Category = x.Category,
                    Stock = _db.Stock.Where(z => z.ProductId.Equals(x.Id)).ToList(),
                }).ToListAsync();
            return Json(pro);
        }
        [HttpPost, Route("/Pos/Sale/Save")]
        public async Task<IActionResult> Save(PosSaleSavemodel PosSaleSavemodel)
        {
            string message;
            try
            {
                if (PosSaleSavemodel.PosSaleMaster.Id == 0)
                {
                    PosSaleSavemodel.PosSaleMaster.Id = _db.PosSaleMaster.Where(p => p != null).Select(p => p.Id).DefaultIfEmpty().Max() + 1;
                    PosSaleSavemodel.dynamicId = PosSaleSavemodel.PosSaleMaster.Id;
                    //
                    var TranscationDetails = AddPosSaleMasterintransactionid(PosSaleSavemodel);
                    var Saledetails = AddMasterinsaledetail(PosSaleSavemodel);
                    var Stock = AddMasterinSalestock(PosSaleSavemodel);
                    await _db.PosSaleMaster.AddAsync(PosSaleSavemodel.PosSaleMaster);
                    await _db.TranscationDetails.AddRangeAsync(TranscationDetails);
                    await _db.Stock.AddRangeAsync(Stock);
                    AddNotificationToView("Transaction created Successfully !", true);
                    message = "Registerd Successfully";
                }
                else
                {
                    string[] _vtype = new string[] { "POSSINV", "CRVPOSSINV" };

                    var D = await _db.PosSaleDetail.Where(c => c.PosSaleMasterId == PosSaleSavemodel.PosSaleMaster.Id).ToListAsync();
                    var S = await _db.Stock.Where(c => c.PosMasterId == PosSaleSavemodel.PosSaleMaster.Id).ToListAsync();
                    var T = await _db.TranscationDetails.Where(c => c.Invid == PosSaleSavemodel.PosSaleMaster.Id && _vtype.Contains(c.Vtype)).ToListAsync();
                    _db.PosSaleDetail.RemoveRange(D);
                    _db.TranscationDetails.RemoveRange(T);
                    _db.Stock.RemoveRange(S);

                    await _db.PosSaleDetail.AddRangeAsync(PosSaleSavemodel.PosSaleMaster.PosSaleDetails);
                    await _db.TranscationDetails.AddRangeAsync(PosSaleSavemodel.TranscationDetailsList);
                    await _db.Stock.AddRangeAsync(PosSaleSavemodel.StockList);
                    _db.PosSaleMaster.Update(PosSaleSavemodel.PosSaleMaster);
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

        public List<PosSaleDetail> AddMasterinsaledetail(PosSaleSavemodel PosSaleSavemodel)
        {
            var saledetails = new List<PosSaleDetail>();
            foreach (var t in PosSaleSavemodel.PosSaleMaster.PosSaleDetails)
            {
                saledetails.Add(new PosSaleDetail
                {
                    Id = t.Id,
                    PosSaleMasterId = PosSaleSavemodel.dynamicId,
                    ProductId = t.ProductId,
                    ProductName = t.ProductName,
                    Unit = t.Unit,
                    Qty = t.Qty,
                    SalePrice = t.SalePrice,
                    Total = t.Total,
                });
            }
            return saledetails;
        }
        public List<Stock> AddMasterinSalestock(PosSaleSavemodel PosSaleSavemodel)
        {
            var Stock = new List<Stock>();
            foreach (var t in PosSaleSavemodel.StockList)
            {
                Stock.Add(new Stock
                {
                    Id = t.Id,
                    PosMasterId = PosSaleSavemodel.dynamicId,
                    ProductId = t.ProductId,
                    CategoryId = t.CategoryId,
                    StockIn = 0,
                    StockOut = t.StockOut,
                    UserId = t.UserId,
                    Date = t.Date,
                });
            }
            return Stock;
        }
    }
}
