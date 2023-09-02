using Microsoft.AspNetCore.Mvc;
using MyEcommerceBook.Models;

namespace MyEcommerceBook.Controllers
{
    public class DashboardController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();
        public ActionResult Index()
        {

            ViewBag.latestOrders = db.Orders.OrderBy(x => x.OrderId).Take(10).ToList();
            //ViewBag.NewOrders = db.Orders.Where(a => a.DIspatched == false && a.Shipped == false && a.Deliver == false).Count();
            //ViewBag.DispatchedOrders = db.Orders.Where(a => a.DIspatched == true && a.Shipped == false && a.Deliver == false).Count();
            //ViewBag.ShippedOrders = db.Orders.Where(a => a.DIspatched == true && a.Shipped == true && a.Deliver == false).Count();
            //ViewBag.DeliveredOrders = db.Orders.Where(a => a.DIspatched == true && a.Shipped == true && a.Deliver == true).Count();
            return View();
        }
    }
}
