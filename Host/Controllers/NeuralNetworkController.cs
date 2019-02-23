using Host.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Host.Controllers
{
    [Route("neural")]
    public sealed class NeuralNetworkController : ControllerBase
    {
        private readonly ILogger<NeuralNetworkController> _logger;
        private readonly Context _context;

        public NeuralNetworkController(Context context, ILogger<NeuralNetworkController> logger)
        {
            _logger = logger;
            _context = context;
        }
    }
}