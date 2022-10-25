using EcoGasBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EcoGasBackend.Services
{
    public class QueueService
    {
        private readonly IMongoCollection<Queue> _quuesCollection;

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

        public async Task<List<Queue>> GetAsync() =>
            await _quuesCollection.Find(_ => true).ToListAsync();

        public async Task<Queue?> GetAsync(string id) =>
            await _quuesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Queue queue) =>
            await _quuesCollection.InsertOneAsync(queue);

        public async Task UpdateAsync(string id, Queue updatedQueue) =>
            await _quuesCollection.ReplaceOneAsync(x => x.Id == id, updatedQueue);

        public async Task RemoveAsync(string id) =>
            await _quuesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
