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
    public class ModelNoController : BaseController
    {
        private readonly ApplicationDbContext _db;
        public ModelNoController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("/ModelNo/All-Models")]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.ModelNo.ToListAsync();
            return View(Li);
        }
        [Route("/ModelNo/New-Models")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ModelNo ModelNo)
        {
            string d;
            if (ModelNo.Id == 0)
            {
                await _db.ModelNo.AddAsync(ModelNo);
                AddNotificationToView("Registerd Successfully", true);
                d = "Create";
            }
            else
            {
                _db.ModelNo.Update(ModelNo);
                AddNotificationToView("Updated Successfully", true);
                d = "Index";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(d);
        }
        [HttpPost]
        public async Task<IActionResult> SaveVehicle(ModelNo ModelNo)
        {
            if (ModelNo.Id == 0)
            {
                await _db.ModelNo.AddAsync(ModelNo);
            }
            await _db.SaveChangesAsync();
            var Li = await _db.ModelNo.ToListAsync();
            return Json(new { Li =Li});
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var Li = await _db.ModelNo.SingleOrDefaultAsync(c => c.Id.Equals(Id));
            return View("Create", Li);
        }
    }
}
