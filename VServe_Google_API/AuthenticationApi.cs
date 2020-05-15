using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;

namespace VServe_Google_API
{
    public class AuthenticationApi
    {
        private readonly HttpClient client;
        private readonly string BaseApiUrl;
        public AuthenticationApi()
        {
            if (client == null)
            {
                client = new HttpClient();
                BaseApiUrl = "https://identitytoolkit.googleapis.com/v1/accounts:";
            }
        }

        public async Task<string> LoginWithEmailPassword(string userName,string password,string ApiKey)
        {
            string url = BaseApiUrl + "signInWithPassword?key=" + ApiKey;

            var result = await client.PostAsJsonAsync(url,new { email = userName, password = password, returnSecureToken = true }).ConfigureAwait(false);
            var response =await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return response;
        }
    }
}
