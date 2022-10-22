using EcoGasBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EcoGasBackend.Services
{
    public class StationService
    {
        private readonly IMongoCollection<Station> _stationsCollection;

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

        public async Task<List<Station>> GetAsync() =>
            await _stationsCollection.Find(_ => true).ToListAsync();

        public async Task<Station?> GetAsync(string id) =>
            await _stationsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Station?> GetAsyncByOwnerID(string id) =>
           await _stationsCollection.AsQueryable().Where(x => x.OwnerID == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Station station) =>
            await _stationsCollection.InsertOneAsync(station);

        public async Task UpdateAsync(string id, Station updatedStation) =>
            await _stationsCollection.ReplaceOneAsync(x => x.Id == id, updatedStation);

        public async Task RemoveAsync(string id) =>
            await _stationsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
