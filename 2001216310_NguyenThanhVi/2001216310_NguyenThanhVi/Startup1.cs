using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.EntityFramework;
using _2001216310_NguyenThanhVi.Identity;

[assembly: OwinStartup(typeof(_2001216310_NguyenThanhVi.Startup1))]

namespace _2001216310_NguyenThanhVi
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions() { 
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath=new PathString("/Account/Login")
            });
        }
        public void CreateRolesAnhUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AppDbContext()));
            var appDbContext = new AppDbContext();
            var appUserStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(appUserStore);

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (userManager.FindByName("admin") == null)
            {
                var user = new AppUser();
                user.Name = "admin";
                user.UserName = "QTV Sever";
                user.Email = "admin@gmail.com";
                string userPwd = "admin123";
                var chkUser=userManager.Create(user,userPwd);
                if(chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
        }
    }
}
