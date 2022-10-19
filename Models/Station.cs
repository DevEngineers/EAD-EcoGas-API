﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EcoGasBackend.Models
{
    public class Station
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("ownerID")]
        public string? OwnerID { get; set; }
        [BsonElement("ownerName")]
        public string? OwnerName { get; set; }
        [BsonElement("stationName")]
        public string? StationName { get; set; }
        [BsonElement("location")]
        public string? Location { get; set; }
        [BsonElement("fuel")]
        public List<Fuel>? Fuel { get; set; }
        [BsonElement("petrolQueue")]
        public int? PetrolQueue { get; set; }
        [BsonElement("superPetrolQueue")]
        public int? SuperPetrolQueue { get; set; }
        [BsonElement("dieselQueue")]
        public int? DieselQueue { get; set; }
    }
}