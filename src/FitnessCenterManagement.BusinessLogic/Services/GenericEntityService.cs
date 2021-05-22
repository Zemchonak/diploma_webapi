using System;
using System.Linq;
using System.Threading.Tasks;
using FitnessCenterManagement.BusinessLogic.Exceptions;
using FitnessCenterManagement.BusinessLogic.Interfaces;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.BusinessLogic.Services
{
    internal class GenericEntityService<T> : IEntityService<T>
        where T : class, IBasicEntity
    {
        private readonly IRepository<T> _repository;

        public GenericEntityService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(T item)
        {
            try
            {
                return await _repository.CreateAsync(item);
            }
            catch (ArgumentNullException)
            {
                throw new BusinessLogicException(Resources.StringRes.OperateOnNullEntityMsg);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
            {
                throw new BusinessLogicException(Resources.StringRes.NotFoundMsg);
            }

            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task UpdateAsync(T item)
        {
            try
            {
                await _repository.UpdateAsync(item);
            }
            catch (ArgumentNullException)
            {
                throw new BusinessLogicException(Resources.StringRes.NotFoundMsg);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException(BusinessLogic.Resources.StringRes.NotFoundMsg);
            }
        }
    }
}
