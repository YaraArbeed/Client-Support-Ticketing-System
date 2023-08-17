using Microsoft.EntityFrameworkCore;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected TicketingDbContext _Context;
        public GenericRepository(TicketingDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return _Context.Set<T>().ToList();
        }
       
        public async Task<T> GetByIdAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _Context.Set<T>().AddAsync(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _Context.Set<T>().Remove(entity);
            await _Context.SaveChangesAsync();
        }
        public async Task<T> UpdateAsync(T entity)
        {
          _Context.Update(entity);
            await _Context.SaveChangesAsync();
            return entity;
        }
      

        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _Context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User> GetUserByPasswordAsync(string password)
        {
            return await _Context.Users.FirstOrDefaultAsync(u => u.Password == password);
        }




    }
}
