using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Ticket>> GetAllByIdAsync(int id)
        {
            return await _Context.Set<Ticket>()
                .Where(ticket => ticket.CustomerId == id)
                .ToListAsync();
        }
    }
}
