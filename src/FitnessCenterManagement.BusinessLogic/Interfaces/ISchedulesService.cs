using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessCenterManagement.BusinessLogic.Dtos;
using FitnessCenterManagement.BusinessLogic.Exceptions;

namespace FitnessCenterManagement.BusinessLogic.Interfaces
{
    public interface ISchedulesService
    {
        // ABONEMENTFITNESSEVENT

        /// <summary>
        /// Checks if the specified <see cref="AbonementFitnessEventDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">An <see cref="AbonementFitnessEventDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateAbonementFitnessEventAsync(AbonementFitnessEventDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{AbonementFitnessEventDto}"/> of all the created <see cref="AbonementFitnessEventDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{AbonementFitnessEventDto}"/> of <see cref="AbonementFitnessEventDto"/>.</returns>
        Task<IReadOnlyCollection<AbonementFitnessEventDto>> GetAllAbonementFitnessEventsAsync();

        /// <summary>
        /// Returns an <see cref="AbonementFitnessEventDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="AbonementFitnessEventDto"/> to get.</param>
        /// <returns>An <see cref="AbonementFitnessEventDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<AbonementFitnessEventDto> GetAbonementFitnessEventByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="AbonementFitnessEventDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="AbonementFitnessEventDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateAbonementFitnessEventAsync(AbonementFitnessEventDto item);

        /// <summary>
        /// Ensures if the specified <see cref="AbonementFitnessEventDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="AbonementFitnessEventDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteAbonementFitnessEventAsync(int id);

        // WEEKLYEVENT

        /// <summary>
        /// Checks if the specified <see cref="WeeklyEventDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">A <see cref="WeeklyEventDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateWeeklyEventAsync(WeeklyEventDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{WeeklyEventDto}"/> of all the created <see cref="WeeklyEventDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{WeeklyEventDto}"/> of <see cref="WeeklyEventDto"/>.</returns>
        Task<IReadOnlyCollection<WeeklyEventDto>> GetAllWeeklyEventsAsync();

        /// <summary>
        /// Returns an <see cref="WeeklyEventDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="WeeklyEventDto"/> to get.</param>
        /// <returns>An <see cref="WeeklyEventDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<WeeklyEventDto> GetWeeklyEventByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="WeeklyEventDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="WeeklyEventDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateWeeklyEventAsync(WeeklyEventDto item);

        /// <summary>
        /// Ensures if the specified <see cref="WeeklyEventDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="WeeklyEventDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteWeeklyEventAsync(int id);

        // DATEEVENT

        /// <summary>
        /// Checks if the specified <see cref="DateEventDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">A <see cref="DateEventDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateDateEventAsync(DateEventDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{DateEventDto}"/> of all the created <see cref="DateEventDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{DateEventDto}"/> of <see cref="DateEventDto"/>.</returns>
        Task<IReadOnlyCollection<DateEventDto>> GetAllDateEventsAsync();

        /// <summary>
        /// Returns an <see cref="DateEventDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="DateEventDto"/> to get.</param>
        /// <returns>An <see cref="DateEventDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<DateEventDto> GetDateEventByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="DateEventDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="DateEventDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateDateEventAsync(DateEventDto item);

        /// <summary>
        /// Ensures if the specified <see cref="DateEventDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="DateEventDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteDateEventAsync(int id);
    }
}
