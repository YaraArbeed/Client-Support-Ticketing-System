using Microsoft.AspNetCore.Mvc;
using Ticketing.Services.Implementation;
using Ticketing.Services.Interface;

namespace Ticketing.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        /// <summary>
        /// Retrieve all products
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productResponses = await _ProductService.GetProductListAsync();

            return Ok(productResponses);
        }
    }
}
