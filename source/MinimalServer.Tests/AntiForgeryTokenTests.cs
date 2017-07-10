using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;
using Xunit;

namespace MinimalServer.Tests
{
    public class AntiForgeryTokenTests
    {
        private const string mvcTokenName = "__RequestVerificationToken";
        private const string idsrvTokenName = "idsrv.xsrf";

        // This test should pass when the MVC anti-forgery token is not present. Fails otherwise.
        // CRITICAL: See MinimalServer.Views.IdentityServer.Login.cshtml
        // CRITICAL: Even though this app is compiled for .Net 4.6.2, ensure running computer has .Net 4.7 installed.
        [Fact]
        public async Task NoMvcAntiForgeryToken_IdsrvTokenIsPresent()
        {
            var cookies = await ReadCookiesFromLoginView();
            Assert.DoesNotContain(cookies, c => c.Name.Equals(mvcTokenName, StringComparison.OrdinalIgnoreCase));
            Assert.Contains(cookies, c => c.Name.Equals(idsrvTokenName, StringComparison.OrdinalIgnoreCase));
        }

        // This test should pass when the MVC anti-forgery token is present. Fails otherwise.
        // CRITICAL: See MinimalServer.Views.IdentityServer.Login.cshtml
        // CRITICAL: Even though this app is compiled for .Net 4.6.2, ensure running computer has .Net 4.7 installed.
        [Fact]
        public async Task HasMvcAntiForgeryToken_IdsrvTokenIsNotPresent()
        {
            var cookies = await ReadCookiesFromLoginView();
            Assert.Contains(cookies, c => c.Name.Equals(mvcTokenName, StringComparison.OrdinalIgnoreCase));
            Assert.DoesNotContain(cookies, c => c.Name.Equals(idsrvTokenName, StringComparison.OrdinalIgnoreCase));
        }

        private async Task<IEnumerable<Cookie>> ReadCookiesFromLoginView()
        {
            // Simple "nonces"
            string state = Guid.NewGuid().ToString("N");
            string nonce = Guid.NewGuid().ToString("N");

            string authorizeEndpoint = "http://localhost:53326/Identity/connect/authorize";

            OAuth2Client oauthClient = new OAuth2Client(new Uri(authorizeEndpoint));

            string responseType = "code id_token";
            string scope = "openid email contact_id license_api offline_access";
            string redirectUri = "oob://Minimal/Client";

            string startUri = oauthClient.CreateAuthorizeUrl("MinimalClientId", responseType, scope, redirectUri, state, nonce);

            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);

            HttpResponseMessage response = await client.GetAsync(startUri);
            response.EnsureSuccessStatusCode();

            CookieCollection cookies = handler.CookieContainer.GetCookies(new Uri("http://localhost:53326/Identity"));
            return cookies.Cast<Cookie>().ToList(); // Convert from IEnumerable to generic IEnumerable<T>.
        }
    }
}
