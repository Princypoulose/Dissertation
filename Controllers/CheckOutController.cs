using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEcommerceBook.Models;
//using System.Web.Mvc;

namespace MyEcommerceBook.Controllers
{
    public class CheckOutController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();
        // GET: CheckOut
        public ActionResult Index()
        {
            ViewBag.PayMethod = new SelectList(db.PaymentTypes, "PayTypeID", "TypeName");


            //var data = this.GetDefaultData();
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

            return View(data);
        }


        //PLACE ORDER--LAST STEP
        public ActionResult PlaceOrder(IFormCollection getCheckoutDetails)
        {

            int shpID = 1;
            if (db.ShippingDetails.Count() > 0)
            {
                shpID = db.ShippingDetails.Max(x => x.ShippingId) + 1;
            }
            int payID = 1;
            if (db.Payments.Count() > 0)
            {
                payID = db.Payments.Max(x => x.PaymentId) + 1;
            }
            int orderID = 1;
            if (db.Orders.Count() > 0)
            {
                orderID = db.Orders.Max(x => x.OrderId) + 1;
            }



            ShippingDetail shpDetails = new ShippingDetail();
            //shpDetails.ShippingId = shpID;
            shpDetails.FirstName = getCheckoutDetails["FirstName"];
            shpDetails.LastName = getCheckoutDetails["LastName"];
            shpDetails.Email = getCheckoutDetails["Email"];
            shpDetails.Mobile = getCheckoutDetails["Mobile"];
            shpDetails.Address = getCheckoutDetails["Address"];
            shpDetails.City = getCheckoutDetails["City"];
            shpDetails.PostCode = getCheckoutDetails["PostCode"];
            db.ShippingDetails.Add(shpDetails);
            db.SaveChanges();

            Payment pay = new Payment();
           // pay.PaymentId = payID;
            pay.Type = Convert.ToInt32(getCheckoutDetails["PayMethod"]);
            db.Payments.Add(pay);
            db.SaveChanges();

            Order o = new Order();
          //  o.OrderId = orderID;
            o.CustomerId = TempShpData.UserID;
            o.PaymentId = payID;
            o.ShippingId = shpID;
            o.Discount = Convert.ToInt32(getCheckoutDetails["discount"]);
            o.TotalAmount = Convert.ToInt32(Convert.ToInt32(TempShpData.items.Sum(x => x.TotalAmount)));
            o.IsCompleted = true;
            o.OrderDate = DateTime.Now;
            db.Orders.Add(o);
            db.SaveChanges();

            foreach (var OD in TempShpData.items)
            {
                OD.OrderId = orderID;
                OD.Order = db.Orders.Find(orderID);
                OD.Product = db.Products.Find(OD.ProductId);
                db.OrderDetails.Add(OD);
                db.SaveChanges();
            }


            return RedirectToAction("Index", "ThankYou");

        }
    }
}
