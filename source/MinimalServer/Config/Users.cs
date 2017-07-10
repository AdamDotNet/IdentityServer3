using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace MinimalServer.Config
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "110",
                    Username = "user",
                    Password = "password",
                    Enabled = true,
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "User Guy"),
                        new Claim(Constants.ClaimTypes.Email, "fake@mail.com"),
                        new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                    }
                }
            };
        }
    }
}