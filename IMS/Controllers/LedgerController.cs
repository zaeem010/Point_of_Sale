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
    public class LedgerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LedgerController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("/Ledger/Detail-Ledger")]
        public IActionResult Index()
        {
            IEnumerable<SelectListItem> AccountList = _db.ThirdLevel.OrderByDescending(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.AccountTitle
            });
            var VM = new LedgerVM 
            {
                AccountList= AccountList,
                StDate = DateTime.Now,
                EnDate=DateTime.Now
            };
            return View(VM);
        }
        [HttpPost]
        public async Task<IActionResult> GetLedger(LedgerVM LedgerVM)
        {
            IEnumerable<SelectListItem> AccountList = _db.ThirdLevel.OrderByDescending(c => c.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.AccountTitle
            });
            List<LedgerDTO> Transaction = new List<LedgerDTO>();
            double ThirdlevelOp =0, TransactionOp=0;
            if (ModelState.IsValid)
            {
                ThirdlevelOp = await _db.ThirdLevel.Where(t => t.Id == LedgerVM.LedgerAccountNo).Select(x => x.AccountHeadId == 1 || x.AccountHeadId == 5 ? x.Dr - x.Cr : x.Cr - x.Dr).SumAsync();
                TransactionOp = await _db.TranscationDetails.Where(t => t.ThirdLevelId == LedgerVM.LedgerAccountNo && t.TransDate < LedgerVM.StDate).Select(x => x.HeadId == 1 || x.HeadId == 5 ? x.Dr - x.Cr : x.Cr - x.Dr).SumAsync();
               Transaction = await (from Trans in _db.TranscationDetails
                                     where Trans.ThirdLevelId == LedgerVM.LedgerAccountNo && Trans.TransDate >= LedgerVM.StDate && Trans.TransDate <= LedgerVM.EnDate
                                     select new LedgerDTO
                                     {
                                         ThirdLevelId = Trans.ThirdLevelId,
                                         TransDate = Trans.TransDate,
                                         Vtype = Trans.Vtype,
                                         TransDesc = Trans.TransDes,
                                         Dr = Trans.Dr,
                                         Cr = Trans.Cr,
                                         HeadId = Trans.HeadId,
                                         ThirdLevel = _db.ThirdLevel.Where(x=>x.Id.Equals(Trans.ThirdLevelId)).FirstOrDefault(),
                                     })
                                     .ToListAsync();
            }
            var VM = new LedgerVM
            {
                AccountList = AccountList,
                StDate = LedgerVM.StDate,
                EnDate = LedgerVM.EnDate,
                LedgerAccountNo =LedgerVM.LedgerAccountNo,
                OpBalance = ThirdlevelOp + TransactionOp,
                Transcations =Transaction
            };
            return View(VM);
        }
    }
}
