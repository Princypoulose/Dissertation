using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyEcommerceBook.Models;
using static Azure.Core.HttpHeader;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection;
using System.Web.Helpers;
//using System.Web.Mvc;

namespace MyEcommerceBook.Controllers
{
    public class CustomerController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // CREATE: Customer

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerVM cvm)
        {
            ModelState.Remove(nameof(cvm.status));
            ModelState.Remove(nameof(cvm.Notes));
            ModelState.Remove(nameof(cvm.PicturePath));
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
            if (ModelState.IsValid)
            {
                //if (cvm.Picture != null)
                //{
                    //string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(cvm.Picture.FileName));
                    //cvm.Picture.SaveAs(Server.MapPath(filePath));

                    Customer c = new Customer
                    {
                        CustomerId = cvm.CustomerID,
                        FirstName = cvm.First_Name,
                        LastName = cvm.Last_Name,
                        UserName = cvm.UserName,
                        Password = cvm.Password,
                        Gender = cvm.Gender,
                        DateofBirth = cvm.DateofBirth,
                        Country = cvm.Country,
                        City = cvm.City,
                        PostalCode = cvm.PostalCode,
                        Email = cvm.Email,
                        Phone = cvm.Phone,
                        Address = cvm.Address,
                        PicturePath = "",
                        Status = cvm.status,
                        LastLogin = cvm.LastLogin,
                        Created = cvm.Created,
                        Notes = cvm.Notes
                    };
                    db.Customers.Add(c);
                    db.SaveChanges();
                    return RedirectToAction("Index");
               // }
            }
            return View("Create",cvm);
        }


        // EDIT: Customer
        public ActionResult Edit(int id)
        {
            Customer cus = db.Customers.Find(id);
            CustomerVM cvm = new CustomerVM
            {
                CustomerID = cus.CustomerId,
                First_Name = cus.FirstName,
                Last_Name = cus.LastName,
                UserName = cus.UserName,
                Password = cus.Password,
                Gender = cus.Gender,
                DateofBirth = cus.DateofBirth,
                Country = cus.Country,
                City = cus.City,
                PostalCode = cus.PostalCode,
                Email = cus.Email,
                Phone = cus.Phone,
                Address = cus.Address,
                PicturePath = cus.PicturePath,
                status = cus.Status,
                LastLogin = cus.LastLogin,
                Created = cus.Created,
                Notes = cus.Notes

            };

            return View(cvm);
        }

        [HttpPost]
        public ActionResult Edit(CustomerVM cust)
        {
            ModelState.Remove(nameof(cust.status));
            ModelState.Remove(nameof(cust.Notes));
            ModelState.Remove(nameof(cust.PicturePath));
            foreach (var key in ModelState.Keys)
            {
                var error = ModelState[key].Errors.FirstOrDefault();
                if (error != null)
                {
                    var errorMessage = error.ErrorMessage;
                    var propertyName = key; // This is the property name causing the error
                                            // Log or do something with the error and property name
                }
            }
            if (ModelState.IsValid)
            {
                //string filePath = cvm.PicturePath;
                Customer cus = db.Customers.Find(cust.CustomerID);

                //if (cvm.Picture != null)
                //{
                //    filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(cvm.Picture.FileName));
                //    cvm.Picture.SaveAs(Server.MapPath(filePath));

                //Customer c = new Customer
                //{
                //    CustomerId = cvm.CustomerID,
                //    FirstName = cvm.First_Name,
                //    LastName = cvm.Last_Name,
                //    UserName = cvm.UserName,
                //    Password = cvm.Password,
                //    Gender = cvm.Gender,
                //    DateofBirth = cvm.DateofBirth,
                //    Country = cvm.Country,
                //    City = cvm.City,
                //    PostalCode = cvm.PostalCode,
                //    Email = cvm.Email,
                //    Phone = cvm.Phone,
                //    Address = cvm.Address,
                //    PicturePath = filePath,
                //    Status = cvm.status,
                //    LastLogin = cvm.LastLogin,
                //    Created = cvm.Created,
                //    Notes = cvm.Notes
                //};
                //db.Customers.Add(c);
                cus.FirstName = cust.First_Name;
                cus.LastName = cust.Last_Name;
                cus.DateofBirth = cust.DateofBirth;
                cus.Email = cust.Email;
                cus.Phone = cust.Phone;
                cus.City = cust.City;
                cus.Country = cust.Country;
                cus.Address = cust.Address;
                cus.Gender = cust.Gender;
                cus.UserName = cust.UserName;
                cus.Password = cust.Password;
                cus.Notes = cust.Notes;
                cus.PostalCode = cust.PostalCode;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit",cust);
               // }
           // return PartialView(cus);
        }

        // DITELS: Customer

        public ActionResult Details(int id)
        {
            Customer cus = db.Customers.Find(id);

            CustomerVM cvm = new CustomerVM
            {
                CustomerID = cus.CustomerId,
                First_Name = cus.FirstName,
                Last_Name = cus.LastName,
                UserName = cus.UserName,
                Password = cus.Password,
                Gender = cus.Gender,
                DateofBirth = cus.DateofBirth,
                Country = cus.Country,
                City = cus.City,
                PostalCode = cus.PostalCode,
                Email = cus.Email,
                Phone = cus.Phone,
                Address = cus.Address,
                PicturePath = cus.PicturePath,
                status = cus.Status,
                LastLogin = cus.LastLogin,
                Created = cus.Created,
                Notes = cus.Notes

            };
            return View(cus);
        }

        [HttpPost]
        public ActionResult Details(CustomerVM cvm)
        {

           // if (ModelState.IsValid)
           // {
           //     string filePath = cvm.PicturePath;
           //     //if (cvm.Picture != null)
           //     //{
           //     //    filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(cvm.Picture.FileName));
           //     //    cvm.Picture.SaveAs(Server.MapPath(filePath));

           //         Customer c = new Customer
           //         {
           //             CustomerId = cvm.CustomerId,
           //             FirstName = cvm.First_Name,
           //             LastName = cvm.Last_Name,
           //             UserName = cvm.UserName,
           //             Password = cvm.Password,
           //             Gender = cvm.Gender,
           //             DateofBirth = cvm.DateofBirth,
           //             Country = cvm.Country,
           //             City = cvm.City,
           //             PostalCode = cvm.PostalCode,
           //             Email = cvm.Email,
           //             Phone = cvm.Phone,
           //             Address = cvm.Address,
           //             PicturePath = filePath,
           //             Status = cvm.status,
           //             LastLogin = cvm.LastLogin,
           //             Created = cvm.Created,
           //             Notes = cvm.Notes
           //         };
           //         db.Customers.Add(c);
           //         db.SaveChanges();
           //         return RedirectToAction("Index");
           //     }
           //     else
           //     {
           //         Customer c = new Customer
           //         {
           //             CustomerId = cvm.CustomerId,
           //             FirstName = cvm.FirstName,
           //             LastName = cvm.LastName,
           //             UserName = cvm.UserName,
           //             Password = cvm.Password,
           //             Gender = cvm.Gender,
           //             DateofBirth = cvm.DateofBirth,
           //             Country = cvm.Country,
           //             City = cvm.City,
           //             PostalCode = cvm.PostalCode,
           //             Email = cvm.Email,
           //             Phone = cvm.Phone,
           //             Address = cvm.Address,
           //             PicturePath = "",
           //             Status = cvm.Status,
           //             LastLogin = cvm.LastLogin,
           //             Created = cvm.Created,
           //             Notes = cvm.Notes
           //         };
           //     db.Customers.Add(c);
           //         db.SaveChanges();
           //         return RedirectToAction("Index");
           //     }
           //// }
            return PartialView(cvm);
        }

        // DELETE: Customer

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return new StatusCodeResult(200);
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Customer customer = db.Customers.Find(id);
            string file_name = customer.PicturePath;
           // string path = Server.MapPath(file_name);
            //FileInfo file = new FileInfo(path);
            //if (file.Exists)
            //{
            //    file.Delete();
            //}
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
