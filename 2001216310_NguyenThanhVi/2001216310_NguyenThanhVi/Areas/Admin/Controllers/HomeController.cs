using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2001216310_NguyenThanhVi.Models;

namespace _2001216310_NguyenThanhVi.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            MyDB db = new MyDB();
            ViewBag.brand=db.Brands.ToList();
            return View();
        }
    }
}