using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Ticketing.Models.Models;

namespace Repositories.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByUserNameAsync(string username);
        Task<User> GetUserByPasswordAsync(string password);
        Task<IEnumerable<User>> GetTeamMembersAsync();
        Task<IEnumerable<User>> GetClientsAsync();
    }
}