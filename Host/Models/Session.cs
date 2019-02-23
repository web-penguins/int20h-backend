using System.ComponentModel.DataAnnotations;

namespace Host.Models
{
    public sealed class Session
    {
        [Key] public string Token { get; set; }
        
        public int UserId { get; set; }
    }
}