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
    public class BalanceSheetController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BalanceSheetController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("/BalanceSheet/Balance-Sheet")]
        public IActionResult Index()
        {
            var VM = new BalanceVM
            {
                StDate = DateTime.Now,
                EnDate = DateTime.Now
            };
            return View(VM);
        }
        [HttpPost]
        public async Task<IActionResult> GetBalance(BalanceVM BalanceVM)
        {
            List<BalanceDTO> Capital = new List<BalanceDTO>();
            List<BalanceDTO> Liability = new List<BalanceDTO>();
            List<BalanceDTO> Assets = new List<BalanceDTO>();
            if (ModelState.IsValid)
            {
                Assets = await (from Account in _db.ThirdLevel
                                where Account.AccountHeadId == 1
                                select new BalanceDTO
                                {
                                    ThirdLevelId = Account.Id,
                                    AccountName = Account.AccountTitle,
                                    TDr = _db.TranscationDetails.Where(c => c.ThirdLevelId.Equals(Account.Id) && c.TransDate >= BalanceVM.StDate && c.TransDate <= BalanceVM.EnDate).Select(c => c.Dr).Sum(),
                                    TCr = _db.TranscationDetails.Where(c => c.ThirdLevelId.Equals(Account.Id) && c.TransDate >= BalanceVM.StDate && c.TransDate <= BalanceVM.EnDate).Select(c => c.Cr).Sum(),
                                    HeadId = Account.AccountHeadId
                                }).OrderBy(c=>c.ThirdLevelId).ToListAsync();
                Liability = await (from Account in _db.ThirdLevel
                                where Account.AccountHeadId == 2
                                select new BalanceDTO
                                {
                                    ThirdLevelId = Account.Id,
                                    AccountName = Account.AccountTitle,
                                    TDr = _db.TranscationDetails.Where(c => c.ThirdLevelId.Equals(Account.Id) && c.TransDate >= BalanceVM.StDate && c.TransDate <= BalanceVM.EnDate).Select(c => c.Dr).Sum(),
                                    TCr = _db.TranscationDetails.Where(c => c.ThirdLevelId.Equals(Account.Id) && c.TransDate >= BalanceVM.StDate && c.TransDate <= BalanceVM.EnDate).Select(c => c.Cr).Sum(),
                                    HeadId = Account.AccountHeadId
                                }).OrderBy(c=>c.ThirdLevelId).ToListAsync();
                Capital = await (from Account in _db.ThirdLevel
                                where Account.AccountHeadId == 3
                                select new BalanceDTO
                                {
                                    ThirdLevelId = Account.Id,
                                    AccountName = Account.AccountTitle,
                                    TDr = _db.TranscationDetails.Where(c => c.ThirdLevelId.Equals(Account.Id) && c.TransDate >= BalanceVM.StDate && c.TransDate <= BalanceVM.EnDate).Select(c => c.Dr).Sum(),
                                    TCr = _db.TranscationDetails.Where(c => c.ThirdLevelId.Equals(Account.Id) && c.TransDate >= BalanceVM.StDate && c.TransDate <= BalanceVM.EnDate).Select(c => c.Cr).Sum(),
                                    HeadId = Account.AccountHeadId
                                }).OrderBy(c=>c.ThirdLevelId).ToListAsync();
            }
            //
            var VM = new BalanceVM
            {
                StDate = BalanceVM.StDate,
                EnDate = BalanceVM.EnDate,
                Capital = Capital,
                Liability = Liability,
                Assets = Assets,
            };
            return View(VM);
        }
    }
}
