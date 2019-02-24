using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Host.Models
{
    public sealed class ProductModel
    {
        [BsonId] public int ProductId { get; set; }
        public int UserId { get; set; }
        [BsonIgnoreIfNull] public string InputFields { get; set; }
        [BsonIgnoreIfNull] public string Output { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [BsonIgnoreIfDefault] public int ExecutedTimes { get; set; }
        public List<Input> Inputs { get; set; }
    }
}