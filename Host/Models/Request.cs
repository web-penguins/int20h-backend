using System.Collections.Generic;

namespace Host.Models
{
    public sealed class Request
    {
        public int ProductId { get; set; }
        public Dictionary<int, string> Inputs { get; set; }
    }
}