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
using Ticketing.DataAccess.Models;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Interface;
using Tickiting.Utility;

namespace Ticketing.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _TicketRepositry;
        private readonly IStateRepository _StateRepository;
        private readonly IUserRepository _UserRepository;
        public TicketService(ITicketRepository TicketRepositry, IStateRepository stateRepository, IUserRepository userRepository)
        {
            _TicketRepositry = TicketRepositry;
            _StateRepository = stateRepository;
            _UserRepository = userRepository;
        }

        public async Task<IEnumerable<TicketResponse>> GetTicketListByIdAsync(int id) { 

        IEnumerable<Ticket> tickets = await _TicketRepositry.GetAllByIdAsync(id);

            if (tickets.Count() == 0)
            {
             return new List<TicketResponse> { new TicketResponse { Message = "There are no tickets for this user" } };
            }


            return tickets.Select(ticket => new TicketResponse { Id = ticket.Id, Title = ticket.Title, AssigneeId = ticket.AssigneeId });
        }

        public async Task<TicketAddResponse> AddTicketAsync(TicketParam Param)
        {
            // Check if the Describtion is found
            if (Param.Description == "")
                return new TicketAddResponse { Message = "Describtion is required"};

            // Create a new Ticket object and populate its properties
            var newTicket = new Ticket
            {
                Title = Param.Title,
                Description = Param.Description,
                ProductId = Param.ProductId,
                CustomerId = Param.CustomerId,
                Attachments = Param.Attachments,
                AssigneeId=1,
                StateId=1

            };

            // Save the new Ticket to the repository
            await _TicketRepositry.AddAsync(newTicket);

            return new TicketAddResponse { Message = "Ticket added successfully!", Id = newTicket.Id,Title=newTicket.Title };
        }

        public async Task<Ticket> ViewTicketAsync(int id)
        {
            Ticket ticket = await _TicketRepositry.ViewTickeAsync(id);

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
        public async Task<IEnumerable<TicketResponse>> GetTicketListAsync()
        {
            IEnumerable<Ticket> tickets = await _TicketRepositry.GetAllAsync();

            if (tickets.Count() == 0)
            {
                return new List<TicketResponse> { new TicketResponse { Message = "There are no Tickets" } };
            }
            return tickets.Select(Ticket => new TicketResponse { Id = Ticket.Id, Title = Ticket.Title, AssigneeId = Ticket.AssigneeId });
        }

        public async Task<IEnumerable<TicketResponse>> GetSupportTeamTicketListAsync(int id)
        {
            User user = await _UserRepository.GetByIdAsync(id);

            if (user == null || user.RoleId != 3)
            {
                return new List<TicketResponse> { new TicketResponse { Message = "Only Team Members " } };
            }
            IEnumerable<Ticket> tickets = await _TicketRepositry.GetAllAsync();

            if (tickets.Count() == 0)
            {
                return new List<TicketResponse> { new TicketResponse { Message = "There are no Tickets" } };
            }

            IEnumerable<TicketResponse> ticketResponses = tickets.Where(ticket => ticket.AssigneeId== user.Id)
                .Select(ticket => new TicketResponse
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    AssigneeId = ticket.AssigneeId
                });
            if (!ticketResponses.Any())
            {
                return new List<TicketResponse> { new TicketResponse { Message = "No Tickets Assigned to the This Team Member" } };
            }
            return ticketResponses;
        }
            public async Task<IEnumerable<StatesResponse>> GetStatusListAsync()
        {
            IEnumerable<State> states = await _StateRepository.GetAllAsync();

            return states.Select(State => new StatesResponse { Id = State.Id, Name = State.Name });
        }

        public async Task<IEnumerable<UserResponse>> GetTeamMemberListAsync()
        {
            IEnumerable<User> users = await _UserRepository.GetTeamMembersAsync();


            if (users.Count() == 0)
            {
                return new List<UserResponse> { new UserResponse { Message = "There are no users of type Team Member" } };
            }
            return users.Select(User => new UserResponse { Id = User.Id,RoleId=User.RoleId, UserName = User.UserName,MobileNumber=User.MobileNumber,Email=User.Email });
        }

        public async Task<bool> EditTicketManagerAsync(int asigneeId, int ticketId,int statusId)
        {
            // Retrieve the user from the repository by ID
            var user = await _UserRepository.GetByIdAsync(asigneeId);
            if (user == null)
            {
                return false;
            }

            // Retrieve the team members from the repository
            var teamMembers = await _UserRepository.GetTeamMembersAsync();

            // Check if the user is a team member
            if (!teamMembers.Any(u => u.Id == user.Id))
            {
                return false;
            }
            // Retrieve the ticket from the repository by ID
            var ticket = await _TicketRepositry.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }
            // Update the ticket's assignee ID in the repository
            ticket.AssigneeId = user.Id;
            ticket.StateId = statusId;

            // Save changes to the database
            await _TicketRepositry.SaveAsync();

            return true;
        }

        public async Task<Ticket> EditTicketTeamMemberAsync(int id, TicketEditTeamMemberParam param)
        {
            Ticket ticket = await _TicketRepositry.GetByIdAsync(id);

            if (ticket == null)
            {
                var message = $"No ticket was found with ID: {id}";
                ticket = new Ticket { Description = message };
                return ticket;
            }

            // Update the ticket entity with the properties from the ticketParam model
            ticket.Title = param.Title;
            ticket.Description = param.Description;
            ticket.ProductId = param.ProductId;
            ticket.StateId=param.StateId;

            await _TicketRepositry.UpdateAsync(ticket);
            return ticket;
        }
    }
}
