using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _2001216310_NguyenThanhVi.Identity
{
    public class AppUser:IdentityUser
    {
        public string UsName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}