using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Abc.MvcWebUI.Identity
{
    public class IdentityInitializer : DropCreateDatabaseIfModelChanges<IdentityDataContext>
    {
        // app açıldığında veri tabanında hasar olabilir bunun önüne geçmek için initializer kurup sistem ilk açıldığında rolleri ekleyebiliriz
        //hatta üye ekleyip rol atayabiliriz bu şekilde uygulamayı açıp önce rol eklemeye gerek kalmaz yada veritabanından elle doldurmamıza gerek kalmaz
        protected override void Seed(IdentityDataContext context)
        {


            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "admin", Description = "yönetici rolü" };
                manager.Create(role);

            }



            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "user", Description = "kullanıcı rolü" };
                manager.Create(role);
            }


            if (!context.Users.Any(i => i.Name == "bot"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    UserName = "botAdmin",
                    Name = "bot",
                    SurName = "admin",
                    Email = "botAdmin@hotmail.com",
                    Roles = { }
                };
                manager.Create(user, "botAdmin123!");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }


            base.Seed(context);

        }
    }
}