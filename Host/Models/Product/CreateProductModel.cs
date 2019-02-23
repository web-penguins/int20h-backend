using System.Collections.Generic;

namespace Host.Models.Product
{
    public sealed class CreateProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Input> Inputs { get; set; }
    }
}