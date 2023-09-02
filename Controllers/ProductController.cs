using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEcommerceBook.Models;
//using System.Web.Mvc;

namespace MyEcommerceBook.Controllers
{
    public class ProductController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();
        private readonly IWebHostEnvironment _webHostEnvironment;
        // GET: Product

        public ProductController(IWebHostEnvironment webHostEnvironment)
        { 
           _webHostEnvironment= webHostEnvironment;
        }
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }


        // CREATE: Product

        public ActionResult Create()
        {
            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierId", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductVM pvm)
        {
            //foreach (var key in ModelState.Keys)
            //{
            //    var error = ModelState[key].Errors.FirstOrDefault();
            //    if (error != null)
            //    {
            //        var errorMessage = error.ErrorMessage;
            //        var propertyName = key; // This is the property name causing the error
            //                                // Log or do something with the error and property name
            //    }
            //}

            ModelState.Remove(nameof(pvm.PicturePath));
            ModelState.Remove(nameof(pvm.Note));

            if (ModelState.IsValid)
            {
                //string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName));
                //pvm.Picture.SaveAs(Server.MapPath(filePath));
                if (pvm.Picture == null || pvm.Picture.Length == 0)
                {

                    ModelState.AddModelError("Picture", "Please select a valid image file.");
                    return RedirectToAction("Create", pvm);
                }

                byte[] imageData;

                using (var memoryStream = new MemoryStream())
                {
                    pvm.Picture.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray();
                }
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName);
                string filePath = Path.Combine("~/Images", uniqueFileName);

                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", uniqueFileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    pvm.Picture.CopyTo(fileStream);
                }

                Product p = new Product
                {
                    ProductId = pvm.ProductID,
                    Name = pvm.Name,
                    SupplierId = pvm.SupplierId,
                    CategoryId = pvm.CategoryId,
                    SubCategoryId = pvm.SubCategoryId,
                    UnitPrice = pvm.UnitPrice,
                    OldPrice = pvm.OldPrice,
                    Discount = pvm.Discount,
                    UnitInStock = pvm.UnitInStock,
                    ProductAvailable = pvm.ProductAvailable,
                    ShortDescription = pvm.ShortDescription,
                    Note = pvm.Note,
                    PicturePath = filePath
                };

                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierId", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryId", "Name");
            //foreach (var key in ModelState.Keys)
            //{
            //    var error = ModelState[key].Errors.FirstOrDefault();
            //    if (error != null)
            //    {
            //        var errorMessage = error.ErrorMessage;
            //        var propertyName = key; // This is the property name causing the error
            //                                // Log or do something with the error and property name
            //    }
            //}
            return View("Create", pvm);


        }




        // EDIT: Product


        public ActionResult Edit(int id)
        {
            Product p = db.Products.Find(id);
            ProductVM pvm = new ProductVM
            {
                ProductID = p.ProductId,
                Name = p.Name,
                SupplierId = p.SupplierId,
                CategoryId = p.CategoryId,
                SubCategoryId = p.SubCategoryId,
                UnitPrice = p.UnitPrice,
                OldPrice = p.OldPrice,
                Discount = p.Discount,
                UnitInStock = p.UnitInStock,
                ProductAvailable = p.ProductAvailable,
                ShortDescription = p.ShortDescription,
                Note = p.Note,
                PicturePath = p.PicturePath

            };

            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierId", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryId", "Name");
            return View(pvm);
        }

        [HttpPost]

        public ActionResult Edit(ProductVM pvm)
        {
            ModelState.Remove(nameof(pvm.Picture));
            ModelState.Remove(nameof(pvm.Note));

            if (ModelState.IsValid)
            { 
            string filePath = pvm.PicturePath;           
            if (pvm.Picture!=null)
            { 
            byte[] imageData;

            using (var memoryStream = new MemoryStream())
            {
                pvm.Picture.CopyTo(memoryStream);
                imageData = memoryStream.ToArray();
            }
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName);
            filePath = Path.Combine("~/Images", uniqueFileName);

            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", uniqueFileName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                pvm.Picture.CopyTo(fileStream);
            }
            }
           

            // string filePath = pvm.PicturePath;

            //filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName));
            //pvm.Picture.SaveAs(Server.MapPath(filePath));
            Product p = db.Products.Find(pvm.ProductID);
                    //Product p = new Product
                    //{
                    //    ProductID = pvm.ProductID,
                    //    Name = pvm.Name,
                    //    SupplierID = pvm.SupplierID,
                    //    CategoryID = pvm.CategoryID,
                    //    SubCategoryID = pvm.SubCategoryID,
                    //    UnitPrice = pvm.UnitPrice,
                    //    OldPrice = pvm.OldPrice,
                    //    Discount = pvm.Discount,
                    //    UnitInStock = pvm.UnitInStock,
                    //    ProductAvailable = pvm.ProductAvailable,
                    //    ShortDescription = pvm.ShortDescription,
                    //    Note = pvm.Note,
                    //    PicturePath = filePath
                    //};
                   // db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                   p.Name= pvm.Name;
                    p.SupplierId = pvm.SupplierId;
                    p.CategoryId=pvm.CategoryId;
                    p.SubCategoryId =  pvm.SubCategoryId;
                    p.UnitPrice = pvm.UnitPrice;
                    p.OldPrice = pvm.OldPrice;
                       p.Discount = pvm.Discount;
                    p.UnitInStock = pvm.UnitInStock;    
                    p.ProductAvailable = pvm.ProductAvailable;
                    p.ShortDescription= pvm.ShortDescription;  
                    p.Note  = pvm.Note;
                    p.PicturePath= filePath;
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierId", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryId", "Name");
            //foreach (var key in ModelState.Keys)
            //{
            //    var error = ModelState[key].Errors.FirstOrDefault();
            //    if (error != null)
            //    {
            //        var errorMessage = error.ErrorMessage;
            //        var propertyName = key; // This is the property name causing the error
            //                                // Log or do something with the error and property name
            //    }
            //}
            return View("Edit",pvm);



            //return PartialView(pvm);
        }


        // DETAILS: Product
        public ActionResult Details(int id)
        {
            Product p = db.Products.Find(id);

            ProductVM pvm = new ProductVM
            {
                ProductID = p.ProductId,
                Name = p.Name,
                SupplierId = p.SupplierId,
                CategoryId = p.CategoryId,
                SubCategoryId = p.SubCategoryId,
                UnitPrice = p.UnitPrice,
                OldPrice = p.OldPrice, 
                Discount = p.Discount,
                UnitInStock = p.UnitInStock,
                ProductAvailable = p.ProductAvailable,
                ShortDescription = p.ShortDescription,
                Note = p.Note,
                PicturePath = p.PicturePath

            };
            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierId", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryId", "Name");
            return View(pvm);
        }

        [HttpPost]

        public ActionResult Details(Product pvm)
        {

            //if (ModelState.IsValid)
            //{
            //    string filePath = pvm.PicturePath;
               
                   
                   
                   // db.Entry(p).State = System.Data.Entity.EntityState.Unchanged;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                //}
               
                    //db.Entry(p).State = System.Data.Entity.EntityState.Unchanged;
                    db.SaveChanges();
                    return RedirectToAction("Index");
             
            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryID", "Name");
            return PartialView(pvm);
        }


        // DELETE: Product

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return new StatusCodeResult(200);
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Product product = db.Products.Find(id);
            string file_name = product.PicturePath;
            //string path = Server.MapPath(file_name);
            //FileInfo file = new FileInfo(path);
            //if (file.Exists)
            //{
            //    file.Delete();
            //}
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
