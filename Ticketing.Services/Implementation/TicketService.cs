using Masegat.Repository.Implementation;
using Repositories.Implementation;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.BuisinessLayer.Implementation;
using Ticketing.BuisinessLayer.Interface;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Interface;

namespace Ticketing.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _TicketRepositry;


        public TicketService(ITicketRepository TicketRepositry)
        {
            _TicketRepositry = TicketRepositry;

        }

        public async Task<IEnumerable<TicketResponse>> GetTicketListAsync()
        {
           
            IEnumerable<Ticket> tickets = await _TicketRepositry.GetAllAsync();

            if (tickets.Count() == 0)
            {
                return new List<TicketResponse> { new TicketResponse { Message = "There are no tickets" } };
            }


            return tickets.Select(ticket => new TicketResponse { Id = ticket.Id, Description = ticket.Description });
        }
    }
}
