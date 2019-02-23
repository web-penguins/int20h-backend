using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Host.Models
{
    public sealed class ProductModel
    {
        [Key] public int ProductId { get; set; }
        public int UserId { get; set; }
        public string InputFields { get; set; }
        public string Output { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ExecutedTimes { get; set; }
        public List<Input> Inputs { get; set; }
    }
}