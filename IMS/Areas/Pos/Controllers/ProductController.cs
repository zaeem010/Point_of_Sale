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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace IMS.Areas.Pos.Controllers
{
    [Area("Pos")]
    public class ProductController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment host;
        public ProductController(IWebHostEnvironment host, ApplicationDbContext db)
        {
            _db = db;
            this.host = host;
        }
        [Route("/Pos/Product/List")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.Product.Where(c=>c.Status).ToListAsync();
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
        [Route("/Pos/Product/Save")]
        [HttpPost]
        public async Task<IActionResult> Save(Product Product)
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
                            Product.ImagePath = num + filename;
                        }
                    }
                }
            }
            //Img end
            string d;
            if (Product.Id == 0)
            {
                await _db.Product.AddAsync(Product);
                AddNotificationToView("Registerd Successfully", true);
                d = "Create";
            }
            else
            {
                _db.Product.Update(Product);
                AddNotificationToView("Updated Successfully", true);
                d = "Index";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(d);
        }
        [Route("/Pos/Product/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            var Li = await _db.Product
                .SingleOrDefaultAsync(c => c.Id.Equals(Id));
            IEnumerable<SelectListItem> List = _db.Category.OrderByDescending(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            ViewBag.Catlist = List;
            return View("Create", Li);
        }
        [Route("/Pos/Product/Delete")]
        [HttpGet]
        public async Task<IActionResult> Delete(long Id)
        {
            var Li = await _db.Product
                .SingleOrDefaultAsync(c => c.Id.Equals(Id));
            Li.Status = false;
            await _db.SaveChangesAsync();
            AddNotificationToView("Deleted Successfully", true);
            return RedirectToAction("Index");
        }
    }
}
