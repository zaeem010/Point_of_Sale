using IMS.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Areas.Pos.Controllers
{
    [Area("Pos")]
    public class SaleController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("/Pos/Sale/Screen")]
        public IActionResult Salescreen()
        {
            return View();
        }
    }
}
