using EcoGasBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

/**
 * This is the controller for station service endpoints
 * 
 * Auther: IT19175126 Zumry A.M
 * **/

namespace EcoGasBackend.Services
{
    public class QueueService
    {
        private readonly IMongoCollection<Queue> _quuesCollection;

        // Configuring Mongodb database connection
        public QueueService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
               databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
               databaseSettings.Value.DatabaseName);

            _quuesCollection = mongoDatabase.GetCollection<Queue>(
                databaseSettings.Value.QueueCollectionName);
        }

        // Get all Queue data
        public async Task<List<Queue>> GetAsync() =>
            await _quuesCollection.Find(_ => true).ToListAsync();

        // Get Queue data by id
        public async Task<Queue?> GetAsync(string id) =>
            await _quuesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Get station data by owner id
        public async Task<Queue?> GetAsyncByUserID(string id) =>
           await _quuesCollection.Find(x => x.UserID == id).FirstOrDefaultAsync();

        // Create Queue
        public async Task CreateAsync(Queue queue) =>
            await _quuesCollection.InsertOneAsync(queue);

        // Update Queue
        public async Task UpdateAsync(string id, Queue updatedQueue) =>
            await _quuesCollection.ReplaceOneAsync(x => x.Id == id, updatedQueue);

        // Remove Queue
        public async Task RemoveAsync(string id) =>
            await _quuesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
