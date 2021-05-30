using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessCenterManagement.BusinessLogic.Dtos;
using FitnessCenterManagement.BusinessLogic.Exceptions;

namespace FitnessCenterManagement.BusinessLogic.Interfaces
{
    public interface IUsersService
    {
        // CUSTOMERCATEGORY

        /// <summary>
        /// Checks if the specified <see cref="CustomerCategoryDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">A <see cref="CustomerCategoryDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateCustomerCategoryAsync(CustomerCategoryDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{CustomerCategoryDto}"/> of all the created <see cref="CustomerCategoryDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{CustomerCategoryDto}"/> of <see cref="CustomerCategoryDto"/>.</returns>
        Task<IReadOnlyCollection<CustomerCategoryDto>> GetAllCustomerCategoriesAsync();

        /// <summary>
        /// Returns an <see cref="CustomerCategoryDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="CustomerCategoryDto"/> to get.</param>
        /// <returns>An <see cref="CustomerCategoryDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<CustomerCategoryDto> GetCustomerCategoryByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="CustomerCategoryDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">An <see cref="CustomerCategoryDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateCustomerCategoryAsync(CustomerCategoryDto item);

        /// <summary>
        /// Ensures if the specified <see cref="CustomerCategoryDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="CustomerCategoryDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteCustomerCategoryAsync(int id);

        // CUSTOMER

        /// <summary>
        /// Checks if the specified <see cref="CustomerDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">A <see cref="CustomerDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateCustomerAsync(CustomerDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{CustomerDto}"/> of all the created <see cref="CustomerDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{CustomerDto}"/> of <see cref="CustomerDto"/>.</returns>
        Task<IReadOnlyCollection<CustomerDto>> GetAllCustomersAsync();

        /// <summary>
        /// Returns an <see cref="CustomerDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="CustomerDto"/> to get.</param>
        /// <returns>An <see cref="CustomerDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<CustomerDto> GetCustomerByIdAsync(int id);

        /// <summary>
        /// Checks if the specified <see cref="CustomerDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">A <see cref="CustomerDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateCustomerAsync(CustomerDto item);

        /// <summary>
        /// Ensures if the specified <see cref="CustomerDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="CustomerDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteCustomerAsync(int id);

        // REVIEW

        /// <summary>
        /// Checks if the specified <see cref="ReviewDto"/> is valid and if it's true - creates it.
        /// </summary>
        /// <param name="item">A <see cref="ReviewDto"/> to create.</param>
        /// <returns>The created item's ID.</returns>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        Task<int> CreateReviewAsync(ReviewDto item);

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{ReviewDto}"/> of all the created <see cref="ReviewDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{ReviewDto}"/> of <see cref="ReviewDto"/>.</returns>
        Task<IReadOnlyCollection<ReviewDto>> GetAllReviewsAsync();

        /// <summary>
        /// Returns an <see cref="IReadOnlyCollection{ReviewDto}"/> of all the created NOT HIDDEN <see cref="ReviewDto"/>.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyCollection{ReviewDto}"/> of <see cref="ReviewDto"/>.</returns>
        Task<IReadOnlyCollection<ReviewDto>> GetAllNotHiddenReviewsAsync();

        /// <summary>
        /// Returns an <see cref="ReviewDto"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="ReviewDto"/> to get.</param>
        /// <returns>An <see cref="ReviewDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task<ReviewDto> GetReviewByIdAsync(int id);

        /// <summary>
        /// Returns an <see cref="ReviewDto"/> with the specified author's ID.
        /// </summary>
        /// <param name="userId">The author's ID of the <see cref="ReviewDto"/> to get.</param>
        /// <returns>A <see cref="ReviewDto"/> object.</returns>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        ReviewDto GetReviewByAuthorIdAsync(string userId);

        /// <summary>
        /// Checks if the specified <see cref="ReviewDto"/> is valid and if it's true - updates it.
        /// </summary>
        /// <param name="item">A <see cref="ReviewDto"/> entity to update.</param>
        /// <exception cref="ValidationException">Validation failure exception.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task UpdateReviewAsync(ReviewDto item);

        /// <summary>
        /// Ensures if the specified <see cref="ReviewDto"/> exists and if it's true - deletes it.
        /// </summary>
        /// <param name="id">The ID of the <see cref="ReviewDto"/> object to delete.</param>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">.</exception>
        /// <exception cref="BusinessLogic.Exceptions.BusinessLogicException">Business logic exception in case of the specified item wasn't found.</exception>
        Task DeleteReviewAsync(int id);
    }
}
