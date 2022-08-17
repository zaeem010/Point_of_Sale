using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.Controllers;
using IMS.Data;
using Microsoft.EntityFrameworkCore;
using IMS.Areas.Pos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IMS.Areas.Pos.Controllers
{
    [Area("Pos")]
    public class ProductController : BaseController
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("/Pos/Product/List")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.Product.ToListAsync();
            return View(Li);
        }
        [Route("/Pos/Product/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> List = _db.Category.OrderByDescending(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            ViewBag.Catlist = List;
            return View(new Product());
        }
    }
}
