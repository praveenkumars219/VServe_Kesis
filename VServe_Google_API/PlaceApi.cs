using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VServe_Google_API
{
    public class PlaceApi
    {
        private readonly HttpClient client;
        private readonly string BaseApiUrl;
        public PlaceApi()
        {
            if (client == null)
            {
                client = new HttpClient();
                BaseApiUrl = "https://maps.googleapis.com/maps/api/";
            }
        }
        public async Task<JObject> SearchPlace(string input, string type, List<string> fields, string ApiKey)
        {
            string url = BaseApiUrl + "place/findplacefromtext/json?input=" + input + "&inputtype=" +
                            type + "&fields=" + string.Join(',', fields) + "&key=" + ApiKey;
            var result = await client.GetAsync(url).ConfigureAwait(false);
            var resultString =await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            JObject jObject = JObject.Parse(resultString);
            return jObject;
        }


        public async Task<dynamic> AutoSearch(string input,string ApiKey)
        {
            string url = BaseApiUrl + "place/autocomplete/json?input=" + input + "&key=" + ApiKey;
            var result = await client.GetAsync(url).ConfigureAwait(false);
            var resultString = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            dynamic jObject = JsonConvert.DeserializeObject(resultString);
            return jObject;
        }

    }
}
