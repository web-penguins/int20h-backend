namespace Host.Models
{
    public sealed class Result
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ResultType { get; set; }
        public string ResultValue { get; set; }
    }
}