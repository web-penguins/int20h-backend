using System.Collections.Generic;
using System.Threading.Tasks;
using Host.Database;
using Host.Models;
using Host.Models.Product;
using Host.MvcFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Host.Controllers
{
    [Route("product")]
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
        [Auth]
        public async Task<ActionResult<List<ProductModel>>> ListProducts()
        {
            return await _context.Products.Find(FilterDefinition<ProductModel>.Empty).ToListAsync();
        }

        [HttpPost("create")]
        [Auth]
        public async Task<ActionResult<ProductModel>> CreateProduct([FromBody] CreateProductModel model, int userId)
        {
            var product = new ProductModel
            {
                Name = model.Name,
                Description = model.Description,
                Inputs = model.Inputs,
                UserId = userId
            };
            await _context.Products.InsertOneAsync(product);
            
            return product;
        }

        [HttpPost("execute")]
        [Auth]
        public async Task<ActionResult> ExecuteProduct([FromBody] Request model)
        {
            await _context.Products.UpdateOneAsync(p => p.ProductId == model.ProductId,
                Builders<ProductModel>.Update.Inc(p => p.ExecutedTimes, 1));
            
            // TODO execute product
            
            return Ok();
        }
    }
}