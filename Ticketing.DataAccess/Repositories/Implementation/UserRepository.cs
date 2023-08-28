
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<User>> GetTeamMembersAsync()
        {
            return await _Context.Set<User>()
               .Where(user => user.RoleId == 3)
        .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetClientsAsync() { 
            return await _Context.Set<User>()
               .Where(user => user.RoleId == 2)
        .ToListAsync();
        }
        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _Context.Users.FirstOrDefaultAsync(u => u.UserName == username || u.Email == username || u.MobileNumber == username);
        }

        public async Task<User> GetUserByPasswordAsync(string password)
        {
            return await _Context.Users.FirstOrDefaultAsync(u => u.Password == password);
        }
    }
}