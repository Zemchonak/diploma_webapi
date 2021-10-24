using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FitnessCenterManagement.BusinessLogic.Dtos;
using FitnessCenterManagement.BusinessLogic.Exceptions;
using FitnessCenterManagement.BusinessLogic.Interfaces;
using FitnessCenterManagement.BusinessLogic.Resources;
using FitnessCenterManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterManagement.BusinessLogic.Services
{
    public class UsersService : IUsersService
    {
        private readonly IEntityService<CustomerCategory> _customerCategoryEntityService;

        private readonly IEntityService<Customer> _customerEntityService;

        private readonly IEntityService<Review> _reviewEntityService;

        private readonly IMapper _mapper;

        public UsersService(IEntityService<CustomerCategory> customerCategoryEntityService,
            IEntityService<Customer> customerEntityService,
            IEntityService<Review> reviewEntityService,
            IMapper mapper)
        {
            _customerCategoryEntityService = customerCategoryEntityService;
            _customerEntityService = customerEntityService;
            _reviewEntityService = reviewEntityService;
            _mapper = mapper;
        }

        private static void ValidateCustomer(CustomerDto item)
        {
            if (item is null)
            {
                throw new ValidationException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }
        }

        private static void ValidateReview(ReviewDto item)
        {
            if (item is null)
            {
                throw new ValidationException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }

            if (string.IsNullOrWhiteSpace(item.Text) || string.IsNullOrEmpty(item.Text))
            {
                throw new ValidationException(StringRes.NullTextMsg, fieldName: nameof(item.Text));
            }
        }

        // CUSTOMERCATEGORY
        public async Task<int> CreateCustomerCategoryAsync(CustomerCategoryDto item)
        {
            ValidateCustomerCategory(item);

            return await _customerCategoryEntityService.CreateAsync(_mapper.Map<CustomerCategory>(item));
        }

        public async Task<CustomerCategoryDto> GetCustomerCategoryByIdAsync(int id)
        {
            return _mapper.Map<CustomerCategoryDto>(await _customerCategoryEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<CustomerCategoryDto>> GetAllCustomerCategoriesAsync()
        {
            return _mapper.Map<IReadOnlyCollection<CustomerCategoryDto>>((await _customerCategoryEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateCustomerCategoryAsync(CustomerCategoryDto item)
        {
            ValidateCustomerCategory(item);

            await _customerCategoryEntityService.UpdateAsync(_mapper.Map<CustomerCategory>(item));
        }

        public async Task DeleteCustomerCategoryAsync(int id)
        {
            await _customerCategoryEntityService.DeleteAsync(id);
        }

        // CUSTOMER
        public async Task<int> CreateCustomerAsync(CustomerDto item)
        {
            ValidateCustomer(item);

            return await _customerEntityService.CreateAsync(_mapper.Map<Customer>(item));
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            return _mapper.Map<CustomerDto>(await _customerEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<CustomerDto>> GetAllCustomersAsync()
        {
            return _mapper.Map<IReadOnlyCollection<CustomerDto>>((await _customerEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateCustomerAsync(CustomerDto item)
        {
            ValidateCustomer(item);

            await _customerEntityService.UpdateAsync(_mapper.Map<Customer>(item));
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerEntityService.DeleteAsync(id);
        }

        // REVIEW
        public async Task<int> CreateReviewAsync(ReviewDto item)
        {
            ValidateReview(item);

            return await _reviewEntityService.CreateAsync(_mapper.Map<Review>(item));
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            return _mapper.Map<ReviewDto>(await _reviewEntityService.GetByIdAsync(id));
        }

        public ReviewDto GetReviewByAuthorIdAsync(string userId)
        {
            return _mapper.Map<ReviewDto>(_reviewEntityService.GetAll().FirstOrDefault(r => r.UserId == userId));
        }

        public async Task<IReadOnlyCollection<ReviewDto>> GetAllReviewsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<ReviewDto>>((await _reviewEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task<IReadOnlyCollection<ReviewDto>> GetAllNotHiddenReviewsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<ReviewDto>>((await _reviewEntityService.GetAll()
                .Where(r => !r.IsHidden)
                .ToListAsync()).AsReadOnly());
        }

        public async Task UpdateReviewAsync(ReviewDto item)
        {
            ValidateReview(item);

            await _reviewEntityService.UpdateAsync(_mapper.Map<Review>(item));
        }

        public async Task DeleteReviewAsync(int id)
        {
            await _reviewEntityService.DeleteAsync(id);
        }

        internal void ValidateCustomerCategory(CustomerCategoryDto item)
        {
            if (item is null)
            {
                throw new ValidationException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }

            if (_customerCategoryEntityService.GetAll().Any(i => i.Name == item.Name && i.Id != item.Id))
            {
                throw new ValidationException(StringRes.NameShouldBeUniqueMsg, fieldName: nameof(item.Name));
            }

            if (item.SaleCoefficient > Constants.CoefficientMaximum || item.SaleCoefficient < Constants.CoefficientMinimum)
            {
                throw new ValidationException(StringRes.CoefficientBoundsMsg, fieldName: nameof(item.SaleCoefficient));
            }
        }
    }
}
