using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using MyEcommerceBook.Models;

namespace MyEcommerceBook.Controllers
{
    public class CategoryController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();
        // GET: Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryVM ctg)
        {
            if (ModelState.IsValid)
            {
                Category c = new Category
                {
                    Name = ctg.Name 
                
                };
                    db.Categories.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View("Create",ctg);

        }
                
            
        




        // EDIT: Product


        public ActionResult Edit(int id)
        {
            Category p = db.Categories.Find(id);

            CategoryVM categoryVM = new CategoryVM { 
              Name=p.Name,
              CategoryId=p.CategoryId
              
            };
            
            //ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierId", "CompanyName");
            //ViewBag.categoryList = new SelectList(db.Categories, "CategoryId", "Name");
            //ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryId", "Name");
            return View(categoryVM);
        }

        [HttpPost]

        public ActionResult Edit(CategoryVM pvm)
        {

            if (ModelState.IsValid)
            {
                //string filePath = pvm.PicturePath;

                //filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName));
                //pvm.Picture.SaveAs(Server.MapPath(filePath));
                Category p = db.Categories.Find(pvm.CategoryId);
               
                p.Name = pvm.Name;
                p.Description = pvm.Description;
                p.CategoryId = pvm.CategoryId;            
                db.SaveChanges();
                return RedirectToAction("Index");


            }
          
            return View("Edit",pvm);
        }


        // DETAILS: Product
        public ActionResult Details(int id)
        {
            Category p = db.Categories.Find(id);

            //p.ca
            //ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            //ViewBag.categoryList = new SelectList(db.Categories, "CategoryID", "Name");
            //ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryID", "Name");
            return View(p);
        }

        [HttpPost]

        public ActionResult Details(Category pvm)
        {

            Category p = db.Categories.Find(pvm.CategoryId);
            p.Name=pvm.Name;
            p.Description=pvm.Description;
            p.IsActive = true;
            db.SaveChanges();
            return RedirectToAction("Index");
          
            //return PartialView(pvm);
        }


        // DELETE: Product

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            Category product = db.Categories.Find(id);
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
            Category c = db.Categories.Find(id);
           // string file_name = product.PicturePath;
            //string path = Server.MapPath(file_name);
            //FileInfo file = new FileInfo(path);
            //if (file.Exists)
            //{
            //    file.Delete();
            //}
            db.Categories.Remove(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
