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
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketingDbContext context) : base(context)
        {
        }
    }
}
