using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MaasVallei.Entities
{
    public class Complaint
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string Priority { get; set; }
        public string State { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
