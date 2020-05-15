using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GraphQl.Models
{
    public class Location:BaseClass
    {
        public string LocationId { get; set; }
        [Required]
        [BsonElement("LocationName")]
        public string LocationName { get; set; }
    }
}