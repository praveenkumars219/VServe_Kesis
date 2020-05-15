using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQl.Models;
using GraphQl.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphQl.Controllers
{
    [Route("api/userdetails")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly UserDetailsRepo _userDetails;

        public UserDetailController(UserDetailsRepo userDetails)
        {
            _userDetails = userDetails;
        }
        // GET: api/UserDetail
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UserDetail/5
        [Route("getbyUserId")]
        [HttpGet]
        public async Task<UserDetail> Get(string id)
        {
           return await _userDetails.GetUserDetails(id).ConfigureAwait(false);
        }

        // POST: api/UserDetail
        [HttpPost]
        public async Task<bool> Post([FromBody] UserDetail user)
        {
            if (string.IsNullOrWhiteSpace(user.Id))
            {
                return await _userDetails.CreateUserDetails(user).ConfigureAwait(false);
            }

            return await _userDetails.Update(user.Id, user).ConfigureAwait(false);         
        }

        // PUT: api/UserDetail/5
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
