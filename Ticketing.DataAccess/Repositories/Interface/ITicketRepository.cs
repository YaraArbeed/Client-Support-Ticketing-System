using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Models.Models;

namespace Repositories.Interface
{
    public interface ITicketRepository:IGenericRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetAllByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetAllByIdTeamMemberAsync(int id);
        Task<Ticket> ViewTickeAsync(int id);
       
    }
}
