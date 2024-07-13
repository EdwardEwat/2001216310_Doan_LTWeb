using _2001216310_NguyenThanhVi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2001216310_NguyenThanhVi.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        MyDB db = new MyDB();
        public ActionResult MainOfProduct(int page = 1, string search = "", int brandsearch = 0, string column = "", string price = "", string status = "", bool sc = true)
        {
            List<Product> lstPro = db.Products.ToList();
            if (brandsearch != 0)
            {
                lstPro = lstPro.Where(e => e.BrandId == brandsearch).ToList();
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
            ViewBag.brand = db.Brands.ToList();
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
        public ActionResult Delete(int id)
        {
            Product pro=db.Products.Where(e=>e.Id == id).FirstOrDefault();
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("MainOfProduct", "Product", null);
        }
        public ActionResult Fix(int id)
        {
            Product pro = db.Products.Where(e => e.Id == id).FirstOrDefault();
            ViewBag.brand=db.Brands.ToList();
            ViewBag.store=db.Stores.ToList();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Fix(int id, Product pro, HttpPostedFileBase imageFile)
        {
            Product a=db.Products.Where(e=> e.Id == id).FirstOrDefault();
            if(a!=null)
            {
                a.Name= pro.Name;
                a.Description= pro.Description;
                a.Price= pro.Price;
                a.BrandId= pro.BrandId;
                a.Status=pro.Status;
                a.Storages.StoreId=pro.Storages.StoreId;
                a.Type=pro.Type;
                a.Storages.Type = pro.Type;
                a.Storages.Amount=pro.Storages.Amount;
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (imageFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("Img", "Kích thước file không được lớn hơn 2MB.");
                        return View();
                    }
                    var allowedExtensions = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileEx))
                    {
                        ModelState.AddModelError("Img", "Chỉ chấp nhận hình ảnh dạng JPG hoặc PNG.");
                        return View();
                    }
                    a.Img = "";
                    db.SaveChanges();

                    Product pro1 = db.Products.Where(e => e.Id == id).FirstOrDefault();
                    var fileName = pro1.Id.ToString() + fileEx;
                    var path = Path.Combine(Server.MapPath("~/Img"), fileName);
                    imageFile.SaveAs(path);

                    pro1.Img = fileName;
                }
                else
                {
                    a.Img = "";
                }
                db.SaveChanges();
                return RedirectToAction("MainOfProduct", "Product", null);
            }
            else
            {
                Product b = db.Products.Where(e => e.Id == id).FirstOrDefault();
                ViewBag.brand = db.Brands.ToList();
                ViewBag.store = db.Stores.ToList();
                return View(b);
            }
        }
        public ActionResult Create()
        {
            ViewBag.brand = db.Brands.ToList();
            ViewBag.store = db.Stores.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product a, HttpPostedFileBase imageFile)
        {
            if(ModelState.IsValid)
            {
                a.BfDiscount = 0;
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (imageFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("Img", "Kích thước file không được lớn hơn 2MB.");
                        return View();
                    }
                    var allowedExtensions = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileEx))
                    {
                        ModelState.AddModelError("Img", "Chỉ chấp nhận hình ảnh dạng JPG hoặc PNG.");
                        return View();
                    }
                    a.Img = "";
                    db.Products.Add(a);
                    db.SaveChanges();

                    Product pro = db.Products.ToList().Last();
                    var fileName = pro.Id.ToString() + fileEx;
                    var path = Path.Combine(Server.MapPath("~/Img"), fileName);
                    imageFile.SaveAs(path);

                    pro.Img = fileName;
                }
                else
                {
                    a.Img = "";
                    db.Products.Add(a);
                }
                db.SaveChanges();
                Storage c = new Storage();
                c.Type = a.Type;
                c.Amount = a.Storages.Amount;
                c.StoreId = a.Storages.StoreId;
                db.Storage.Add(c);
                db.SaveChanges();
                return RedirectToAction("MainOfProduct", "Product", null);
            }
            return View();
        }
        public ActionResult DeleteImage(int id)
        {
            Product product = db.Products.Where(row => row.Id == id).FirstOrDefault();
            if (product != null)
            {
                if (product.Img == "")
                {
                    return RedirectToAction("Index");
                }

                string imagePath = Server.MapPath("~/Img/" + product.Img);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                product.Img = "";
                db.SaveChanges();
                return RedirectToAction("MainOfProduct", "Product", null);
            }
            return RedirectToAction("Index");
        }
    }
}