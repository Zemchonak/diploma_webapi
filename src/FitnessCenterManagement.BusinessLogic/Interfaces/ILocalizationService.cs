using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessCenterManagement.BusinessLogic.Dtos;

namespace FitnessCenterManagement.BusinessLogic.Interfaces
{
    public interface ILocalizationService
    {
        /// <summary>
        /// Returns the default value of <see cref="LanguageDto"/> if it wasn't specified for user.
        /// </summary>
        /// <returns>An <see cref="LanguageDto"/> object.</returns>
        LanguageDto DefaultLanguage { get; }

        /// <summary>
        /// Returns an <see cref="LanguageDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="LanguageDto"/> to get.</param>
        /// <returns>An <see cref="LanguageDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<LanguageDto> GetByIdAsync(int id);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{LanguageDto}"/> of all the <see cref="LanguageDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{LanguageDto}"/> of <see cref="LanguageDto"/>.</returns>
        Task<IReadOnlyCollection<LanguageDto>> GetAllAsync();
    }
}
