using System.Linq;
using Host.Database;
using Host.Extensions;
using Host.Models;
using Host.Models.Oauth;
using Host.Responses.Oauth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MlkPwgen;

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

        [HttpPost("token")]
        public ActionResult<PostTokenResponse> PostToken([FromBody] PostTokenModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Login);
            if (user == null)
            {
                _logger.LogDebug("User {0} was not found", model.Login);
                return NotFound();
            }

            var credential = _context.Credentials.Find(user.Id);
            if (credential == null)
            {
                _logger.LogError("Credential for user {0} was not found", user.Id);
                return StatusCode(500);
            }

            if (PasswordExtensions.HashPassword(model.Password, credential.Salt) != credential.PasswordHash)
            {
                _logger.LogDebug("Access denied for {0}", user.Id);
                return StatusCode(403);
            }

            string token;
            do
            {
                token = PasswordGenerator.Generate(100);
            } while (_context.Sessions.Find(token) != null);
            _context.Sessions.Add(new Session
            {
                Token = token,
                UserId = user.Id
            });

            _context.SaveChanges();

            return new PostTokenResponse
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Token = token
            };
        }
    }
}