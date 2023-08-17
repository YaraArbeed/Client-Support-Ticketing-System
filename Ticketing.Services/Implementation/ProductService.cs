using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Interface;

namespace Ticketing.Services.Implementation
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _ProductRepositry;

        public ProductService(IProductRepository productRepositry)
        {
            _ProductRepositry = productRepositry;
        }
        public async Task<IEnumerable<ProductResponse>> GetProductListAsync()
        {

            IEnumerable<Product> products = await _ProductRepositry.GetAllAsync();

            if (products.Count() == 0)
            {
                return new List<ProductResponse> { new ProductResponse { Message = "There are no products" } };
            }


            return products.Select(product => new ProductResponse { Id = product.Id, Name = product.Name });
        }
    }
}
