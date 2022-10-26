using EcoGasBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

/**
 * This is the service class for User service
 * 
 * Author: IT19167442 Nusky M.A.M
 * **/

namespace EcoGasBackend.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        // Configuring Mongodb database connection
        public UserService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
               databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
               databaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(
                databaseSettings.Value.UsersCollectionName);
        }

        // Get all User data
        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        // Get User data by id
        public async Task<User?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Get User data by name
        public async Task<User?> GetUserbyUserName(string name) =>
           await _usersCollection.Find(x => x.UserName == name).FirstOrDefaultAsync();

        // Create New User 
        public async Task CreateAsync(User user) =>
            await _usersCollection.InsertOneAsync(user);

        // Update User Data 
        public async Task UpdateAsync(string id, User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        // Delete User Data 
        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
