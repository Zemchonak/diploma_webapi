using System;
using System.Linq;
using System.Threading.Tasks;
using FitnessCenterManagement.BusinessLogic.Exceptions;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.BusinessLogic.Interfaces
{
    /// <summary>
    /// An interface that defines basic CRUD operations methods for entity <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The class that implements <see cref="IBasicEntity"/>.
    /// </typeparam>
    public interface IEntityService<T>
        where T : class, IBasicEntity
    {
        /// <summary>
        /// Creates an entity in the database.
        /// </summary>
        /// <param name="item">An entity to create.</param>
        /// <exception cref="ArgumentNullException">exception.</exception>
        /// <returns>The ID of the created entity.</returns>
        Task<int> CreateAsync(T item);

        /// <summary>
        /// Gets the item from the database with the specific ID.
        /// </summary>
        /// <param name="id">The specific ID to look for.</param>
        /// <returns>The found entity.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Gets all the entities from the database.
        /// </summary>
        /// <returns>The set of the entities.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Ensures the entity exists and updates it in the database.
        /// </summary>
        /// <param name="item">The entity to update.</param>
        /// <exception cref="BusinessLogicException">exception.</exception>
        Task UpdateAsync(T item);

        /// <summary>
        /// Ensures the entity exists and deletes it from the database.
        /// </summary>
        /// <param name="id">The entity to delete.</param>
        /// <exception cref="BusinessLogicException">exception.</exception>
        Task DeleteAsync(int id);
    }
}
