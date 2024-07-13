using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _2001216310_NguyenThanhVi.Identity
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("MyCon") { }
    }
}