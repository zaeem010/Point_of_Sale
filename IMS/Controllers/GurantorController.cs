using IMS.Data;
using IMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Controllers
{
    public class GurantorController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment host;
        public GurantorController(/*ICurrentuser currentUser, */IWebHostEnvironment host, ApplicationDbContext db)
        {
            _db = db;
            this.host = host;
            //_currentUser = currentUser;
        }
        [Route("/Gurantor/List")]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.Gurantor.ToListAsync();
            return View(Li);
        }
        [Route("/Gurantor/Create")]
        public IActionResult Create()
        {
            return View(new Gurantor());
        }
        [HttpPost]
        public async Task<IActionResult> Save(Gurantor Gurantor)
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
                    if (files[i].Name == "Front")
                    {
                        if (files[i].ContentType.ToString() == "image/jpeg" || files[i].ContentType.ToString() == "image/png" || files[i].ContentType.ToString() == "image/PNG" || files[i].ContentType.ToString() == "image/JPEG" || files[i].ContentType.ToString() == "image/JPG")
                        {
                            Gurantor.CnicFront = num + filename;
                        }
                    }
                    if (files[i].Name == "Back")
                    {
                        if (files[i].ContentType.ToString() == "image/jpeg" || files[i].ContentType.ToString() == "image/png" || files[i].ContentType.ToString() == "image/PNG" || files[i].ContentType.ToString() == "image/JPEG" || files[i].ContentType.ToString() == "image/JPG")
                        {
                            Gurantor.CnicBack = num + filename;
                        }
                    }
                    if (files[i].Name == "Img")
                    {
                        if (files[i].ContentType.ToString() == "image/jpeg" || files[i].ContentType.ToString() == "image/png" || files[i].ContentType.ToString() == "image/PNG" || files[i].ContentType.ToString() == "image/JPEG" || files[i].ContentType.ToString() == "image/JPG")
                        {
                            Gurantor.ImageUrl = num + filename;
                        }
                    }
                }
            }
            //Img end
            string d;
            if (Gurantor.Id == 0) 
            {
                Gurantor.Verify = true;
                await _db.Gurantor.AddAsync(Gurantor);
                AddNotificationToView("Registerd Successfully", true);
                d = "Create";
            }
            else
            {
                _db.Gurantor.Update(Gurantor);
                AddNotificationToView("Updated Successfully", true);
                d = "Index";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(d);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            var Li = await _db.Gurantor.SingleOrDefaultAsync(c => c.Id.Equals(Id));
            return View("Create", Li);
        }
    }
}
