using _2001216310_NguyenThanhVi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2001216310_NguyenThanhVi.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        MyDB db = new MyDB();
        public ActionResult Index()
        {
            List<Cart> carts = db.Carts.ToList();
            return View(carts);
        }

        public ActionResult Add(int id = 0)
        {
            if (id > 0)
            {
                Cart cartItem = db.Carts.Where(row => row.ProId == id).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                }
                else
                {
                    Cart cart = new Cart();
                    cart.ProId = id;
                    cart.Quantity = 1;
                    db.Carts.Add(cart);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult UpdateQuantity(int quan = 0, int proid = 0)
        {
            if (quan > 0)
            {
                Cart cartItem = db.Carts.Where(cart => cart.ProId == proid).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity = quan;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeletePro(int id)
        {
            Cart cartItem = db.Carts.Where(cart => cart.ProId == id).FirstOrDefault();
            if (cartItem != null)
            {
                db.Carts.Remove(cartItem);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}