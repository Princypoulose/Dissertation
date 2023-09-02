using Microsoft.AspNetCore.Mvc;
using MyEcommerceBook.Models;

namespace MyEcommerceBook.Controllers
{
    public class ThankYouController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.cartBox = null;
            ViewBag.Total = null;
            ViewBag.NoOfItem = null;
            TempShpData.items = null;
            return View("Thankyou");
        }
    }
}
