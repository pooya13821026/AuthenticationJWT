using Authentication.Domain.Repositories.Command.Base;
using Authentication.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infra.Repository.Command.Base
{
    public class CommandRepository<T>(ApplicationDbContext context) : ICommandRepository<T>
        where T : class
    {
        protected readonly ApplicationDbContext _context = context;

        // Insert
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Update
        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
