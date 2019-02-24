using MongoDB.Bson.Serialization.Attributes;

namespace Host.Models
{
    public sealed class Session
    {
        [BsonId] public string Token { get; set; }
        
        public int UserId { get; set; }
    }
}