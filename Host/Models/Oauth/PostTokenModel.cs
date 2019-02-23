using System.ComponentModel.DataAnnotations;

namespace Host.Models.Oauth
{
    public sealed class PostTokenModel
    {
        [Required, MinLength(5), MaxLength(60)]
        public string Username { get; set; }
        
        [Required, MinLength(8), MaxLength(64)]
        public string Password { get; set; }
    }
}