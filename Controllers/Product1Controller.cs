using Microsoft.AspNetCore.Mvc;
using MyEcommerceBook.Models;
using PagedList;

namespace MyEcommerceBook.Controllers
{
    public class Product1Controller : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();


        public ActionResult Index()
        {
            ViewBag.Categories = db.Categories.Select(x => x.Name).ToList();

            //ViewBag.TopRatedProducts = TopSoldProducts();
            ViewBag.RecentViewsProducts = RecentViewProducts();

            return View("Products");
        }

        //TOP SOLD PRODUCTS
        //public List<TopSoldProduct> TopSoldProducts()
        //{
        //    var prodList = (from prod in db.OrderDetails
        //                    select new { prod.ProductID, prod.Quantity } into p
        //                    group p by p.ProductID into g
        //                    select new
        //                    {
        //                        pID = g.Key,
        //                        sold = g.Sum(x => x.Quantity)
        //                    }).OrderByDescending(y => y.sold).Take(3).ToList();



        //    List<TopSoldProduct> topSoldProds = new List<TopSoldProduct>();

        //    for (int i = 0; i < 3; i++)
        //    {
        //        topSoldProds.Add(new TopSoldProduct()
        //        {
        //            product = db.Products.Find(prodList[i].pID),
        //            CountSold = Convert.ToInt32(prodList[i].sold)
        //        });
        //    }
        //    return topSoldProds;
        //}
       // RECENT VIEWS PRODUCTS
        public IEnumerable<Product> RecentViewProducts()
        {
            if (TempShpData.UserID > 0)
            {
                var top3Products = (from recent in db.RecentlyViews
                                    where recent.CustomerId == TempShpData.UserID
                                    orderby recent.ViewDate descending
                                    select recent.ProductId).ToList().Take(3);

                var recentViewProd = db.Products.Where(x => top3Products.Contains(x.ProductId));
                return recentViewProd;
            }
            else
            {
                var prod = (from p in db.Products
                            select p).OrderByDescending(x => x.UnitPrice).Take(3).ToList();
                return prod;
            }
        }

        //ADD TO CART
        public ActionResult AddToCart(int id)
        {
            OrderDetail OD = new OrderDetail();
            OD.ProductId = id;
            int Qty = 1;
            decimal price = db.Products.Find(id).UnitPrice;
            OD.Quantity = Qty;
            OD.UnitPrice = price;
            OD.TotalAmount = Qty * price;
            OD.Product = db.Products.Find(id);

            if (TempShpData.items == null)
            {
                TempShpData.items = new List<OrderDetail>();
            }
            TempShpData.items.Add(OD);
            AddRecentViewProduct(id);
            return Redirect("/Product1/ViewDetails/" + Convert.ToString(id));

        }

        //VIEW DETAILS
        public ActionResult ViewDetails(int id)
        {
            var prod = db.Products.Find(id);
            var reviews = db.Reviews.Where(x => x.ProductId == id).ToList();
            ViewBag.Reviews = reviews;
            ViewBag.TotalReviews = reviews.Count();
            ViewBag.RelatedProducts = db.Products.Where(y => y.CategoryId == prod.CategoryId).ToList();
            AddRecentViewProduct(id);

            var ratedProd = db.Reviews.Where(x => x.ProductId == id).ToList();
            int count = ratedProd.Count();
            int TotalRate = ratedProd.Sum(x => x.Rate).GetValueOrDefault();
            ViewBag.AvgRate = TotalRate > 0 ? TotalRate / count : 0;

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
            //ViewBag.

            //this.GetDefaultData();
            return View(prod);
        }

        //WISHLIST
        public ActionResult WishList(int id)
        {

            Wishlist wl = new Wishlist();
            wl.ProductId = id;
            wl.CustomerId = 2;

            db.Wishlists.Add(wl);
            db.SaveChanges();
            AddRecentViewProduct(id);
            ViewBag.WlItemsNo = db.Wishlists.Where(x => x.CustomerId == 2).ToList().Count();
            //if (TempData["returnURL"].ToString() == "/")
            //{
            ////return RedirectToAction("Index", "Home");
            // }
            //return Redirect(TempData["returnURL"].ToString());
            return Redirect("/Product1/ViewDetails/" + Convert.ToString(id));
        }

        //ADD RECENT VIEWS PRODUCT IN DB
        public void AddRecentViewProduct(int pid)
        {
            if (TempShpData.UserID > 0)
            {
                RecentlyView Rv = new RecentlyView();
                Rv.CustomerId = TempShpData.UserID;
                Rv.ProductId = pid;
                Rv.ViewDate = DateTime.Now;
                db.RecentlyViews.Add(Rv);
                db.SaveChanges();
            }
        }

        //ADD REVIEWS ABOUT PRODUCT
        public ActionResult AddReview(int productID, FormCollection getReview)
        {

            Review r = new Review();
            r.CustomerId = TempShpData.UserID;
            r.ProductId = productID;
            r.Name = getReview["name"];
            r.Email = getReview["email"];
            r.Review1 = getReview["message"];
            r.Rate = Convert.ToInt32(getReview["rate"]);
            r.DateTime = DateTime.Now;

            db.Reviews.Add(r);
            db.SaveChanges();
            return RedirectToAction("ViewDetails/" + productID + "");

        }


        public ActionResult Products(int subCatID)
        {
            ViewBag.Categories = db.Categories.Select(x => x.Name).ToList();
            var prods = db.Products.Where(y => y.SubCategoryId == subCatID).ToList();
            return View(prods);
        }

        //GET PRODUCTS BY CATEGORY
        public ActionResult GetProductsByCategory(string categoryName, int? page)
        {
            ViewBag.Categories = db.Categories.Select(x => x.Name).ToList();
            ViewBag.SubCategories = db.SubCategories.Select(x => x.Name).ToList();
            //ViewBag.TopRatedProducts = TopSoldProducts();

            ViewBag.RecentViewsProducts = RecentViewProducts();

            var prods = db.Products.Where(x => x.SubCategory.Name == categoryName).ToList();
            return View("Products", prods.ToPagedList(page ?? 1, 9));

        }



        //SEARCH BAR RESULT
        public ActionResult Search(string product, int? page)
        {
            ViewBag.SubCategories = db.SubCategories.Select(x => x.Name).ToList();
            //ViewBag.TopRatedProducts = TopSoldProducts();

            ViewBag.RecentViewsProducts = RecentViewProducts();

            List<Product> products;
            if (!string.IsNullOrEmpty(product))
            {
                products = db.Products.Where(x => x.Name.StartsWith(product)).ToList();
            }
            else
            {
                products = db.Products.ToList();
            }
            return View("Products", products.ToPagedList(page ?? 1, 6));
        }

        public JsonResult GetProducts(string term)
        {
            List<string> prodNames = db.Products.Where(x => x.Name.StartsWith(term)).Select(y => y.Name).ToList();
            return Json(prodNames, System.Web.Mvc.JsonRequestBehavior.AllowGet);

        }

        //  Filter Product By Price
        public ActionResult FilterByPrice(int minPrice, int maxPrice, int? page)
        {
            ViewBag.SubCategories = db.SubCategories.Select(x => x.Name).ToList();
            //ViewBag.TopRatedProducts = TopSoldProducts();

            ViewBag.RecentViewsProducts = RecentViewProducts();
            ViewBag.filterByPrice = true;
            var filterProducts = db.Products.Where(x => x.UnitPrice >= minPrice && x.UnitPrice <= maxPrice).ToList();
            return View("Products", filterProducts.ToPagedList(page ?? 1, 9));
        }



    }
}
