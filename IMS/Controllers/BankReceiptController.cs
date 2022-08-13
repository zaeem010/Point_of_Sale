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
    public class BankReceiptController : BaseController
    {
        private readonly ApplicationDbContext _db;
        public BankReceiptController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("/Bank-Receipt/Bank-Receive-Detail")]
        public async Task<IActionResult> Index()
        {
            var Li = await _db.VoucherMaster.Where(c => c.Vtype.Equals("BRV"))
                .Include(z => z.ThirdLevel)
                .Include(z => z.VoucherDetail).ThenInclude(x => x.ThirdLevel)
                .ToListAsync();
            return View(Li);
        }
        [HttpGet]
        [Route("/Bank-Receipt/Receive-Using-Bank")]
        public IActionResult Create(VoucherMaster VoucherMaster)
        {
            IEnumerable<SelectListItem> AccountList = _db.ThirdLevel.OrderByDescending(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.AccountTitle
            });
            IEnumerable<SelectListItem> BankList = _db.ThirdLevel.Where(c => c.SecondlevelId == 1000003).OrderByDescending(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.AccountTitle
            });
            VoucherMaster.GeneralId = _db.VoucherMaster.Where(p => p != null && p.Vtype.Equals("BRV")).Select(p => p.GeneralId).DefaultIfEmpty().Max() + 1;
            VoucherMaster.VoucherDate = DateTime.Now;
            var VM = new VoucherVM
            {
                VoucherMaster = VoucherMaster,
                AccountList = AccountList,
                BankList = BankList,
            };
            return View(VM);
        }
        [HttpPost]
        public async Task<IActionResult> Save(VoucherSavemodel VoucherSavemodel)
        {
            string message;
            try
            {
                if (VoucherSavemodel.VoucherMaster.Id == 0)
                {
                    VoucherSavemodel.VoucherMaster.Id = _db.VoucherMaster.Where(p => p != null).Select(p => p.Id).DefaultIfEmpty().Max() + 1;
                    VoucherSavemodel.dynamicid = VoucherSavemodel.VoucherMaster.Id;
                    //
                    var TranscationDetails = AddMasterintransactionid(VoucherSavemodel);
                    var Voucherdetails = AddMasterinVoucherdetail(VoucherSavemodel);
                    await _db.VoucherMaster.AddAsync(VoucherSavemodel.VoucherMaster);
                    await _db.TranscationDetails.AddRangeAsync(TranscationDetails);
                    AddNotificationToView("Transaction created Successfully !", true);
                    message = "Registerd Successfully";
                }
                else
                {
                    string[] _vtype = new string[] { "BRV" };

                    var D = await _db.VoucherDetail.Where(c => c.VoucherMasterId == VoucherSavemodel.VoucherMaster.Id).ToListAsync();
                    var T = await _db.TranscationDetails.Where(c => c.Invid == VoucherSavemodel.VoucherMaster.Id && _vtype.Contains(c.Vtype)).ToListAsync();
                    _db.VoucherDetail.RemoveRange(D);
                    _db.TranscationDetails.RemoveRange(T);

                    await _db.VoucherDetail.AddRangeAsync(VoucherSavemodel.VoucherMaster.VoucherDetail);
                    await _db.TranscationDetails.AddRangeAsync(VoucherSavemodel.TranscationDetailList);
                    _db.VoucherMaster.Update(VoucherSavemodel.VoucherMaster);
                    AddNotificationToView("Transaction Updated Successfully !", true);
                    message = "Updated Successfully";
                }
                await _db.SaveChangesAsync();

            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { message = message });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            IEnumerable<SelectListItem> AccountList = _db.ThirdLevel.OrderByDescending(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.AccountTitle
            });
            IEnumerable<SelectListItem> BankList = _db.ThirdLevel.Where(c => c.SecondlevelId == 1000003).OrderByDescending(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.AccountTitle
            });
            var VoucherMaster = await _db.VoucherMaster
                .Include(c => c.ThirdLevel)
                .Include(z => z.VoucherDetail).ThenInclude(z => z.ThirdLevel)
                .SingleOrDefaultAsync(c => c.Id.Equals(Id));
            var VM = new VoucherVM
            {
                VoucherMaster = VoucherMaster,
                AccountList = AccountList,
                BankList = BankList,
            };
            return View("Create", VM);
        }
        [HttpGet]
        public async Task<IActionResult> Report(long Id)
        {
            var VoucherMaster = await _db.VoucherMaster
                .Include(c => c.ThirdLevel)
                .Include(z => z.VoucherDetail).ThenInclude(z => z.ThirdLevel)
                .SingleOrDefaultAsync(c => c.Id.Equals(Id));
            var VM = new VoucherVM
            {
                VoucherMaster = VoucherMaster,
                InWords = NumberToWords(Convert.ToInt32(VoucherMaster.TAmount))
            };
            return View(VM);
        }
    }
}
