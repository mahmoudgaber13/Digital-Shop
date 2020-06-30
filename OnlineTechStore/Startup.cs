using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OnlineTechStore.Models;
using Owin;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(OnlineTechStore.Startup))]
namespace OnlineTechStore
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdmin();
            CreateRoles();
        }
        public void CreateAdmin()
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            
            
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "Admin",
                Email = "admin@gmail.com"
            };
            var check = userManager.Create(user, "Admin@123");
            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admin");
            }
        }
        public void CreateRoles()
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if (!roleManager.RoleExists("Admin"))
            {
                role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Customer"))
            {
                role = new IdentityRole
                {
                    Name = "Customer"
                };
                roleManager.Create(role);
            }

        }
        
    }
}
