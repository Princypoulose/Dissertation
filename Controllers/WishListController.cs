using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEcommerceBook.Models;

namespace MyEcommerceBook.Controllers
{
    public class WishListController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();

        // GET: WishList
        public ActionResult Index()
        {
            // this.GetDefaultData();
            if (TempShpData.items == null)
            {
                TempShpData.items = new List<OrderDetail>();
            }
            var data = TempShpData.items.ToList();
            ViewBag.cartBox = data.Count == 0 ? null : data;
            ViewBag.NoOfItem = data.Count();
            int? SubTotal = Convert.ToInt32(data.Sum(x => x.TotalAmount));

            ViewBag.Total = SubTotal;
            int Discount = 0;
            ViewBag.SubTotal = SubTotal;
            ViewBag.Discount = Discount;
            ViewBag.TotalAmount = SubTotal - Discount;

            ViewBag.WlItemsNo = db.Wishlists.Where(x => x.CustomerId == TempShpData.UserID).ToList().Count();
            //using (var context = new MyEcommerceDbContext())
            //{
            //    var blogs = context.Wishlists
            //        .Include(blog => blog.)
            //        .ToList();
            //}
            var wishlistProducts = db.Wishlists.Include(w=>w.Customer).Include(w=>w.Product). Where(x => x.CustomerId == TempShpData.UserID).ToList();
            return View(wishlistProducts);
        }

        //REMOVE ITEM FROM WISHLIST
        public ActionResult Remove(int id)
        {
            db.Wishlists.Remove(db.Wishlists.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        //ADD TO CART WISHLIST
        public ActionResult AddToCart(int id)
        {
            OrderDetail OD = new OrderDetail();

            int pid = db.Wishlists.Find(id).ProductId;
            OD.ProductId = pid;
            int Qty = 1;
            decimal price = db.Products.Find(pid).UnitPrice;
            OD.Quantity = Qty;
            OD.UnitPrice = price;
            OD.TotalAmount = Qty * price;
            OD.Product = db.Products.Find(pid);

            if (TempShpData.items == null)
            {
                TempShpData.items = new List<OrderDetail>();
            }
            TempShpData.items.Add(OD);

            db.Wishlists.Remove(db.Wishlists.Find(id));
            db.SaveChanges();

            return Redirect("/Product1/ViewDetails/" + Convert.ToString(pid));

        }
    }
}
