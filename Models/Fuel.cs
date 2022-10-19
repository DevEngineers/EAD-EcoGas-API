using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EcoGasBackend.Models
{
    public class Fuel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("stationID")]
        public string? StationID { get; set; }
        [BsonElement("fuelName")]
        public string? FuelName { get; set; }
        [BsonElement("capacity")]
        public float? Capacity { get; set; }
        [BsonElement("arrivalDate")]
        public string? ArrivalDate { get; set; }
        [BsonElement("arrivalTime")]
        public string? ArrivalTime { get; set; }
    }
}
