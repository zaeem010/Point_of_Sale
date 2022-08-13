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
    public class TrailBalanceController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TrailBalanceController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("/TrailBalance/Detail-Trail-Balance")]
        public IActionResult Index()
        {
            var VM = new TrailVM
            {
                StDate = DateTime.Now,
                EnDate = DateTime.Now
            };
            return View(VM);
        }
        [HttpPost]
        public async Task<IActionResult> GetTrail(TrailVM TrailVM)
        {
            List<TrailDTO> Balances = new List<TrailDTO>();
            if (ModelState.IsValid)
            {
                Balances = await (from Account in _db.ThirdLevel
                            select
                            new TrailDTO
                            {
                                ThirdlevelId = Account.Id,
                                AccountName = Account.AccountTitle,
                                ADr = Account.Dr,
                                ACr = Account.Cr,
                                TDr = _db.TranscationDetails.Where(c=>c.ThirdLevelId.Equals(Account.Id) && c.TransDate >= TrailVM.StDate && c.TransDate <= TrailVM.EnDate).Select(c=>c.Dr).Sum(),
                                TCr = _db.TranscationDetails.Where(c => c.ThirdLevelId.Equals(Account.Id) && c.TransDate >= TrailVM.StDate && c.TransDate <= TrailVM.EnDate).Select(c => c.Cr).Sum(),
                                HeadId = Account.AccountHeadId
                            }).OrderBy(c=>c.HeadId).ToListAsync();
            }
            //
            var VM = new TrailVM
            {
                StDate = TrailVM.StDate,
                EnDate = TrailVM.EnDate,
                Balances= Balances,
            };
            return View(VM);
        }
    }
}
