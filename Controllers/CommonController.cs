using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using VServe_Google_API;

namespace GraphQl.Controllers
{
    [Route("api/commonservice")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly PlaceApi _placeApi;
        public CommonController(IConfiguration configuration, PlaceApi placeApi)
        {
            _configuration = configuration;
            _placeApi = placeApi;
        }


        // GET: api/Common
        [Route("GetAll")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Common/xyz
        [Route("GetLocations")]
        [HttpGet]
        public async Task<IEnumerable<Location>> Get(string searchkey)
        {
           var autoSearch =  await _placeApi.AutoSearch(searchkey, _configuration.GetSection("FirebaseConfig:ApiKey").Value).ConfigureAwait(false);
            var predictions = autoSearch.GetValue("predictions");
            List<Location> locations = new List<Location>();
            foreach (var jToken in predictions)
            {
                locations.Add(new Location
                {
                    LocationName = jToken.SelectToken("description").ToString(),
                    Id = jToken.SelectToken("place_id").ToString(),

                });
            }
            return locations;
        }

        [Route("GetWorks")]
        [HttpGet]
        public async Task<dynamic> GetWork(string searchwork)
        {
            return await _placeApi.AutoSearch(searchwork, _configuration.GetSection("FirebaseConfig:ApiKey").Value).ConfigureAwait(false);
        }

        [Route("SaveWork")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


        [Route("UpdateWork")]
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [Route("DeleteWork")]
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
