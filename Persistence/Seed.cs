  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                {
                    Email="test@test.ge",
                    UserName="test",
                    PrivateNumber="00000000000",
                    Address=new Address
                    {
                        FirstName="test",
                        LastName="test",
                        Street="10 test",
                        City="Tbilisi",
                        State="Tbilisi",
                        Zipcode="0000"
                    }
                },
                new AppUser
                {
                    Email="test2@test.ge",
                    UserName="test2",
                    PrivateNumber="00000000002",
                    Address=new Address
                    {
                        FirstName="test2",
                        LastName="test2",
                        Street="10 test2",
                        City="Tbilisi",
                        State="Tbilisi",
                        Zipcode="0002"
                    }
                },
                new AppUser
                {
                    Email="test3@test.ge",
                    UserName="test3",
                    PrivateNumber="00000000003",
                    Address=new Address
                    {
                        FirstName="test3",
                        LastName="test3",
                        Street="10 test3",
                        City="Tbilisi",
                        State="Tbilisi",
                        Zipcode="0003"
                    }
                },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }
        }
    }
}