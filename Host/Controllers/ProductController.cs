using System.Collections.Generic;
using Host.Database;
using Host.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Host.Controllers
{
    [Route("neural")]
    public sealed class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly Context _context;

        public ProductController(Context context, ILogger<ProductController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("list")]
        public ActionResult<IEnumerable<Product>> ListProducts()
        {
            return _context.Products;
        }
    }
}