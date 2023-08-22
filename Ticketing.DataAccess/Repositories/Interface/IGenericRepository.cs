using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Models.Models;

namespace Repositories.Interface
{
    //accept any type of class
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<User> GetUserByUserNameAsync(string username);
        Task<User> GetUserByPasswordAsync(string password);
        Task<T> UpdateAsync(T entity);
        Task SaveAsync();





    }
}
