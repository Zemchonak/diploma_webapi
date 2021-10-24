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
    public class SchedulesService : ISchedulesService
    {
        private readonly IEntityService<AbonementFitnessEvent> _abonementFitnessEventEntityService;

        private readonly IEntityService<DateEvent> _dateEventEntityService;

        private readonly IEntityService<WeeklyEvent> _weeklyEventEntityService;

        private readonly IEntityService<FitnessEvent> _fitnessEventEntityService;

        private readonly IMapper _mapper;

        public SchedulesService(
            IEntityService<AbonementFitnessEvent> abonementFitnesseventEntityService,
            IEntityService<DateEvent> dateEventEntityService,
            IEntityService<WeeklyEvent> weeklyEventEntityService,
            IEntityService<FitnessEvent> fitnessEventEntityService,
            IMapper mapper)
        {
            _abonementFitnessEventEntityService = abonementFitnesseventEntityService;
            _dateEventEntityService = dateEventEntityService;
            _weeklyEventEntityService = weeklyEventEntityService;
            _fitnessEventEntityService = fitnessEventEntityService;
            _mapper = mapper;
        }

        private static bool CheckIfAnyTimeCrosses(DateTimeOffset first, int firstLength, DateTimeOffset second)
        {
            return first < second && first.AddMinutes(firstLength) > second;
        }

        internal async Task ValidateAbonementFitnessEvent(AbonementFitnessEventDto item)
        {
            if (item is null)
            {
                throw new ValidationException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }

            if (await _abonementFitnessEventEntityService.GetAll().AnyAsync(i => i.Id != item.Id
                        && i.AbonementId == item.AbonementId
                        && i.FitnessEventId == item.FitnessEventId))
            {
                throw new ValidationException(StringRes.AlreadyExistsMsg);
            }
        }

        // ABONEMENTFITNESSEVENT
        public async Task<int> CreateAbonementFitnessEventAsync(AbonementFitnessEventDto item)
        {
            await ValidateAbonementFitnessEvent(item);

            return await _abonementFitnessEventEntityService.CreateAsync(_mapper.Map<AbonementFitnessEvent>(item));
        }

        public async Task<IReadOnlyCollection<AbonementFitnessEventDto>> GetAbonementFitnessEventsByAbonementIdAsync(int abonementId)
        {
            return _mapper.Map<IReadOnlyCollection<AbonementFitnessEventDto>>(
                (await _abonementFitnessEventEntityService.GetAll().Where(a => a.AbonementId == abonementId).ToListAsync())
                .AsReadOnly());
        }

        public async Task<AbonementFitnessEventDto> GetAbonementFitnessEventByIdAsync(int id)
        {
            return _mapper.Map<AbonementFitnessEventDto>(await _abonementFitnessEventEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<AbonementFitnessEventDto>> GetAllAbonementFitnessEventsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<AbonementFitnessEventDto>>((await _abonementFitnessEventEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateAbonementFitnessEventAsync(AbonementFitnessEventDto item)
        {
            await ValidateAbonementFitnessEvent(item);

            await _abonementFitnessEventEntityService.UpdateAsync(_mapper.Map<AbonementFitnessEvent>(item));
        }

        public async Task DeleteAbonementFitnessEventAsync(int id)
        {
            await _abonementFitnessEventEntityService.DeleteAsync(id);
        }

        // DATEEVENT
        public async Task<int> CreateDateEventAsync(DateEventDto item)
        {
            await ValidateDateEvent(item);

            return await _dateEventEntityService.CreateAsync(_mapper.Map<DateEvent>(item));
        }

        public async Task<DateEventDto> GetDateEventByIdAsync(int id)
        {
            return _mapper.Map<DateEventDto>(await _dateEventEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<DateEventDto>> GetAllDateEventsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<DateEventDto>>((await _dateEventEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateDateEventAsync(DateEventDto item)
        {
            await ValidateDateEvent(item);

            await _dateEventEntityService.UpdateAsync(_mapper.Map<DateEvent>(item));
        }

        public async Task DeleteDateEventAsync(int id)
        {
            await _dateEventEntityService.DeleteAsync(id);
        }

        // WEEKLYEVENT
        public async Task<int> CreateWeeklyEventAsync(WeeklyEventDto item)
        {
            await ValidateWeeklyEvent(item);

            return await _weeklyEventEntityService.CreateAsync(_mapper.Map<WeeklyEvent>(item));
        }

        public async Task<WeeklyEventDto> GetWeeklyEventByIdAsync(int id)
        {
            return _mapper.Map<WeeklyEventDto>(await _weeklyEventEntityService.GetByIdAsync(id));
        }

        public async Task<IReadOnlyCollection<WeeklyEventDto>> GetAllWeeklyEventsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<WeeklyEventDto>>((await _weeklyEventEntityService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task UpdateWeeklyEventAsync(WeeklyEventDto item)
        {
            await ValidateWeeklyEvent(item);

            await _weeklyEventEntityService.UpdateAsync(_mapper.Map<WeeklyEvent>(item));
        }

        public async Task DeleteWeeklyEventAsync(int id)
        {
            await _weeklyEventEntityService.DeleteAsync(id);
        }

        internal async Task ValidateDateEvent(DateEventDto item)
        {
            if (item is null)
            {
                throw new BusinessLogicException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }

            var weeklyEvent = await _weeklyEventEntityService.GetByIdAsync(item.Id);

            if ((int) item.DateTime.DayOfWeek != weeklyEvent.DayOfWeek)
            {
                throw new BusinessLogicException(StringRes.WrongDayOfWeekMsg, fieldName: nameof(item));
            }

            await EnsureDatesNotCrossing(item);
        }

        private Task ValidateWeeklyEvent(WeeklyEventDto item)
        {
            if (item is null)
            {
                throw new BusinessLogicException(StringRes.NullEntityMsg, new ArgumentNullException(nameof(item)));
            }

            item.Time = new DateTime(1, 1, 1, item.Time.Hour, item.Time.Minute, 0);

            if (item.DayOfWeek < Constants.MinDayOfWeek || item.DayOfWeek > Constants.MaxDayOfWeek)
            {
                throw new BusinessLogicException(StringRes.IncorrectDayOfWeekMsg, nameof(item.DayOfWeek));
            }

            if (item.VisitorCapacity < Constants.MinVisitorCapacity)
            {
                throw new BusinessLogicException(StringRes.IncorrectDayOfWeekMsg, nameof(item.DayOfWeek));
            }

            return EnsureNotCrossing(item);
        }

        private async Task EnsureDatesNotCrossing(DateEventDto item)
        {
            var eventsWithTheSameTrainer = await _dateEventEntityService.GetAll()
                .Where(e => e.Id != item.Id && e.TrainerId == item.TrainerId).ToListAsync();

            var itemWeeklyEvent = await _weeklyEventEntityService.GetByIdAsync(item.WeeklyEventId);
            var itemFitnessEvent = await _fitnessEventEntityService.GetByIdAsync(itemWeeklyEvent.FitnessEventId);

            foreach (var one in eventsWithTheSameTrainer)
            {
                var weeklyEvent = await _weeklyEventEntityService.GetByIdAsync(one.WeeklyEventId);
                var fitnessEvent = await _fitnessEventEntityService.GetByIdAsync(weeklyEvent.FitnessEventId);

                if (one.DateTime < item.DateTime ?
                    CheckIfAnyTimeCrosses(one.DateTime, fitnessEvent.Minutes, item.DateTime) :
                    CheckIfAnyTimeCrosses(item.DateTime, itemFitnessEvent.Minutes, one.DateTime))
                {
                    throw new BusinessLogicException(StringRes.TimeCrossesMsg, fieldName: nameof(item.DateTime));
                }
            }
        }

        private async Task EnsureNotCrossing(WeeklyEventDto item)
        {
            var itemfitnessEvent = await _fitnessEventEntityService.GetByIdAsync(item.FitnessEventId);

            // select every weekly event with the same day
            var weeklyEventsSameDay = await _weeklyEventEntityService.GetAll()
                .Where(w => w.DayOfWeek == item.DayOfWeek).ToListAsync();

            if (weeklyEventsSameDay.Count == 0)
            {
                return;
            }

            // get all the fitness events which ids are in weeklyEventsSameDay
            var fitnessEvents = await _fitnessEventEntityService.GetAll()
                .Where(fe => weeklyEventsSameDay.Any(m => m.Id != item.Id && item.FitnessEventId == fe.Id)).ToListAsync();

            var weeklyFitnessEventsSameDay =
                from we in weeklyEventsSameDay
                join fe in fitnessEvents
                on we.FitnessEventId equals fe.Id
                select new WeeklyFitnessEventDto
                {
                    DayOfWeek = we.DayOfWeek,
                    FitnessEventId = fe.Id,
                    Minutes = fe.Minutes,
                    ServiceId = fe.ServiceId,
                    Time = we.Time,
                    VenueId = fe.VenueId,
                    VisitorCapacity = we.VisitorCapacity,
                    WeeklyEventId = we.Id,
                };

            if (weeklyFitnessEventsSameDay.Any(one => (one.Time < item.Time) ?
                    CheckIfAnyTimeCrosses(one.Time, one.Minutes, item.Time) :
                    CheckIfAnyTimeCrosses(item.Time, itemfitnessEvent.Minutes, one.Time)))
            {
                throw new BusinessLogicException(StringRes.TimeCrossesMsg, fieldName: nameof(item.Time));
            }
        }
    }
}
