// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using IdentityModel;
using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;

namespace CookBook.IdentityProvider.App;

public class TestUsers
{
    public static List<TestUser> Users
    {
        get
        {
            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Heidelberg",
                postal_code = 69118,
                country = "Germany"
            };
                
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "user",
                    Password = "password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "User CookBook"),
                        new Claim(JwtClaimTypes.GivenName, "User"),
                        new Claim(JwtClaimTypes.FamilyName, "CookBook"),
                        new Claim(JwtClaimTypes.Email, "usercookbook@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "admin",
                    Password = "password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Admin CookBook"),
                        new Claim(JwtClaimTypes.GivenName, "Admin"),
                        new Claim(JwtClaimTypes.FamilyName, "CookBook"),
                        new Claim(JwtClaimTypes.Email, "admincookbook@email.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}
