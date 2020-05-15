using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;


namespace GraphQl.Models
{
    public class JobProfile:BaseClass
    {

        [Required]
        [BsonElement("JobTitle")]
        public string JobTitle { get; set; }

        [BsonElement("JobCode")]
        public string JobCode { get; set; }
    }
}