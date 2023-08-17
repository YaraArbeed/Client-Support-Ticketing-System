using Microsoft.AspNetCore.Mvc;
using Ticketing.Services.Implementation;
using Ticketing.Services.Interface;

namespace Ticketing.API.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }
        [HttpGet("Get all Products")]
        public async Task<IActionResult> GetProductList()
        {
            var productResponses = await _ProductService.GetProductListAsync();

            return Ok(productResponses);
        }
    }
}
