using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEcommerceBook.Models;

namespace MyEcommerceBook.Controllers
{
    public class SubCategoryController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();
        // GET: SubCategory
        public ActionResult Index()
        { 
            //var data =(from s in db.SubCategories join c in db.Categories on s.CategoryId equals c.CategoryId select s ).ToList();
            return View(db.SubCategories.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SubCategoryVM sctg)
        {
            if (ModelState.IsValid)
            {
                SubCategory subCategory = new SubCategory { 
                CategoryId= sctg.CategoryId,
                Name = sctg.Name
                };
                db.SubCategories.Add(subCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.supplierList = new SelectList(db.Categories, "CategoryId", "Name");
            return View("Create",sctg);
            //return PartialView("_Error");
        }


        // EDIT: Product


        public ActionResult Edit(int id)
        {
            SubCategory p = db.SubCategories.Find(id);
          
                SubCategoryVM subCategoryVM = new SubCategoryVM
                {
                    CategoryId = p.CategoryId,
                    Name = p.Name,
                    SubCategoryId= p.CategoryId
                };
               
               
          
            
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryId", "Name");
            //ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierId", "CompanyName");
            //ViewBag.categoryList = new SelectList(db.Categories, "CategoryId", "Name");
            //ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryId", "Name");
            return View(subCategoryVM);
        }

        [HttpPost]

        public ActionResult Edit(SubCategoryVM pvm)
        {

            if (ModelState.IsValid)
            {
                //string filePath = pvm.PicturePath;

                //filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName));
                //pvm.Picture.SaveAs(Server.MapPath(filePath));
                SubCategory p = db.SubCategories.Find(pvm.SubCategoryId);

                p.Name = pvm.Name;
                p.Description = pvm.Description;
                p.CategoryId = pvm.CategoryId;
                p.SubCategoryId = pvm.SubCategoryId;
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryId", "Name");
            return View("Edit",pvm);
        }


        // DETAILS: Product
        public ActionResult Details(int id)
        {
            SubCategory p = db.SubCategories.Find(id);

            //p.ca
            //ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            //ViewBag.categoryList = new SelectList(db.Categories, "CategoryID", "Name");
            //ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryID", "Name");
            return View(p);
        }

        [HttpPost]

        public ActionResult Details(SubCategory pvm)
        {

            SubCategory p = db.SubCategories.Find(pvm.CategoryId);
            p.Name = pvm.Name;
            p.Description = pvm.Description;
            p.IsActive = true;
            //db.SaveChanges();
            //return RedirectToAction("Index");

            return View(p);
        }


        // DELETE: Product

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            SubCategory product = db.SubCategories.Find(id);
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
            SubCategory c = db.SubCategories.Find(id);
            // string file_name = product.PicturePath;
            //string path = Server.MapPath(file_name);
            //FileInfo file = new FileInfo(path);
            //if (file.Exists)
            //{
            //    file.Delete();
            //}
            db.SubCategories.Remove(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
