using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQl.Models
{
    public class UserDetail:BaseClass
    {
        public string UserId { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("DateofBirth")]
        public DateTime? DateofBirth { get; set; }
         
        [BsonElement("Gender")]
        public string Gender { get; set; }

        [BsonElement("Location")]
        public string Location { get; set; }

        [BsonElement("Profession")]
        public string Profession { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Phone")]
        public string Phone { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

    }
}
