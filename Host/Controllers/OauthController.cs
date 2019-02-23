using Host.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Host.Controllers
{
    [Route("oauth")]
    public sealed class OauthController : ControllerBase
    {
        private readonly Context _context;
        private readonly ILogger<OauthController> _logger;

        public OauthController(Context context, ILogger<OauthController> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}