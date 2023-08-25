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
        Task<TicketAddResponse> AddTicketAsync(TicketParam Param);
        Task<Ticket> ViewTicketAsync(int id);
        Task<Ticket> EditTicketAsync(int id, TicketEditParam Param);
        Task<IEnumerable<TicketResponse>> GetTicketListAsync();
        Task<IEnumerable<StatesResponse>> GetStatusListAsync();
        Task<IEnumerable<UserResponse>> GetTeamMemberListAsync();
        Task<bool> EditTicketManagerAsync(int asigneeId, int ticketId, int statusId);
        Task<IEnumerable<TicketResponse>> GetSupportTeamTicketListAsync(int id);
        Task<Ticket> EditTicketTeamMemberAsync(int id, TicketEditTeamMemberParam param);



    }
}
