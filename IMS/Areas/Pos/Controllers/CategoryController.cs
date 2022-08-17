using IMS.Areas.Pos.Models;
using IMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using IMS.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace IMS.Areas.Pos.Controllers
{
    [Area("Pos")]
    public class CategoryController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment host;
        public CategoryController(IWebHostEnvironment host, ApplicationDbContext db)
        {
            _db = db;
            this.host = host;
        }
        [Route("/Pos/Category/List")]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.Category.ToListAsync();
            return View(Li);
        }
        [Route("/Pos/Category/Create")]
        public IActionResult Create()
        {
            return View(new Category());
        }
        [Route("/Pos/Category/Save")]
        [HttpPost]
        public async Task<IActionResult> Save(Category Category)
        {
            //Img 
            Random r = new Random();
            int num = r.Next();
            var webrootpath = host.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                for (int i = 0; i < files.Count(); i++)
                {
                    var uploads = Path.Combine(webrootpath, "Uploads");
                    var filename = Path.GetFileName(files[i].FileName);
                    using (var stream = new FileStream(Path.Combine(uploads, num + filename), FileMode.Create))
                    {
                        files[i].CopyTo(stream);
                    }
                    if (files[i].Name == "Img")
                    {
                        if (files[i].ContentType.ToString() == "image/jpeg" || files[i].ContentType.ToString() == "image/png" || files[i].ContentType.ToString() == "image/PNG" || files[i].ContentType.ToString() == "image/JPEG" || files[i].ContentType.ToString() == "image/JPG")
                        {
                            Category.ImagePath = num + filename;
                        }
                    }
                }
            }
            //Img end
            string d;
            if (Category.Id == 0)
            {
                await _db.Category.AddAsync(Category);
                AddNotificationToView("Registerd Successfully", true);
                d = "Create";
            }
            else
            {
                _db.Category.Update(Category);
                AddNotificationToView("Updated Successfully", true);
                d = "Index";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(d);
        }
        [Route("/Pos/Category/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            var Li = await _db.Category
                .SingleOrDefaultAsync(c => c.Id.Equals(Id));
            return View("Create", Li);
        }
    }
}
