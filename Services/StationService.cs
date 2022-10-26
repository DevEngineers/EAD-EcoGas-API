using EcoGasBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

/**
 * This is the service class for station service
 * 
 * Auther: IT19153414 Akeel M.N.M
 * **/

namespace EcoGasBackend.Services
{
    public class StationService
    {
        private readonly IMongoCollection<Station> _stationsCollection;

        // Configuring Mongodb database connection
        public StationService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
               databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
               databaseSettings.Value.DatabaseName);

            _stationsCollection = mongoDatabase.GetCollection<Station>(
                databaseSettings.Value.StationsCollectionName);
        }

        // Get all station data
        public async Task<List<Station>> GetAsync() =>
            await _stationsCollection.Find(_ => true).ToListAsync();

        // Get station data by id
        public async Task<Station?> GetAsync(string id) =>
            await _stationsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Get station data by location
        public async Task<Station?> GetAsyncByLocation(string location) =>
            await _stationsCollection.Find(x => x.Location == location).FirstOrDefaultAsync();

        // Get station data by owner id
        public async Task<Station?> GetAsyncByOwnerID(string id) =>
           await _stationsCollection.AsQueryable().Where(x => x.OwnerID == id).FirstOrDefaultAsync();

        // Create station
        public async Task CreateAsync(Station station) =>
            await _stationsCollection.InsertOneAsync(station);

        // Update station data
        public async Task UpdateAsync(string id, Station updatedStation) =>
            await _stationsCollection.ReplaceOneAsync(x => x.Id == id, updatedStation);

        // Remove station data
        public async Task RemoveAsync(string id) =>
            await _stationsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
