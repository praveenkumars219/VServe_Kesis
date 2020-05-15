using GraphQl.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GraphQl.Repository
{
    public class UserDetailsRepo
    {
        private readonly MongoContext _context;
        private readonly IMongoCollection<UserDetail> _userDetails;

        public UserDetailsRepo(MongoContext context)
        {
            _context = context;
            _userDetails = _context.MongoDb.GetCollection<UserDetail>("users");
        }

        public async Task<UserDetail> GetUserDetails(string userId)
        {
            try
            {
                var userDetail = await _userDetails.FindAsync<UserDetail>(job => job.UserId == userId).ConfigureAwait(false);
                return userDetail.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> CreateUserDetails(UserDetail user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
            try
            {
                await _userDetails.InsertOneAsync(user).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(string id, UserDetail user)
        {
            try
            {
                await _userDetails.ReplaceOneAsync(job => job.Id == id, user).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task Remove(UserDetail jobIn) =>
          await _userDetails.DeleteOneAsync(job => job.Id == jobIn.Id).ConfigureAwait(false);

        public async Task Remove(string id) =>
           await _userDetails.DeleteOneAsync(job => job.Id == id).ConfigureAwait(false);
    }
}
