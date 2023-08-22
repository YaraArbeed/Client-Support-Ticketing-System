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
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository(TicketingDbContext context) : base(context)
        {
        }
    }
}
