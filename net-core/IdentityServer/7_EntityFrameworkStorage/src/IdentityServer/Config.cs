// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            var client = CreateJavaScriptClient("js", "JavaScript Client", "http://localhost:5003");

            return new Client[] { client };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                }
            };
        }

        private static Client CreateJavaScriptClient(string id, string name, string url)
        {
            return new Client
            {
                AccessTokenLifetime = 20, // 20 sec
                AllowAccessTokensViaBrowser = true,
                AllowedCorsOrigins = { url },
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                ClientId = id,
                ClientName = name,
                //EnableLocalLogin = false,
                PostLogoutRedirectUris = { $"{url}/index.html" },
                RequireClientSecret = false,
                RequireConsent = false,
                RequirePkce = true,
                RedirectUris = { $"{url}/callback.html", $"{url}/silent_renew.html" }
            };
        }
    }
}