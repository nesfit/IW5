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
                    Username = "chirurg",
                    Password = "password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Kachnička Chirurg"),
                        new Claim(JwtClaimTypes.GivenName, "Kachnička"),
                        new Claim(JwtClaimTypes.FamilyName, "Chirurg"),
                        new Claim(JwtClaimTypes.Email, "kachnickachirurg@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "fuhrer",
                    Password = "password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Malý Fuhrer"),
                        new Claim(JwtClaimTypes.GivenName, "Malý"),
                        new Claim(JwtClaimTypes.FamilyName, "Fuhrer"),
                        new Claim(JwtClaimTypes.Email, "malyfuhrer@email.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}
