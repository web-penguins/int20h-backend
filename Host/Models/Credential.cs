namespace Host.Models
{
    public sealed class Credential
    {
        public int Id { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string Salt { get; set; }
    }
}