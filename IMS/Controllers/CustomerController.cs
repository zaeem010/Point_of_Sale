using IMS.Data;
using IMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment host;
        public CustomerController(/*ICurrentuser currentUser, */IWebHostEnvironment host, ApplicationDbContext db)
        {
            _db = db;
            this.host = host;
            //_currentUser = currentUser;
        }
        [Route ("/Customer/List")]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.Customer.ToListAsync();
            return View(Li);
        }
        [Route("/Customer/Create")]
        public async Task<IActionResult> Create()
        {
            var GurantorList = await _db.Gurantor.Where(c => c.Verify.Equals(true)).ToListAsync();
            var VM = new CustomerVM {
                Customer = new Customer(),
                Gurantor = new Gurantor(),
                GurantorList= GurantorList
            };
            return View(VM);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Customer Customer ,int[] GurantorList)
        {
            foreach (var Gu in GurantorList) 
            {
                Customer.CustomerGurantorList.Add(new CustomerGurantor
                {
                    CustomerId = Customer.Id,
                    GurantorId = Gu,
                });
            }
            //Img 
            Random r = new Random();
            int num = r.Next();
            var webrootpath = host.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                for (int i =0; i< files.Count(); i++) 
                {
                    var uploads = Path.Combine(webrootpath, "Uploads");
                    var filename = Path.GetFileName(files[i].FileName);
                    using (var stream = new FileStream(Path.Combine(uploads, num + filename), FileMode.Create))
                    {
                        files[i].CopyTo(stream);
                    }
                    if (files[i].Name =="Front") 
                    {
                        if (files[i].ContentType.ToString() == "image/jpeg" || files[i].ContentType.ToString() == "image/png" || files[i].ContentType.ToString() == "image/PNG" || files[i].ContentType.ToString() == "image/JPEG" || files[i].ContentType.ToString() == "image/JPG")
                        {
                            Customer.CnicFront = num + filename;
                        }
                    }
                    if (files[i].Name =="Back") 
                    {
                        if (files[i].ContentType.ToString() == "image/jpeg" || files[i].ContentType.ToString() == "image/png" || files[i].ContentType.ToString() == "image/PNG" || files[i].ContentType.ToString() == "image/JPEG" || files[i].ContentType.ToString() == "image/JPG")
                        {
                            Customer.CnicBack = num + filename;
                        }
                    }
                    if (files[i].Name == "Img") 
                    {
                        if (files[i].ContentType.ToString() == "image/jpeg" || files[i].ContentType.ToString() == "image/png" || files[i].ContentType.ToString() == "image/PNG" || files[i].ContentType.ToString() == "image/JPEG" || files[i].ContentType.ToString() == "image/JPG")
                        {
                            Customer.ImageUrl = num + filename;
                        }
                    }
                }
            }
            //Img end
            string d;
            var thirdlevel = new ThirdLevel();
            if (Customer.Id == 0)
            {
                thirdlevel.Id = await _db.ThirdLevel.Where(p => p != null && p.FirstlevelId == 1001).Select(p => p.Id).DefaultIfEmpty().MaxAsync() + 1;
                thirdlevel.AccountHeadId = 1;
                thirdlevel.FirstlevelId = 1001;
                thirdlevel.SecondlevelId = 1000002;
                thirdlevel.AccountTitle = Customer.Name;
                thirdlevel.AccountType = "Customer";
                thirdlevel.Dr = 0;
                thirdlevel.Cr = 0;
                Customer.ThirdLevelId = thirdlevel.Id;
                Customer.Verify = true;
                await _db.Customer.AddAsync(Customer);
                await _db.ThirdLevel.AddAsync(thirdlevel);
                AddNotificationToView("Registerd Successfully",true);
                d = "Create";
            }
            else 
            {
                var list = await _db.CustomerGurantor.Where(c => c.CustomerId.Equals(Customer.Id)).ToListAsync();
                _db.CustomerGurantor.RemoveRange(list);
                thirdlevel = await _db.ThirdLevel.SingleOrDefaultAsync(c => c.Id.Equals(Customer.ThirdLevelId));
                thirdlevel.AccountTitle = Customer.Name;
                await _db.CustomerGurantor.AddRangeAsync(Customer.CustomerGurantorList);
                _db.Customer.Update(Customer);
                AddNotificationToView("Updated Successfully", true);
                d = "Index";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(d);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            var Li = await _db.Customer
                .Include(c=>c.CustomerGurantorList)
                .ThenInclude(z=>z.Gurantor)
                .SingleOrDefaultAsync(c => c.Id.Equals(Id));
            var GurantorList = await _db.Gurantor.Where(c => c.Verify.Equals(true)).ToListAsync();
            var VM = new CustomerVM
            {
                Customer = Li,
                Gurantor = new Gurantor(),
                GurantorList = GurantorList
            };
            return View("Create", VM);
        }
        [HttpPost]
        public async Task<IActionResult> SaveGurantor(IFormCollection form)
        {
            var Request = form["CustomerRequest"];
            var Gurantor = JsonConvert.DeserializeObject<Gurantor>(Request);
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
            if (Gurantor.Id == 0)
            {
                await _db.Gurantor.AddAsync(Gurantor);
                //AddNotificationToView("Registerd Successfully", true);
            }
            await _db.SaveChangesAsync();
            var GurantorList = await _db.Gurantor.Where(c => c.Verify.Equals(true)).ToListAsync();
            return Json(new { GurantorList = GurantorList });
        }
    }
}
