using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T10.Models;

namespace T10.Data
{
    public static class TodoItemDatabase
    {
        private static MongoClient client = new MongoClient(Constants.MONGO_CONNECTION_STRING);
        private static IMongoDatabase database;

        public static void Setup()
        {
            database = client.GetDatabase("todolist");
        }

        public static async Task Save(TodoItem item)
        {
            var collection = GetCollection();
            var rs = collection.Find<TodoItem>(t => t.Id == item.Id);
            if (rs.CountDocuments() == 0)
            {
                await collection.InsertOneAsync(item);
            }
            else
            {
                await collection.ReplaceOneAsync(t => t.Id == item.Id,item, new ReplaceOptions { IsUpsert = false });
            }
        }

        public static async Task Delete(TodoItem item)
        {
            var collection = GetCollection();
            await collection.DeleteOneAsync<TodoItem>(t => t.Id == item.Id);
        }


        public static async Task<List<TodoItem>> GetItemsAsync()
        {
            var collection = GetCollection();
            return await Task.FromResult(collection.AsQueryable<TodoItem>().ToList());
        }

        private static IMongoCollection<TodoItem> GetCollection()
        {
            return database.GetCollection<TodoItem>("todos");
        }
    }
}
