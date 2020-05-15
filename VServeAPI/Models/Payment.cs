using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQl.Models
{
    public class Payment: BaseClass
    {
        [BsonElement("AssignmentId")]
        public string AssignmentId { get; set; }

        [BsonElement("PaymentDate")]
        public DateTime PaymentDate { get; set; }

        [BsonElement("Payer")]
        public string Payer { get; set; }

        [BsonElement("Payee")]
        public string Payee { get; set; }

        [BsonElement("PaymentAmount")]
        public decimal PaymentAmount { get; set; }
    }
}
