using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessCenterManagement.BusinessLogic.Dtos;
using FitnessCenterManagement.BusinessLogic.Exceptions;

namespace FitnessCenterManagement.BusinessLogic.Interfaces
{
    public interface IFitnessCatalogsService
    {
        // FITNESSEVENT

        /// <summary>
        /// Checks if the specified <see cref="FitnessEventDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">A <see cref="FitnessEventDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateFitnessEventAsync(FitnessEventDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{FitnessEventDto}"/> of all the created <see cref="FitnessEventDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{FitnessEventDto}"/> of <see cref="FitnessEventDto"/>.</returns>
        Task<IReadOnlyCollection<FitnessEventDto>> GetAllFitnessEventsAsync();

        /// <summary>
        /// Returns an <see cref="FitnessEventDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="FitnessEventDto"/> to get.</param>
        /// <returns>An <see cref="FitnessEventDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<FitnessEventDto> GetFitnessEventByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="FitnessEventDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="FitnessEventDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateFitnessEventAsync(FitnessEventDto item);

        /// <summary>
        /// Ensures if the specified <see cref="FitnessEventDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="FitnessEventDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteFitnessEventAsync(int id);

        // SPECIALIZATION

        /// <summary>
        /// Checks if the specified <see cref="SpecializationDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">A <see cref="SpecializationDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateSpecializationAsync(SpecializationDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{SpecializationDto}"/> of all the created <see cref="SpecializationDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{SpecializationDto}"/> of <see cref="SpecializationDto"/>.</returns>
        Task<IReadOnlyCollection<SpecializationDto>> GetAllSpecializationsAsync();

        /// <summary>
        /// Returns an <see cref="SpecializationDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="SpecializationDto"/> to get.</param>
        /// <returns>An <see cref="SpecializationDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<SpecializationDto> GetSpecializationByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="SpecializationDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="SpecializationDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateSpecializationAsync(SpecializationDto item);

        /// <summary>
        /// Ensures if the specified <see cref="SpecializationDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="SpecializationDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteSpecializationAsync(int id);

        // SERVICE

        /// <summary>
        /// Checks if the specified <see cref="ServiceDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">A <see cref="ServiceDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateServiceAsync(ServiceDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{ServiceDto}"/> of all the created <see cref="ServiceDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{ServiceDto}"/> of <see cref="ServiceDto"/>.</returns>
        Task<IReadOnlyCollection<ServiceDto>> GetAllServicesAsync();

        /// <summary>
        /// Returns an <see cref="ServiceDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="ServiceDto"/> to get.</param>
        /// <returns>An <see cref="ServiceDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<ServiceDto> GetServiceByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="ServiceDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="ServiceDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateServiceAsync(ServiceDto item);

        /// <summary>
        /// Ensures if the specified <see cref="ServiceDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="ServiceDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteServiceAsync(int id);

        // VENUE

        /// <summary>
        /// Checks if the specified <see cref="VenueDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">A <see cref="VenueDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateVenueAsync(VenueDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{VenueDto}"/> of all the created <see cref="VenueDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{VenueDto}"/> of <see cref="VenueDto"/>.</returns>
        Task<IReadOnlyCollection<VenueDto>> GetAllVenuesAsync();

        /// <summary>
        /// Returns an <see cref="VenueDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="VenueDto"/> to get.</param>
        /// <returns>An <see cref="VenueDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<VenueDto> GetVenueByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="VenueDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="VenueDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateVenueAsync(VenueDto item);

        /// <summary>
        /// Ensures if the specified <see cref="VenueDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="VenueDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteVenueAsync(int id);

        // TRAINER

        /// <summary>
        /// Checks if the specified <see cref="TrainerDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">A <see cref="TrainerDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateTrainerAsync(TrainerDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{TrainerDto}"/> of all the created <see cref="TrainerDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{TrainerDto}"/> of <see cref="TrainerDto"/>.</returns>
        Task<IReadOnlyCollection<TrainerDto>> GetAllTrainersAsync();

        /// <summary>
        /// Returns an <see cref="TrainerDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="TrainerDto"/> to get.</param>
        /// <returns>An <see cref="TrainerDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<TrainerDto> GetTrainerByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="TrainerDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="TrainerDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateTrainerAsync(TrainerDto item);

        /// <summary>
        /// Ensures if the specified <see cref="TrainerDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="TrainerDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteTrainerAsync(int id);
    }
}
