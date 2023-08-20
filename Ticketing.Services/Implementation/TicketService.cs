using Masegat.Repository.Implementation;
using Repositories.Implementation;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Ticketing.BuisinessLayer.Implementation;
using Ticketing.BuisinessLayer.Interface;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Interface;
using Tickiting.Utility;

namespace Ticketing.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _TicketRepositry;
       

        public TicketService(ITicketRepository TicketRepositry)
        {
            _TicketRepositry = TicketRepositry;
           
        }

       public async Task<IEnumerable<TicketResponse>> GetTicketListByIdAsync(int id) { 

        IEnumerable<Ticket> tickets = await _TicketRepositry.GetAllByIdAsync(id);

            if (tickets.Count() == 0)
            {
             return new List<TicketResponse> { new TicketResponse { Message = "There are no tickets for this user" } };
            }


            return tickets.Select(ticket => new TicketResponse { Id = ticket.Id, Title = ticket.Title});
        }

        public async Task<TicketResponse> AddTicketAsync(TicketParam Param)
        {
            // Check if the Describtion is found
            if (Param.Description == "")
                return new TicketResponse{ Message = "Describtion is required"};

            // Create a new Ticket object and populate its properties
            var newTicket = new Ticket
            {

                Title = Param.Title,
                Description = Param.Description,
                Status = Param.Status,
                AssigneeId = Param.AssigneeId,
                ProductId = Param.ProductId,
                CustomerId = Param.CustomerId,
                Attachments = Param.Attachments


            };

            // Save the new Ticket to the repository
            await _TicketRepositry.AddAsync(newTicket);

            return new TicketResponse { Message = "Ticket added successfully!", Id = newTicket.Id };


        }

        public async Task<Ticket> ViewTicketAsync(int id)
        {
            Ticket ticket=await _TicketRepositry.GetByIdAsync(id);
            if (ticket == null)
            {
                var message = "No Ticket found for this Id";
                ticket = new Ticket { Description = message };
            }
            return ticket;
        }

        public async Task<Ticket> EditTicketAsync(int id, TicketEditParam Param)
        {
            Ticket ticket = await _TicketRepositry.GetByIdAsync(id);

            if (ticket == null)
            {
                var message = $"No ticket was found with ID: {id}";
                ticket = new Ticket { Description = message };
                return ticket;
            }
            

            // Update the ticket entity with the properties from the ticketParam model
            ticket.Title = Param.Title;
            ticket.Description = Param.Description;
            ticket.ProductId = Param.ProductId;

           await _TicketRepositry.UpdateAsync(ticket);
            return ticket;
        }

        public Task<IEnumerable<TicketResponse>> GetTicketListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
