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

        public async Task<IEnumerable<Ticket>> GetAllByIdTeamMemberAsync(int id)
        {
            return await _Context.Set<Ticket>()
                .Where(ticket => ticket.AssigneeId == id)
                .ToListAsync();
        }
        
        public async Task<Ticket> ViewTickeAsync(int id)
        {
            return await _Context.Tickets
                .Where(t => t.Id == id)
                .Select(t => new Ticket
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    StateId = t.StateId,
                    AssigneeId = t.AssigneeId,
                    ProductId = t.ProductId,
                    CustomerId = t.CustomerId,
                    Attachments = t.Attachments,
                    Assignee = t.Assignee,
                    Product = t.Product,
                    Customer = t.Customer,
                    State = t.State
                })
                .FirstOrDefaultAsync();
        }
    }
}
