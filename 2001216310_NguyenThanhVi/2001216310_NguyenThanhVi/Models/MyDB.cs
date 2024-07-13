using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _2001216310_NguyenThanhVi.Models
{
    public class MyDB:DbContext
    {
        public MyDB() : base("MyCon") { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Buying> Buying { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Storage> Storage { get; set; }
        public DbSet<Store> Stores { get; set; }
    }
}