using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace MinimalServer.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    ClientName = "Minimal Client Application",
                    Enabled = true,
                    ClientId = "MinimalClientId",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("MinimalClientSecret".Sha256())
                    },
                    Flow = Flows.Hybrid,
                    RequireConsent = false,
                    AllowRememberConsent = false,
                    EnableLocalLogin = true,
                    AccessTokenLifetime = 3600, // The default of 1 hour
                    AbsoluteRefreshTokenLifetime = 5184000, // 60 days
                    AccessTokenType = AccessTokenType.Reference,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AllowAccessToAllScopes = true,

                    RedirectUris = new List<string>
                    {
                        "oob://Minimal/Client"
                    }
                }
            };
        }
    }
}