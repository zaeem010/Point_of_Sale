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
    public class CargoController : BaseController
    {
        private readonly ApplicationDbContext _db;
        public CargoController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("/Cargo/List")]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.Cargo.ToListAsync();
            return View(Li);
        }
        [Route("/Cargo/Create")]
        public IActionResult Create()
        {
            return View(new Cargo());
        }
        [HttpPost]
        public async Task<IActionResult> Save(Cargo Cargo)
        {
            string d;
            if (Cargo.Id == 0)
            {
                await _db.Cargo.AddAsync(Cargo);
                AddNotificationToView("Registerd Successfully", true);
                d = "Create";
            }
            else
            {
                _db.Cargo.Update(Cargo);
                AddNotificationToView("Updated Successfully", true);
                d = "Index";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(d);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var Li = await _db.Cargo.SingleOrDefaultAsync(c => c.Id.Equals(Id));
            return View("Create", Li);
        }
    }
}
