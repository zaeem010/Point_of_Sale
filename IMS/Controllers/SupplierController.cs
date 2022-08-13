using IMS.Data;
using IMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly ApplicationDbContext _db;
        public SupplierController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("/Supplier/List")]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.Supplier.ToListAsync();
            return View(Li);
        }
        [Route("/Supplier/Create")]
        public IActionResult Create()
        {
            return View(new Supplier());
        }
        [HttpPost]
        public async Task<IActionResult> Save(Supplier Supplier)
        {
            string d;
            var thirdlevel = new ThirdLevel();
            if (Supplier.Id == 0)
            {
                thirdlevel.Id = await _db.ThirdLevel.Where(p => p != null && p.FirstlevelId == 2001).Select(p => p.Id).DefaultIfEmpty().MaxAsync() + 1;
                thirdlevel.AccountHeadId = 2;
                thirdlevel.FirstlevelId = 2001;
                thirdlevel.SecondlevelId = 2000001;
                thirdlevel.AccountTitle = Supplier.Name;
                thirdlevel.AccountType = "Supplier";
                thirdlevel.Dr = 0;
                thirdlevel.Cr = 0;
                Supplier.ThirdLevelId = thirdlevel.Id;
                Supplier.Verify = true;
                await _db.Supplier.AddAsync(Supplier);
                await _db.ThirdLevel.AddAsync(thirdlevel);
                
                AddNotificationToView("Registerd Successfully", true);
                d = "Create";
            }
            else
            {
                thirdlevel = await _db.ThirdLevel.SingleOrDefaultAsync(c=>c.Id.Equals(Supplier.ThirdLevelId));
                thirdlevel.AccountTitle = Supplier.Name;
                _db.Supplier.Update(Supplier);
                AddNotificationToView("Updated Successfully", true);
                d = "Index";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(d);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            var Li = await _db.Supplier.SingleOrDefaultAsync(c => c.Id.Equals(Id));
            return View("Create", Li);
        }
    }
}
