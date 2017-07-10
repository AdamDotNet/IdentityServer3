using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace MinimalServer.Config
{
    public static class Scopes
    {
        private const string contactIdClaimType = "contact_id";
        private const string contactApiScope = "contact_api";
        private const string licenseApiScope = "license_api";

        public static IEnumerable<Scope> Get()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Email,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = contactIdClaimType,
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim
                        {
                            AlwaysIncludeInIdToken = true,
                            Name = contactIdClaimType
                        },
                        new ScopeClaim
                        {
                            AlwaysIncludeInIdToken = true,
                            Name = Constants.ClaimTypes.Role
                        }
                    }
                },
                new Scope
                {
                    Name = contactApiScope,
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim> { new ScopeClaim { Name = contactIdClaimType } }
                },
                new Scope
                {
                    Name = licenseApiScope,
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim> { new ScopeClaim { Name = contactIdClaimType } }
                }
             };
        }
    }
}