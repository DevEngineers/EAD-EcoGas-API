using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/**
 * This is the User model class
 * 
 * Author: IT19167442 Nusky M.A.M
 * **/


namespace EcoGasBackend.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("userName")]
        public string? UserName { get; set; }
        [BsonElement("type")]
        public string? Type { get; set; }

    }
}
