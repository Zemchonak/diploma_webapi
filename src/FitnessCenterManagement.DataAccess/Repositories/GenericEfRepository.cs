using System;
using System.Linq;
using System.Threading.Tasks;
using FitnessCenterManagement.DataAccess.Contexts;
using FitnessCenterManagement.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterManagement.DataAccess.Repositories
{
    internal class GenericEfRepository<T> : IRepository<T>
        where T : class, IBasicEntity
    {
        private const string NotFound = "404";

        private readonly MainDbContext _context;

        public GenericEfRepository(MainDbContext dbContext)
        {
            _context = dbContext;
        }

        public Task<int> CreateAsync(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateAsynchronous(item);
        }

        private async Task<int> CreateAsynchronous(T item)
        {
            _context.Set<T>().Add(item);
            await _context.SaveChangesAsync();

            return item.Id;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(T item)
        {
            var entry = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == item.Id);

            if (item is null || entry is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Entry(entry).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity is null)
            {
                throw new ArgumentException(NotFound);
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}