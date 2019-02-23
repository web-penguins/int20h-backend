namespace Host.Models
{
    public sealed class User
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Username { get; set; }
        
        public string RegisterDate { get; set; }
        
        public int TotalAmountOfProducts { get; set; }
    }
}