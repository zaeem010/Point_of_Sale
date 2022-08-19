using IMS.Areas.Pos.Models;
using IMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.Controllers;

namespace IMS.Areas.Pos.Controllers
{
    [Area("Pos")]
    public class SubCategoryController :BaseController 
    {
        private readonly ApplicationDbContext _db;
        public SubCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("/Pos/Sub-Category/List")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.SubCategory.Include(c=>c.Category).ToListAsync();
            return View(Li);
        }
        [Route("/Pos/Sub-Category/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> List = _db.Category.OrderByDescending(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            ViewBag.Catlist = List;
            return View(new SubCategory());
        }
        [Route("/Pos/Sub-Category/Save")]
        [HttpPost]
        public async Task<IActionResult> Save(SubCategory SubCategory)
        {
            string d;
            if (SubCategory.Id == 0)
            {
                await _db.SubCategory.AddAsync(SubCategory);
                AddNotificationToView("Registerd Successfully", true);
                d = "Create";
            }
            else
            {
                _db.SubCategory.Update(SubCategory);
                AddNotificationToView("Updated Successfully", true);
                d = "Index";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(d);
        }
        [Route("/Pos/Sub-Category/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            var Li = await _db.SubCategory
                .SingleOrDefaultAsync(c => c.Id.Equals(Id));
            IEnumerable<SelectListItem> List = _db.Category.OrderByDescending(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            ViewBag.Catlist = List;
            return View("Create", Li);
        }
    }
}
