using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2001216310_NguyenThanhVi.Models;

namespace _2001216310_NguyenThanhVi.Areas.Customer.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        MyDB db = new MyDB();
        public ActionResult MainOfProduct(int page = 1, string search = "", int brandsearch=0, string column = "", string price = "", string status = "", bool sc = true)
        {
            List<Product> lstPro = db.Products.ToList();
            if (brandsearch != 0)
            {
                lstPro=lstPro.Where(e=> e.BrandId == brandsearch).ToList();
            }
            switch (column)
            {
                case "Id":
                    lstPro = lstPro.OrderBy(e => e.Id).ToList(); break;
                case "Name":
                    lstPro = lstPro.OrderBy(e => e.Name).ToList(); break;
                case "Price":
                    lstPro = lstPro.OrderBy(e => e.Price).ToList(); break;
                case "Price2":
                    lstPro = lstPro.OrderByDescending(e => e.Price).ToList(); break;
                case "Type":
                    lstPro = lstPro.OrderBy(e => e.Type).ToList(); break;
                case "Status":
                    lstPro = lstPro.OrderBy(e => e.Status).ToList(); break;
                default:
                    break;
            }
            switch (status)
            {
                case "Have":
                    lstPro = lstPro.Where(e => e.Status == "Còn hàng").ToList(); break;
                case "None":
                    lstPro = lstPro.Where(e => e.Status == "Hết hàng").ToList(); break;
                default:
                    break;
            }
            switch (price)
            {
                case "All":
                    lstPro = db.Products.ToList(); break;
                case "L100":
                    lstPro = lstPro.Where(e => e.Price < 100000).ToList(); break;
                case "100200":
                    lstPro = lstPro.Where(e => e.Price >= 100000 && e.Price < 200000).ToList(); break;
                case "200500":
                    lstPro = lstPro.Where(e => e.Price >= 200000 && e.Price < 500000).ToList(); break;
                case "H500":
                    lstPro = lstPro.Where(e => e.Price >= 500000).ToList(); break;
                default:
                    break;
            }
            if (search != string.Empty)
            {
                lstPro = lstPro.Where(e => e.Name.Contains(search)).ToList();
            }
            ViewBag.brand=db.Brands.ToList();
            int NoEmpInPage = 8;
            int NoPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lstPro.Count) / Convert.ToDouble(NoEmpInPage)));
            int NoEmpForSkip = (page - 1) * NoEmpInPage;
            lstPro = lstPro.Skip(NoEmpForSkip).Take(NoEmpInPage).ToList();
            ViewBag.Page = page;
            ViewBag.NoPage = NoPage;
            if (sc == true)
                return View(lstPro);
            else
                return View("TrangChu", lstPro);
        }
        public ActionResult Detail(int id)
        {
            ViewBag.brand = db.Brands.ToList();
            Product product = db.Products.Where(e => e.Id.Equals(id)).FirstOrDefault();
            return View(product);
        }
    }
}