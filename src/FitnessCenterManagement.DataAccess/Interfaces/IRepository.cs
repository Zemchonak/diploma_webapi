using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessCenterManagement.DataAccess.Interfaces
{
    public interface IRepository<T>
        where T : class, IBasicEntity
    {
        /// <summary>
        /// Gets all the entities from the database.
        /// </summary>
        /// <returns>The set of the entities.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets the item from the database with the specific ID.
        /// </summary>
        /// <param name="id">The specific ID to look for.</param>
        /// <returns>The found entity.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Creates an entity in the database.
        /// </summary>
        /// <param name="item">An entity to create.</param>
        /// <exception cref="ArgumentNullException">exception.</exception>
        /// <returns>The ID of the created entity.</returns>
        Task<int> CreateAsync(T item);

        /// <summary>
        /// Updates the entity in the database.
        /// </summary>
        /// <param name="item">The entity to update.</param>
        /// <exception cref="ArgumentNullException">exception.</exception>
        Task UpdateAsync(T item);

        /// <summary>
        /// Deletes the entity in the database.
        /// </summary>
        /// <param name="id">The entity to delete.</param>
        /// <exception cref="ArgumentException">exception.</exception>
        Task DeleteAsync(int id);
    }
}
