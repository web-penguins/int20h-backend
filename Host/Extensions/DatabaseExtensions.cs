using System;
using Host.Database;
using Host.Models;
using MlkPwgen;
using MongoDB.Driver;

namespace Host.Extensions
{
    public static class DatabaseExtensions
    {
        public static void AddDefaultData(this Context context)
        {
            if (context.Users.Find(FilterDefinition<User>.Empty).Any()) return;
            
            context.Users.InsertOne(new User
            {
                Id = 1,
                Name = "Viacheslav Zhuravskyi",
                Username = "lundibundi",
                RegisterDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"),
                TotalAmountOfProducts = 0
            });
            
            var credential = new Credential
            {
                Id = 1,
                Salt = PasswordGenerator.Generate(4)
            };
            credential.PasswordHash = PasswordExtensions.HashPassword("jerboa666", credential.Salt);
            context.Credentials.InsertOne(credential);
        }
    }
}