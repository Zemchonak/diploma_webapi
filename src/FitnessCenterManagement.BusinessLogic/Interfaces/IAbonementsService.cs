using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessCenterManagement.BusinessLogic.Dtos;
using FitnessCenterManagement.BusinessLogic.Exceptions;

namespace FitnessCenterManagement.BusinessLogic.Interfaces
{
    public interface IAbonementsService
    {
        // VALIDATIONS

        /// <summary>
        /// Checks if the specified <see cref="AbonementDto"/> is valid.
        /// </summary>
        /// <param name="item">An <see cref="AbonementDto"/> entity to check if is valid.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception.</exception>
        void ValidateAbonement(AbonementDto item);

        /// <summary>
        /// Checks if the specified <see cref="AbonementCardDto"/> is valid.
        /// </summary>
        /// <param name="item">An <see cref="AbonementCardDto"/> entity to check if is valid.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception.</exception>
        Task ValidateAbonementCard(AbonementCardDto item);

        /// <summary>
        /// Checks if the specified <see cref="CardEventItemDto"/> is valid.
        /// </summary>
        /// <param name="item">An <see cref="CardEventItemDto"/> entity to check if is valid.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception.</exception>
        void ValidateCardEventItem(CardEventItemDto item);

        // ABONEMENT

        /// <summary>
        /// Checks if the specified <see cref="AbonementDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">An <see cref="AbonementDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateAbonementAsync(AbonementDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{AbonementDto}"/> of all the created <see cref="AbonementDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{AbonementDto}"/> of <see cref="AbonementDto"/>.</returns>
        Task<IReadOnlyCollection<AbonementDto>> GetAllAbonementsAsync();

        /// <summary>
        /// Returns an <see cref="AbonementDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="AbonementDto"/> to get.</param>
        /// <returns>An <see cref="AbonementDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<AbonementDto> GetAbonementByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="AbonementDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="AbonementDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateAbonementAsync(AbonementDto item);

        /// <summary>
        /// Ensures if the specified <see cref="AbonementDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="AbonementDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteAbonementAsync(int id);

        // ABONEMENTCARD

        /// <summary>
        /// Checks if the specified <see cref="AbonementCardDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">An <see cref="AbonementCardDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateAbonementCardAsync(AbonementCardDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{AbonementCardDto}"/> of all the created <see cref="AbonementCardDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{AbonementCardDto}"/> of <see cref="AbonementCardDto"/>.</returns>
        Task<IReadOnlyCollection<AbonementCardDto>> GetAllAbonementCardsAsync();

        /// <summary>
        /// Returns an <see cref="AbonementCardDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="AbonementCardDto"/> to get.</param>
        /// <returns>An <see cref="AbonementCardDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<AbonementCardDto> GetAbonementCardByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="AbonementCardDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="AbonementCardDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateAbonementCardAsync(AbonementCardDto item);

        /// <summary>
        /// Ensures if the specified <see cref="AbonementCardDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="AbonementCardDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteAbonementCardAsync(int id);

        // CARDEVENTITEM

        /// <summary>
        /// Checks if the specified <see cref="CardEventItemDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">A <see cref="CardEventItemDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateCardEventItemAsync(CardEventItemDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{CardEventItemDto}"/> of all the created <see cref="CardEventItemDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{CardEventItemDto}"/> of <see cref="CardEventItemDto"/>.</returns>
        Task<IReadOnlyCollection<CardEventItemDto>> GetAllCardEventItemsAsync();

        /// <summary>
        /// Returns an <see cref="CardEventItemDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="CardEventItemDto"/> to get.</param>
        /// <returns>An <see cref="CardEventItemDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<CardEventItemDto> GetCardEventItemByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="CardEventItemDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="CardEventItemDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateCardEventItemAsync(CardEventItemDto item);

        /// <summary>
        /// Ensures if the specified <see cref="CardEventItemDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="CardEventItemDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteCardEventItemAsync(int id);
    }
}
