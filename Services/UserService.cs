﻿using EcoGasBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EcoGasBackend.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

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

        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<User?> GetUserbyUserName(string name) =>
           await _usersCollection.Find(x => x.UserName == name).FirstOrDefaultAsync();

        public async Task CreateAsync(User user) =>
            await _usersCollection.InsertOneAsync(user);

        public async Task UpdateAsync(string id, User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
