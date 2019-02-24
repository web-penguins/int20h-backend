using System;
using System.Collections.Generic;
using System.Net;
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
            _logger.LogInformation("List products requested");
            return await _context.Products.Find(FilterDefinition<ProductModel>.Empty).ToListAsync();
        }

        [HttpPost("create")]
        [Auth]
        public async Task<ActionResult<ProductModel>> CreateProduct([FromBody] CreateProductModel model, int userId)
        {
            _logger.LogInformation("Create product requested");
            var product = new ProductModel
            {
                Name = model.Name,
                Description = model.Description,
                Inputs = model.Inputs,
                UserId = userId,
                Outputs = model.Outputs,
                ProductId = Convert.ToInt32(_context.Products.CountDocuments(FilterDefinition<ProductModel>.Empty)) + 1
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

            var request = WebRequest.CreateHttp($"http://localhost:23000/keras/{model.ProductId}");
            request.ContentType = "multipart/form-data";

            using (var input = await request.GetRequestStreamAsync())
            {
                // await input.WriteAsync(Encoding.UTF8.GetBytes(""));
                // TODO write an image to the request stream
            }

            if (!(await request.GetResponseAsync() is HttpWebResponse response))
            {
                _logger.LogError("response is null");
                return StatusCode(500);
            }
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError("response's status code equals to {0}", response.StatusCode);
                return StatusCode(500);
            }
            
            // TODO process response
            
            return Ok();
        }
    }
}