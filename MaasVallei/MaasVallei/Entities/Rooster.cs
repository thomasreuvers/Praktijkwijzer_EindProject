using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MaasVallei.Entities
{
    public class Rooster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }

        public DateTime StartShift { get; set; }

        public DateTime EndShift { get; set; }

        public DateTime RoosterDate { get; set; }
    }
}
