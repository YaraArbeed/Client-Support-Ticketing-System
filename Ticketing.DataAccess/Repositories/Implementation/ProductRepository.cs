using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DataAccess.Models;
using Ticketing.Models.Models;

namespace Repositories.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(TicketingDbContext context) : base(context)
        {
        }
    }
}
