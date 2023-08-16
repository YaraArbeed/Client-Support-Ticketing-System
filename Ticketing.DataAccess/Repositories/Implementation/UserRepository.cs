
using Repositories.Implementation;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Ticketing.DataAccess.Models;
using Ticketing.Models.Models;

namespace Masegat.Repository.Implementation
{
    public class UserRepository :GenericRepository<User>, IUserRepository
    {
        public UserRepository(TicketingDbContext context) : base(context)
        {
        }
    }
}