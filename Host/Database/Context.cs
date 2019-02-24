using Host.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Host.Database
{
    public sealed class Context : DbContext
    {
        public readonly IMongoCollection<ProductModel> Products;
        public readonly IMongoCollection<Credential> Credentials;
        public readonly IMongoCollection<Session> Sessions;
        public readonly IMongoCollection<User> Users;

        public Context()
        {
            var mongo = new MongoClient("mongodb://localhost");
            var db = mongo.GetDatabase(nameof(Context));
            Products = db.GetCollection<ProductModel>(nameof(Products));
            Credentials = db.GetCollection<Credential>(nameof(Credentials));
            Sessions = db.GetCollection<Session>(nameof(Sessions));
            Users = db.GetCollection<User>(nameof(Users));
        }
    }
}