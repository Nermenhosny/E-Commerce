using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentittyEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class AppIdentityContextSeed
    {
        public AppIdentityContextSeed() { }
        public static async Task SeedUserAsync(UserManager<AppUser>userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Nermen Hosny",
                    Email = "NermenHosny@gmail.com",
                    UserName = "NermenHosny",
                    Address = new Address
                    {
                        FirstName = "Nermen",
                        LastName = "Hosny",
                        City = "Giza",
                        State = "Cairo",
                        ZipCode = "12344",
                    }


                };
                await userManager.CreateAsync(user, "Password123!@");
            }
        }
    }
}
