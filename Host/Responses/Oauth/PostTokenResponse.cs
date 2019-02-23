namespace Host.Responses.Oauth
{
    public sealed class PostTokenResponse
    {
        public string Token { get; set; }
        
        public string Username { get; set; }
        
        public string Name { get; set; }
        
        public int Id { get; set; }
    }
}