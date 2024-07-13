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
using System.Data.Entity.Validation;


namespace _2001216310_NguyenThanhVi.Areas.Customer.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        MyDB db = new MyDB();
        public ActionResult CreateAccount()
        {
            ViewBag.brand = db.Brands.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(RegisterVM rm)
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
                    UsName=rm.Name,
                    Name = rm.Name,
                    PasswordHash = passwdHash,
                    Address = rm.Address,
                    Phone = rm.Phone
                };
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
                IdentityResult identityResult =userManager.Create(user);
                if (identityResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                    //var au=HttpContext.GetOwinContext().Authentication;
                    //var userIdentity=userManager.CreateIdentity(user,DefaultAuthenticationTypes.ApplicationCookie);
                    //au.SignIn(new AuthenticationProperties(), userIdentity);
                }
                return RedirectToAction("MainOfProduct", "Product", new {sc = true });
            }
            else
            {
                ModelState.AddModelError("New Error", "Dữ liệu trống !!");
                return View();
            }
        }
        public ActionResult Login()
        {
            ViewBag.brand = db.Brands.ToList();
            return View();
        }
        [HttpPost]

       public ActionResult Login(LoginVM lg)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new AppDbContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);
                var user = userManager.Find(lg.UserName, lg.Password);
                if (user != null)
                {
                    var au = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    au.SignIn(new AuthenticationProperties(), userIdentity);
                    if (userManager.IsInRole(user.Id, "Admin"))
                    {
                        return RedirectToAction("MainOfProduct", "Product", new { area = "Admin", sc = true });
                    }
                    else
                    {
                        return RedirectToAction("MainOfProduct", "Product", new { sc = true });
                    }
                }
            }
            ModelState.AddModelError("New Error", "Dữ liệu trống !!");
            return View();
        }
        public ActionResult Logout()
        {
            var au = HttpContext.GetOwinContext().Authentication;
            au.SignOut();
            return RedirectToAction("MainOfProduct", "Product", new { sc = true });
        }
    }
}