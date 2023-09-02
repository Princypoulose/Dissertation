using Microsoft.AspNetCore.Mvc;
using MyEcommerceBook.Models;
using System.Diagnostics;

namespace MyEcommerceBook.Controllers
{
    public class HomeController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.MenProduct = db.Products.Where(x => x.Category.Name.Equals("Men's Fashion")).ToList();
            ViewBag.WomenProduct = db.Products.Where(x => x.Category.Name.Equals("Women's Fashion")).ToList();
            ViewBag.AccessoriesProduct = db.Products.Where(x => x.Category.Name.Equals("Electronic Accessories")).ToList();
            ViewBag.ElectronicsProduct = db.Products.Where(x => x.Category.Name.Equals("Electronic Devices")).ToList();
            ViewBag.Slider = db.GenMainSliders.ToList();
            ViewBag.PromoRight = db.GenPromoRights.ToList();

           
               // TempShpData.items = new List<OrderDetail>();
        
            var data = new List<OrderDetail>();

            ViewBag.cartBox = data.Count == 0 ? null : data;
            ViewBag.NoOfItem = data.Count();
            int? SubTotal = Convert.ToInt32(data.Sum(x => x.TotalAmount));
            ViewBag.Total = SubTotal;

            int Discount = 0;
            ViewBag.SubTotal = SubTotal;
            ViewBag.Discount = Discount;
            ViewBag.TotalAmount = SubTotal - Discount;

            ViewBag.WlItemsNo = db.Wishlists.Where(x => x.CustomerId == 1).ToList().Count();

            return View();
        }

    }
}