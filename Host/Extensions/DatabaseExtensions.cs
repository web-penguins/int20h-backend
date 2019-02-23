using System.Linq;
using Host.Database;
using Host.Models;
using MlkPwgen;

namespace Host.Extensions
{
    public static class DatabaseExtensions
    {
        public static void AddDefaultData(this Context context)
        {
            if (context.Users.Any()) return;
            
            context.Users.Add(new User
            {
                Id = 1,
                Name = "Viacheslav Zhuravskyi",
                Username = "lundibundi"
            });
            
            var credential = new Credential
            {
                Id = 1,
                Salt = PasswordGenerator.Generate(4)
            };
            credential.PasswordHash = PasswordExtensions.HashPassword("jerboa666", credential.Salt);
            context.Credentials.Add(credential);
        }
    }
}