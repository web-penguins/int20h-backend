using System.Threading.Tasks;
using Host.Database;
using Host.Extensions;
using Host.Models;
using Host.Models.Oauth;
using Host.Responses.Oauth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MlkPwgen;
using MongoDB.Driver;

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
        public async Task<ActionResult<PostTokenResponse>> PostToken([FromBody] PostTokenModel model)
        {
            var user = await _context.Users.Find(u => u.Username == model.Login).SingleOrDefaultAsync();
            if (user == default)
            {
                _logger.LogDebug("User {0} was not found", model.Login);
                return NotFound();
            }

            var credential = await _context.Credentials.Find(c => c.Id == user.Id).SingleAsync();
            if (credential == default)
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
            } while (await _context.Sessions.Find(token).AnyAsync());
            
            await _context.Sessions.InsertOneAsync(new Session
            {
                Token = token,
                UserId = user.Id
            });

            return new PostTokenResponse
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Token = token,
                RegisterDate = user.RegisterDate,
                TotalAmountOfProducts = user.TotalAmountOfProducts
            };
        }
    }
}