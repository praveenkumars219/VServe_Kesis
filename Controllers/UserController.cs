using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQl.Models;
using GraphQl.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GraphQl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly FireBaseRepo _fireBaseRepo;

        public UserController(UserRepository userRepository, IConfiguration configuration, FireBaseRepo fireBaseRepo)
        {
            _userRepository = userRepository;
            _fireBaseRepo = fireBaseRepo;
            _configuration = configuration;
        }
        // GET: api/User
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            await _userRepository.Users().ConfigureAwait(false);
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get User By Id")]
        public async Task<Users> Get(string id)
        {
           return await _fireBaseRepo.GetUserById(id).ConfigureAwait(false);
        }

        // POST: api/User
        [HttpPost]
        public async Task<Users> Post(Users user)
        {
            return await _fireBaseRepo.CreateUser(user).ConfigureAwait(false);
        }

        // PUT: api/User/5
        [HttpPut]
        public async Task<Users> Put(Users user)
        {
            return await _fireBaseRepo.UpdateUser(user).ConfigureAwait(false);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<Users> Delete(Users user)
        {
            return await _fireBaseRepo.DeleteUser(user).ConfigureAwait(false);
        }
    }
}
