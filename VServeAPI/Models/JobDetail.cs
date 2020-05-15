using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GraphQl.Models
{
    public class JobDetail:BaseClass
    {
        [Required]
        public JobProfile JobProfile { get; set; }
        [Required]
        public Location Location { get; set; }

        [BsonElement("JobDescription")]
        public string JobDescription { get; set; }

        [BsonElement("AdditionalDetails")]
        public string AdditionalDetails { get; set; }

        [Required]
        [BsonElement("PhoneNo")]
        public string PhoneNo { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("StartDate")]
        public string StartDate { get; set; }

        [BsonElement("EndDate")]
        public string EndDate { get; set; }

        [BsonElement("IsValid")]
        public bool IsValid { get; set; }

        [BsonElement("IsValidEndDate")]
        public string IsValidEndDate { get; set; }

    }
}