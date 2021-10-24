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
    public class FitnessCatalogsService : IFitnessCatalogsService
    {
        private readonly IEntityService<FitnessEvent> _fitnesseventEntityService;

        private readonly IEntityService<Specialization> _specializationEntityService;

        private readonly IEntityService<Service> _serviceEntityService;

        private readonly IEntityService<Trainer> _trainerEntityService;

        private readonly IEntityService<Venue> _venueEntityService;

        private readonly IMapper _mapper;

        public FitnessCatalogsService(IEntityService<FitnessEvent> fitnesseventEntityService,
            IEntityService<Specialization> specializationEntityService,
            IEntityService<Service> serviceEntityService,
            IEntityService<Trainer> trainerEntityService,
            IEntityService<Venue> venueEntityService,
            IMapper mapper)
        {
            _fitnesseventEntityService = fitnesseventEntityService;
            _specializationEntityService = specializationEntityService;
            _serviceEntityService = serviceEntityService;
            _trainerEntityService = trainerEntityService;
            _venueEntityService = venueEntityService;
            _mapper = mapper;
        }

        private static void ValidateSpecialization(SpecializationDto item)
        {
            if (item is null)
            {
                throw new BusinessLogicException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }

            if (string.IsNullOrEmpty(item.Info))
            {
                throw new ValidationException(Resources.StringRes.EmptyInfoMsg, nameof(item.Info));
            }
        }

        private static void ValidateFitnessEvent(FitnessEventDto item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.Minutes < Constants.MeetingsMinutesLengthMinimum)
            {
                throw new ValidationException(Resources.StringRes.NegativeMinutesMsg, nameof(item.Minutes));
            }

            if (item.Minutes > Constants.MeetingsMinutesLengthMaximum)
            {
                throw new ValidationException(Resources.StringRes.MaximumMinutesMsg, nameof(item.Minutes));
            }
        }

        private static void ValidateTrainer(TrainerDto item)
        {
            if (item is null)
            {
                throw new BusinessLogicException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }

            if (string.IsNullOrEmpty(item.Description))
            {
                throw new ValidationException(Resources.StringRes.EmptyDescriptionMsg, nameof(item.Description));
            }
        }

        // FITNESSEVENT
        public async Task<int> CreateFitnessEventAsync(FitnessEventDto item)
        {
            ValidateFitnessEvent(item);

            return await _fitnesseventEntityService.CreateAsync(_mapper.Map<FitnessEvent>(item));
        }

        public async Task<FitnessEventDto> GetFitnessEventByIdAsync(int id)
        {
            return _mapper.Map<FitnessEventDto>(await _fitnesseventEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<FitnessEventDto>> GetAllFitnessEventsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<FitnessEventDto>>((await _fitnesseventEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateFitnessEventAsync(FitnessEventDto item)
        {
            ValidateFitnessEvent(item);

            await _fitnesseventEntityService.UpdateAsync(_mapper.Map<FitnessEvent>(item));
        }

        public async Task DeleteFitnessEventAsync(int id)
        {
            await _fitnesseventEntityService.DeleteAsync(id);
        }

        // SPECIALIZATION
        public async Task<int> CreateSpecializationAsync(SpecializationDto item)
        {
            ValidateSpecialization(item);

            return await _specializationEntityService.CreateAsync(_mapper.Map<Specialization>(item));
        }

        public async Task<SpecializationDto> GetSpecializationByIdAsync(int id)
        {
            return _mapper.Map<SpecializationDto>(await _specializationEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<SpecializationDto>> GetAllSpecializationsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<SpecializationDto>>((await _specializationEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateSpecializationAsync(SpecializationDto item)
        {
            ValidateSpecialization(item);

            await _specializationEntityService.UpdateAsync(_mapper.Map<Specialization>(item));
        }

        public async Task DeleteSpecializationAsync(int id)
        {
            await _specializationEntityService.DeleteAsync(id);
        }

        // SERVICE
        public async Task<int> CreateServiceAsync(ServiceDto item)
        {
            ValidateService(item);

            return await _serviceEntityService.CreateAsync(_mapper.Map<Service>(item));
        }

        public async Task<ServiceDto> GetServiceByIdAsync(int id)
        {
            return _mapper.Map<ServiceDto>(await _serviceEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<ServiceDto>> GetAllServicesAsync()
        {
            return _mapper.Map<IReadOnlyCollection<ServiceDto>>((await _serviceEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateServiceAsync(ServiceDto item)
        {
            ValidateService(item);

            await _serviceEntityService.UpdateAsync(_mapper.Map<Service>(item));
        }

        public async Task DeleteServiceAsync(int id)
        {
            await _serviceEntityService.DeleteAsync(id);
        }

        // TRAINER
        public async Task<int> CreateTrainerAsync(TrainerDto item)
        {
            ValidateTrainer(item);

            return await _trainerEntityService.CreateAsync(_mapper.Map<Trainer>(item));
        }

        public async Task<TrainerDto> GetTrainerByIdAsync(int id)
        {
            return _mapper.Map<TrainerDto>(await _trainerEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<TrainerDto>> GetAllTrainersAsync()
        {
            return _mapper.Map<IReadOnlyCollection<TrainerDto>>((await _trainerEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateTrainerAsync(TrainerDto item)
        {
            ValidateTrainer(item);

            await _trainerEntityService.UpdateAsync(_mapper.Map<Trainer>(item));
        }

        public async Task DeleteTrainerAsync(int id)
        {
            await _trainerEntityService.DeleteAsync(id);
        }

        // VENUE
        public async Task<int> CreateVenueAsync(VenueDto item)
        {
            await ValidateVenue(item);

            return await _venueEntityService.CreateAsync(_mapper.Map<Venue>(item));
        }

        public async Task<VenueDto> GetVenueByIdAsync(int id)
        {
            return _mapper.Map<VenueDto>(await _venueEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<VenueDto>> GetAllVenuesAsync()
        {
            return _mapper.Map<IReadOnlyCollection<VenueDto>>((await _venueEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateVenueAsync(VenueDto item)
        {
            await ValidateVenue(item);

            await _venueEntityService.UpdateAsync(_mapper.Map<Venue>(item));
        }

        public async Task DeleteVenueAsync(int id)
        {
            await _venueEntityService.DeleteAsync(id);
        }

        private Task ValidateVenue(VenueDto item)
        {
            if (item is null)
            {
                throw new ValidationException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }

            if (string.IsNullOrEmpty(item.Location))
            {
                throw new ValidationException(Resources.StringRes.EmptyAddressMsg, nameof(item.Location));
            }

            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ValidationException(Resources.StringRes.EmptyNameMsg, nameof(item.Name));
            }

            return EnsureAddressAndNameIsUnique(item);
        }

        private async Task EnsureAddressAndNameIsUnique(VenueDto item)
        {
            var venues = (await _venueEntityService.GetAll().ToListAsync()).AsReadOnly();

            if (venues.Any(v => v.Location == item.Location && v.Name == item.Name && v.Id != item.Id))
            {
                throw new ValidationException(Resources.StringRes.NotUniqueAddressNameMsg);
            }
        }

        internal void ValidateService(ServiceDto item)
        {
            if (item is null)
            {
                throw new BusinessLogicException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }

            if (item.Price < Constants.PriceMinimum)
            {
                throw new ValidationException(Resources.StringRes.NegativePriceMsg, nameof(item.Price));
            }

            if (_serviceEntityService.GetAll().Any(s => s.Name == item.Name && s.SpecializationId == item.SpecializationId && s.Id != item.Id))
            {
                throw new ValidationException(Resources.StringRes.UniqueNameForSpecialization, nameof(item.Name));
            }
        }
    }
}
