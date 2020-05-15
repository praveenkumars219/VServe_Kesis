using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VServe_Google_API;

namespace GraphQl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationApi _auth;
        private readonly IConfiguration _configuration;
        public AuthController(AuthenticationApi auth, IConfiguration configuration)
        {
            _auth = auth;
            _configuration = configuration;
        }

        // GET: api/Auth
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Auth/5
        [HttpGet("{id}", Name = "Get Auth")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Auth
        [HttpPost]
        public async Task Post(Users user)
        {
            var apiKey = _configuration.GetSection("FirebaseConfig:ApiKey").Value;
            await _auth.LoginWithEmailPassword(user.Email, user.Password, apiKey).ConfigureAwait(false);
        }

        // PUT: api/Auth/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
