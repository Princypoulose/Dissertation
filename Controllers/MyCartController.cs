using Microsoft.AspNetCore.Mvc;
using MyEcommerceBook.Models;

namespace MyEcommerceBook.Controllers
{
    public class MyCartController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();

        // GET: MyCart
        public ActionResult Index()
        {
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
           // var data = this.GetDefaultData();

            return View(data);

        }

        public ActionResult Remove(int id)
        {
            TempShpData.items.RemoveAll(x => x.ProductId == id);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult ProcedToCheckout(IFormCollection formcoll)
        {
            var a = TempShpData.items.ToList();
            for (int i = 0; i < formcoll.Count / 2; i++)
            {

                int pID = Convert.ToInt32(formcoll["shcartID-" + i + ""]);
                var ODetails = TempShpData.items.FirstOrDefault(x => x.ProductId == pID);


                int qty = Convert.ToInt32(formcoll["Qty-" + i + ""]);
                ODetails.Quantity = qty;
                ODetails.UnitPrice = ODetails.UnitPrice;
                ODetails.TotalAmount = qty * ODetails.UnitPrice;
                TempShpData.items.RemoveAll(x => x.ProductId == pID);

                if (TempShpData.items == null)
                {
                    TempShpData.items = new List<OrderDetail>();
                }
                TempShpData.items.Add(ODetails);

            }

            return RedirectToAction("Index", "CheckOut");
        }

    }
}
