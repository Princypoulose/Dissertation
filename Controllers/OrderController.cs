using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEcommerceBook.Models;

namespace MyEcommerceBook.Controllers
{
    public class OrderController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();
        // GET: Order
        public ActionResult Index()
        {
            return View(db.Orders.OrderBy(x => x.OrderId).ToList());
        }
        public ActionResult Details(int id)
        {
            Order ord = db.Orders.Include(w => w.Customer).Include(w => w.Shipping).Include(w => w.Payment).Where(x => x.OrderId == id).FirstOrDefault();
            //  var wishlistProducts = db.Wishlists.Include(w => w.Customer).Include(w => w.Product).Where(x => x.CustomerId == TempShpData.UserID).ToList();

            var Ord_details = db.OrderDetails.Include(w => w.Product).Include(w => w.Order).Where(x => x.OrderId == id).ToList();
            var tuple = new Tuple<Order, IEnumerable<OrderDetail>>(ord, Ord_details);

            double SumAmount = Convert.ToDouble(Ord_details.Sum(x => x.TotalAmount));
            ViewBag.TotalItems = Ord_details.Sum(x => x.Quantity);
            ViewBag.Discount = 0;
            ViewBag.TAmount = SumAmount - 0;
            ViewBag.Amount = SumAmount;
            return View(tuple);

        }
    }
    }
