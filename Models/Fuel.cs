using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/**
 * This is the Fuel model class
 * 
 * Auther: IT19153414 Akeel M.N.M
 * **/

namespace EcoGasBackend.Models
{
    public class Fuel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String? Id { get; set; }
        [BsonElement("fuelName")]
        public string? FuelName { get; set; }
        [BsonElement("capacity")]
        public float? Capacity { get; set; }
        [BsonElement("arrivalDate")]
        public string? ArrivalDate { get; set; }
        [BsonElement("arrivalTime")]
        public string? ArrivalTime { get; set; }

        public Fuel(){
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
