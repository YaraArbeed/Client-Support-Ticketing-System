using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;

namespace Ticketing.Services.Interface
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketResponse>> GetTicketListByIdAsync(int id);
        Task<TicketResponse> AddTicketAsync(TicketParam Param);
        Task<Ticket> ViewTicketAsync(int id);
        Task<Ticket> EditTicketAsync(int id, TicketEditParam Param);
        Task<IEnumerable<TicketResponse>> GetTicketListAsync();
        Task<IEnumerable<StatesResponse>> GetStatusListAsync();
        Task<IEnumerable<UserResponse>> GetTeamMemberListAsync();
        Task<bool> EditTicketManagerAsync(int userId, int ticketId);


    }
}
