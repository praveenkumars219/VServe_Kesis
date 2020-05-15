using GraphQl.Models;
using GraphQl.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VServe_Google_API;

namespace GraphQl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AuthenticationApi _auth;
        private readonly IConfiguration _configuration;
        private readonly FireBaseRepo _fireBaseRepo;
        public LoginController(AuthenticationApi auth, IConfiguration configuration, FireBaseRepo fireBaseRepo)
        {
            _auth = auth;
            _configuration = configuration;
            _fireBaseRepo = fireBaseRepo;
        }

        [HttpPost]
        public async Task<Users> Post(Users user)
        {
            var apiKey = _configuration.GetSection("FirebaseConfig:ApiKey").Value;
            var response = await _auth.LoginWithEmailPassword(user.Email, user.Password, apiKey).ConfigureAwait(false);      
            var data = JsonConvert.DeserializeObject<Users>(response);
            Users userData = await _fireBaseRepo.GetUserById(data.LocalId).ConfigureAwait(false);
            userData.IdToken = data.IdToken;
            return userData;
        }
    }
}
