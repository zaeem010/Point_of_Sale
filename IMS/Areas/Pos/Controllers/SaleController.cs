using IMS.Areas.Pos.Models;
using IMS.Controllers;
using IMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var VM = new SaleScreenVM
            {
                Categorys = await _db.Category.ToListAsync(),
                SubCategorys = await _db.SubCategory.ToListAsync(),
                Products = await _db.Product.ToListAsync(),
            };
            return View(VM);
        }
        [HttpGet, Route("/Pos/Sale/GetProductbyCatId")]
        public async Task<IActionResult> GetProductbyCatId(long Id)
        {
            var pro = await _db.Product.Where(x => x.CategoryId.Equals(Id)).ToListAsync();
            return Json(pro);
        }
    }
}
