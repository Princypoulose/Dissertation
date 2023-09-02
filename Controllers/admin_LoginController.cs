using Microsoft.AspNetCore.Mvc;
using MyEcommerceBook.Models;

namespace MyEcommerceBook.Controllers
{
    public class admin_LoginController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();
        // GET: admin_Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminLogin login)
        {
            if (ModelState.IsValid || !ModelState.IsValid)
            {
                var model = (from m in db.AdminLogins
                             where m.UserName == login.UserName && m.Password == login.Password
                             select m).Any();

                if (model)
                {
                    var loginInfo = db.AdminLogins.Where(x => x.UserName == login.UserName && x.Password == login.Password).FirstOrDefault();

                    HttpContext.Session.SetString("username", loginInfo.UserName); 
                    TemData.EmpID = loginInfo.EmpId;
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return RedirectToAction("Index", "admin_Login");
            //return View();
        }
        // Logout Server Code
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "admin_Login");
        }
    }
}
