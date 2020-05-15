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
    public class UserRepository
    {
        private readonly MongoContext _context;
        private readonly IMongoCollection<Users> _userDetails;
        public UserRepository(MongoContext context)
        {
            _context = context;
        }

        public async Task Users()
        {
            var command = new BsonDocument("usersInfo", 1);
            var result = await _context.MongoDb.RunCommandAsync<BsonDocument>(command).ConfigureAwait(false);
        }

        public async Task CreateUser(Users user)
        {
            var userInformation = user.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(user, null));
            var writeConcern = WriteConcern.WMajority.With(wTimeout: TimeSpan.FromMilliseconds(5000));
            var command = new BsonDocument
            {
                { "createUser", user.Email },
                { "pwd", user.Password },
                { "customData", new BsonDocument(userInformation) },
                { "roles", new BsonArray
                {
                   new BsonDocument
                   {
                       { "role", "vserveuser" },
                       { "db", "admin" }
                   },
                   "readWrite"
               }},
                { "writeConcern", writeConcern.ToBsonDocument() }
            };

            await _context.MongoDb.RunCommandAsync<BsonDocument>(command);
        }

        public async Task UpdateUser(Users user)
        {
            var userInformation = user.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(user, null));
            var writeConcern = WriteConcern.WMajority.With(wTimeout: TimeSpan.FromMilliseconds(5000));
            var command = new BsonDocument
            {
                { "updateUser", user.Email },
                { "pwd", user.Password },
                { "customData", new BsonDocument(userInformation) },
                { "roles", new BsonArray
                {
                   new BsonDocument
                   {
                       { "role", "vserveuser" },
                       { "db", "admin" }
                   },
                   "readWrite"
               }},
                { "writeConcern", writeConcern.ToBsonDocument() }
            };

            await _context.MongoDb.RunCommandAsync<BsonDocument>(command);
        }

        public async Task DeleteUser(Users user)
        {
            var command = @"{ dropUser: "+ user.Email + @",
                  writeConcern: { w: ""majority"", wtimeout: 5000 }
                }";

            // Run the command. If it fails, an exception will be thrown.
            await _context.MongoDb.RunCommandAsync<BsonDocument>(command);
        }
    }
}
