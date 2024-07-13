using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using _2001216310_NguyenThanhVi.Models;
using Microsoft.AspNetCore.Http;
using _2001216310_NguyenThanhVi.ViewModel;
using _2001216310_NguyenThanhVi.Identity;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.IO;
using _2001216310_NguyenThanhVi.Identity;
using Microsoft.Owin.BuilderProperties;
using System.Net.PeerToPeer;
using System.Data.Entity.Validation;


namespace _2001216310_NguyenThanhVi.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        MyDB db = new MyDB();
        public ActionResult Index()
        {
            return View("ShowAccount");
        }
        public ActionResult ShowAccount(string search = "", string type = "", int page = 1, string column="")
        {
            List<Account> lst = db.Accounts.ToList();
            if(string.IsNullOrEmpty(search) && string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "UserName":
                        lst = lst.Where(e => e.UserName.Contains(search)).ToList(); break;
                    case "Name":
                        lst = lst.Where(e => e.Name.Contains(search)).ToList(); break;
                    case "Email":
                        lst = lst.Where(e => e.Email.Contains(search)).ToList(); break;
                    case "Phone":
                        lst = lst.Where(e => e.Phone.Contains(search)).ToList(); break;
                    case "Address":
                        lst = lst.Where(e => e.Address.Contains(search)).ToList(); break;
                }
            }
            switch (column)
            {
                case "Id":
                    lst = lst.OrderBy(e => e.Id).ToList(); break;
                case "Name":
                    lst = lst.OrderBy(e => e.Name).ToList(); break;
                case "UserName":
                    lst = lst.OrderBy(e => e.UserName).ToList(); break;
                default:
                    break;
            }
            int NoEmpInPage = 10;
            int NoPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count) / Convert.ToDouble(NoEmpInPage)));
            int NoEmpForSkip = (page - 1) * NoEmpInPage;
            lst = lst.Skip(NoEmpForSkip).Take(NoEmpInPage).ToList();
            ViewBag.Page = page;
            ViewBag.NoPage = NoPage;
            return View(lst);
        }
        public ActionResult Detail(int id)
        {
            ViewBag.brand = db.Brands.ToList();
            Account acc = db.Accounts.Where(e => e.Id.Equals(id)).FirstOrDefault();
            return View(acc);
        }
        public ActionResult Delete(int id)
        {
            Account acc = db.Accounts.Where(e => e.Id.Equals(id)).FirstOrDefault();
            db.Accounts.Remove(acc);
            db.SaveChanges();
            return RedirectToAction("Index", "Account");
        }
        public ActionResult Fix(int id)
        {
            Account acc = db.Accounts.Where(e => e.Id.Equals(id)).FirstOrDefault();
            ViewBag.brand = db.Brands.ToList();
            ViewBag.store = db.Stores.ToList();
            return View(acc);
        }
        [HttpPost]
        public ActionResult Fix(int id, Account a, int Role)
        {
            Account acc = db.Accounts.Where(e => e.Id.Equals(id)).FirstOrDefault();
            if (acc != null)
            {
                acc.Address = a.Address;
                acc.UserName = a.UserName;
                if(a.Password != null)
                {
                    acc.Password = Crypto.HashPassword(a.Password);
                }
                acc.Email = a.Email;
                acc.Phone = a.Phone;
                acc.Name = a.Name;
                if (Role == 1)
                {
                    acc.IsGuest = true;
                    acc.IsAdmin = false;
                }else if (Role == 2)
                {
                    acc.IsAdmin = true;
                    acc.IsGuest = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Account");
            }
            else
            {
                ViewBag.brand = db.Brands.ToList();
                ViewBag.store = db.Stores.ToList();
                return View(acc);
            }
        }
        public ActionResult Create()
        {
            ViewBag.brand = db.Brands.ToList();
            ViewBag.store = db.Stores.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(RegisterVM rm)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new AppDbContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);
                var passwdHash = Crypto.HashPassword(rm.Password);
                var user = new AppUser()
                {
                    Email = rm.Email,
                    UserName = rm.UserName,
                    UsName = rm.Name,
                    Name = rm.Name,
                    PasswordHash = passwdHash,
                    Address = rm.Address,
                    Phone = rm.Phone
                };
                Account m = new Account();
                m.UserName = rm.UserName;
                m.Email = rm.Email;
                m.Phone = rm.Phone;
                m.Password = passwdHash;
                m.Address = rm.Address;
                m.Name = rm.Name;
                m.Islocked = false;
                m.IsAdmin = false;
                m.IsGuest = true;
                db.Accounts.Add(m);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                }
                IdentityResult identityResult = userManager.Create(user);
                if (identityResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                    //var au=HttpContext.GetOwinContext().Authentication;
                    //var userIdentity=userManager.CreateIdentity(user,DefaultAuthenticationTypes.ApplicationCookie);
                    //au.SignIn(new AuthenticationProperties(), userIdentity);
                }
                return RedirectToAction("Index", "Account");
            }
            else
            {
                ModelState.AddModelError("New Error", "Dữ liệu trống !!");
                return View();
            }
        }
    }
}