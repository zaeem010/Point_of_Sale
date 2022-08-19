using IMS.Areas.Pos.Models;
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
    public class PosPurchaseController : Controller
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
                CategoryList =CategoryList,
                SubCategoryList=SubCategoryList,
                ProductList=ProductList
            };
            return View(VM);
        }
        [Route("/Pos/PosPurchase/GetProductBysubList")]
        [HttpGet]
        public async Task<JsonResult> GetProductBysubList(long Id)
        {
            var Li = await _db.Product.Where(x=>x.SubCategoryId.Equals(Id))
                .ToListAsync();
            return Json(Li);
        }
        [Route("/Pos/PosPurchase/GetProductByCatList")]
        [HttpGet]
        public async Task<JsonResult> GetProductByCatList(long Id)
        {
            var Li = await _db.Product.Where(x=>x.CategoryId.Equals(Id))
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
    }
}
