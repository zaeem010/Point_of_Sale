using IMS.Data;
using IMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Controllers
{
    public class PurchaseController : BaseController
    {
        private long myid { get; set; }
        private readonly ApplicationDbContext _db;
        public PurchaseController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("/Purchase/All-Purchases")]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.PurchaseMaster
                .Include(c=>c.ThirdLevel)
                .ToListAsync();
            return View(Li);
        }
        [Route("/Purchase/Create-New-Purchases")]
        public IActionResult create(PurchaseMaster PurchaseMaster)
        {
            IEnumerable<SelectListItem> SupplierList = _db.Supplier.Select(c => new SelectListItem
            {
                Value = c.ThirdLevelId.ToString(),
                Text = c.Name
            });
            IEnumerable<SelectListItem> ModelList = _db.ModelNo.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            });
            IEnumerable<SelectListItem> CargoList = _db.Cargo.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            PurchaseMaster.InvDate = DateTime.Now;
            var VM = new PurchaseVM
            {
                SupplierList = SupplierList,
                PurchaseMaster = PurchaseMaster,
                ModelList=ModelList,
                CargoList=CargoList
            };
            return View(VM);
        }
        [HttpPost]
        public async Task<IActionResult> Save(purchaseSavemodel purchaseSavemodel)
        {
            string message;
            try
            {
                if (purchaseSavemodel.PurchaseMaster.Id == 0)
                {
                    purchaseSavemodel.PurchaseMaster.Id= _db.PurchaseMaster.Where(p => p != null).Select(p => p.Id).DefaultIfEmpty().Max() + 1;
                    myid= purchaseSavemodel.PurchaseMaster.Id;
                    //
                    var TranscationDetails = AddMasterintransactionid(purchaseSavemodel);
                    var Purchasedetails = AddMasterinpurchasedetail(purchaseSavemodel);
                    await _db.PurchaseMaster.AddAsync(purchaseSavemodel.PurchaseMaster);
                    await _db.TranscationDetails.AddRangeAsync(TranscationDetails);
                    AddNotificationToView("Transaction created Successfully !", true);
                    message = "Registerd Successfully";
                }
                else
                {
                    string[] _vtype = new string[] {"PINV","CPVPINV"};
                    
                    var D = await _db.PurchaseDetail.Where(c=>c.PurchaseMasterId== purchaseSavemodel.PurchaseMaster.Id).ToListAsync();
                    var T = await _db.TranscationDetails.Where(c=>c.Invid== purchaseSavemodel.PurchaseMaster.Id && _vtype.Contains(c.Vtype)).ToListAsync();
                    _db.PurchaseDetail.RemoveRange(D);
                    _db.TranscationDetails.RemoveRange(T);

                    await _db.PurchaseDetail.AddRangeAsync(purchaseSavemodel.PurchaseMaster.PurchaseDetailList);
                    await _db.TranscationDetails.AddRangeAsync(purchaseSavemodel.TranscationDetailList);
                    _db.PurchaseMaster.Update(purchaseSavemodel.PurchaseMaster);
                    AddNotificationToView("Transaction Updated Successfully !", true);
                    message = "Updated Successfully";
                }
                await _db.SaveChangesAsync();
                
            }
            catch (Exception e)
            {
                 message = e.Message;
            }
            return Json(new {message= message});
        }
        public List<TranscationDetails> AddMasterintransactionid(purchaseSavemodel purchaseSavemodel) 
        {
            var TranscationDetails = new List<TranscationDetails>();
            foreach (var t in purchaseSavemodel.TranscationDetailList) 
            {
                TranscationDetails.Add(new TranscationDetails
                {
                    Id = t.Id,
                    Invid =myid,
                    Vtype=t.Vtype,
                    TransDate=t.TransDate,
                    ThirdLevelId=t.ThirdLevelId,
                    Dr=t.Dr,
                    Cr=t.Cr,
                    UserId=t.UserId
                });
            }
            return TranscationDetails;
        }
        public List<PurchaseDetail> AddMasterinpurchasedetail(purchaseSavemodel purchaseSavemodel) 
        {
            var Purchasedetails = new List<PurchaseDetail>();
            foreach (var t in purchaseSavemodel.PurchaseMaster.PurchaseDetailList) 
            {
                Purchasedetails.Add(new PurchaseDetail
                {
                    Id = t.Id,
                    PurchaseMasterId =myid,
                    VName=t.VName,
                    ModelNo = t.ModelNo,
                    ChassisNo = t.ChassisNo,
                    EngineNo = t.EngineNo,
                    KeyNo = t.KeyNo,
                    BikeNo = t.BikeNo,
                    Color = t.Color,
                    Rate = t.Rate,
                    CargoRate = t.CargoRate,
                    Discount = t.Discount,
                    Total = t.Total,
                });
            }
            return Purchasedetails;
        }
        [HttpPost]
        public async Task<IActionResult> SaveSupplier(Supplier Supplier)
        {
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
            }
            await _db.SaveChangesAsync();
            var SupplierList = await _db.Supplier.Where(c => c.Verify.Equals(true)).ToListAsync();
            return Json(new { SupplierList = SupplierList });
        }
        
        [Route("/Purchase/Edit-Purchases/{Id}")]
        public async Task<IActionResult> Edit(long Id)
        {
            IEnumerable<SelectListItem> SupplierList = _db.Supplier.Select(c => new SelectListItem
            {
                Value = c.ThirdLevelId.ToString(),
                Text = c.Name
            });
            IEnumerable<SelectListItem> ModelList = _db.ModelNo.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            });
            IEnumerable<SelectListItem> CargoList = _db.Cargo.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            var PurchaseMaster = await _db.PurchaseMaster
                .Include(z=>z.PurchaseDetailList)
                .SingleOrDefaultAsync(c => c.Id.Equals(Id) );
            var VM = new PurchaseVM
            {
                SupplierList = SupplierList,
                PurchaseMaster = PurchaseMaster,
                ModelList = ModelList,
                CargoList = CargoList
            };
            return View("Create",VM);
        }
    }
}
