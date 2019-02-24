using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Host.Models.Product
{
    public sealed class CreateProductModel
    {
        [Required, MinLength(5), MaxLength(60)]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = true), MaxLength(200)]
        public string Description { get; set; }
        
        [Required]
        public List<Input> Inputs { get; set; }
        
        [Required]
        public List<Output> Outputs { get; set; }
    }
}