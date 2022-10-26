using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

/**
 * This is the controller for station service endpoints
 * 
 * Auther: IT19175126 Zumry A.M
 * **/

namespace EcoGasBackend.Models
{
    public class Queue
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("stationID")]
        public string? StationID { get; set; }
        [BsonElement("userID")]
        public string? UserID { get; set; }
        [BsonElement("fuelName")]
        public string? FuelName { get; set; }
        [BsonElement("arrivalDate")]
        public string? ArrivalDate { get; set; }
        [BsonElement("arrivalTime")]
        public string? ArrivalTime { get; set; }

    }
}
