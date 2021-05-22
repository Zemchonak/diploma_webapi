using System;
using System.Collections.Generic;
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
    public class AbonementsService : IAbonementsService
    {
        private readonly IEntityService<Abonement> _abonementEntityService;

        private readonly IEntityService<AbonementCard> _abonementCardEntityService;

        private readonly IEntityService<CardEventItem> _cardEventItemEntityService;

        private readonly IMapper _mapper;

        public AbonementsService(IEntityService<Abonement> abonementEntityService,
            IEntityService<AbonementCard> abonementCardEntityService,
            IEntityService<CardEventItem> cardEventItemEntityService,
            IMapper mapper)
        {
            _abonementEntityService = abonementEntityService;
            _abonementCardEntityService = abonementCardEntityService;
            _cardEventItemEntityService = cardEventItemEntityService;
            _mapper = mapper;
        }

        private static void ValidateAbonement(AbonementDto item)
        {
            if (item is null)
            {
                throw new BusinessLogicException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }

            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ValidationException(Resources.StringRes.EmptyNameMsg, nameof(item.Name));
            }

            if (item.Attendances < Constants.AbonementAttendancesMinimum)
            {
                throw new ValidationException(Resources.StringRes.NegativeAttendancesMsg, nameof(item.Attendances));
            }

            if (item.Coefficient > Constants.CoefficientMaximum || item.Coefficient < Constants.CoefficientMinimum)
            {
                throw new ValidationException(StringRes.CoefficientBoundsMsg, fieldName: nameof(item.Coefficient));
            }
        }

        private static void ValidateCardEventItem(CardEventItemDto item)
        {
            if (item is null)
            {
                throw new BusinessLogicException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }
        }

        // ABONEMENT
        public async Task<int> CreateAbonementAsync(AbonementDto item)
        {
            ValidateAbonement(item);

            return await _abonementEntityService.CreateAsync(_mapper.Map<Abonement>(item));
        }

        public async Task<AbonementDto> GetAbonementByIdAsync(int id)
        {
            return _mapper.Map<AbonementDto>(await _abonementEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<AbonementDto>> GetAllAbonementsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<AbonementDto>>((await _abonementEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateAbonementAsync(AbonementDto item)
        {
            ValidateAbonement(item);

            await _abonementEntityService.UpdateAsync(_mapper.Map<Abonement>(item));
        }

        public async Task DeleteAbonementAsync(int id)
        {
            await _abonementEntityService.DeleteAsync(id);
        }

        // ABONEMENTCARD
        public async Task<int> CreateAbonementCardAsync(AbonementCardDto item)
        {
            await ValidateAbonementCard(item);

            return await _abonementCardEntityService.CreateAsync(_mapper.Map<AbonementCard>(item));
        }

        public async Task<AbonementCardDto> GetAbonementCardByIdAsync(int id)
        {
            return _mapper.Map<AbonementCardDto>(await _abonementCardEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<AbonementCardDto>> GetAllAbonementCardsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<AbonementCardDto>>((await _abonementCardEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateAbonementCardAsync(AbonementCardDto item)
        {
            await ValidateAbonementCard(item);

            await _abonementCardEntityService.UpdateAsync(_mapper.Map<AbonementCard>(item));
        }

        public async Task DeleteAbonementCardAsync(int id)
        {
            await _abonementCardEntityService.DeleteAsync(id);
        }

        // CARDEVENTITEM
        public async Task<int> CreateCardEventItemAsync(CardEventItemDto item)
        {
            ValidateCardEventItem(item);

            return await _cardEventItemEntityService.CreateAsync(_mapper.Map<CardEventItem>(item));
        }

        public async Task<CardEventItemDto> GetCardEventItemByIdAsync(int id)
        {
            return _mapper.Map<CardEventItemDto>(await _cardEventItemEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<CardEventItemDto>> GetAllCardEventItemsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<CardEventItemDto>>((await _cardEventItemEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateCardEventItemAsync(CardEventItemDto item)
        {
            ValidateCardEventItem(item);

            await _cardEventItemEntityService.UpdateAsync(_mapper.Map<CardEventItem>(item));
        }

        public async Task DeleteCardEventItemAsync(int id)
        {
            await _cardEventItemEntityService.DeleteAsync(id);
        }

        private Task ValidateAbonementCard(AbonementCardDto item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.Visits < Constants.VisitsMinimum)
            {
                throw new ValidationException(Resources.StringRes.NegativeVisitsCountMsg, nameof(item.Visits));
            }

            if (item.PurchaseDate > DateTimeOffset.Now)
            {
                throw new ValidationException(Resources.StringRes.DateInTheFutureMsg, nameof(item.PurchaseDate));
            }

            return EnsureVisitsLessThanAbonementAttendances(item);
        }

        private async Task EnsureVisitsLessThanAbonementAttendances(AbonementCardDto item)
        {
            var abonement = await _abonementEntityService.GetByIdAsync(item.Id);

            if (item.Visits > abonement.Attendances)
            {
                throw new ValidationException(Resources.StringRes.VisitsMoreThanAbonementAttendancesMsg, nameof(item.Visits));
            }
        }
    }
}
