using AutoMapper;
using FitnessCenterManagement.BusinessLogic.Interfaces;
using FitnessCenterManagement.BusinessLogic.Services;
using FitnessCenterManagement.DataAccess.Entities;
using FitnessCenterManagement.DataAccess.Interfaces;
using Moq;

namespace FitnessCenterManagement.UnitTests
{
    public class UnitSetUpBaseClass
    {
        // SERVICES
        protected IAbonementsService AbonementsService { get; set; }

        protected IFitnessCatalogsService FitnessCatalogsService { get; set; }

        protected ISchedulesService SchedulesService { get; set; }

        protected IUsersService UsersService { get; set; }

        // REPOSITORIES
        protected Mock<IRepository<Abonement>> AbonementRepositoryMock { get; set; }

        protected Mock<IRepository<AbonementCard>> AbonementCardRepositoryMock { get; set; }

        protected Mock<IRepository<AbonementFitnessEvent>> AbonementFitnessEventRepositoryMock { get; set; }

        protected Mock<IRepository<CardEventItem>> CardEventItemRepositoryMock { get; set; }

        protected Mock<IRepository<Customer>> CustomerRepositoryMock { get; set; }

        protected Mock<IRepository<CustomerCategory>> CustomerCategoryRepositoryMock { get; set; }

        protected Mock<IRepository<DateEvent>> DateEventRepositoryMock { get; set; }

        protected Mock<IRepository<FitnessEvent>> FitnessEventRepositoryMock { get; set; }

        protected Mock<IRepository<Review>> ReviewRepositoryMock { get; set; }

        protected Mock<IRepository<Service>> ServiceRepositoryMock { get; set; }

        protected Mock<IRepository<Specialization>> SpecializationRepositoryMock { get; set; }

        protected Mock<IRepository<Trainer>> TrainerRepositoryMock { get; set; }

        protected Mock<IRepository<Venue>> VenueRepositoryMock { get; set; }

        protected Mock<IRepository<WeeklyEvent>> WeeklyEventRepositoryMock { get; set; }

        protected IMapper Mapper { get; set; }

        // ENTITY SERVICES
        protected IEntityService<Abonement> AbonementEntityService { get; set; }

        protected IEntityService<AbonementCard> AbonementCardEntityService { get; set; }

        protected IEntityService<AbonementFitnessEvent> AbonementFitnessEventEntityService { get; set; }

        protected IEntityService<CardEventItem> CardEventItemEntityService { get; set; }

        protected IEntityService<Customer> CustomerEntityService { get; set; }

        protected IEntityService<CustomerCategory> CustomerCategoryEntityService { get; set; }

        protected IEntityService<DateEvent> DateEventEntityService { get; set; }

        protected IEntityService<FitnessEvent> FitnessEventEntityService { get; set; }

        protected IEntityService<Review> ReviewEntityService { get; set; }

        protected IEntityService<Service> ServiceEntityService { get; set; }

        protected IEntityService<Specialization> SpecializationEntityService { get; set; }

        protected IEntityService<Trainer> TrainerEntityService { get; set; }

        protected IEntityService<Venue> VenueEntityService { get; set; }

        protected IEntityService<WeeklyEvent> WeeklyEventEntityService { get; set; }

        protected internal void InitializeAbonementsServiceRepositories()
        {
            AbonementRepositoryMock = new Mock<IRepository<Abonement>>();
            AbonementCardRepositoryMock = new Mock<IRepository<AbonementCard>>();
            CardEventItemRepositoryMock = new Mock<IRepository<CardEventItem>>();
        }

        protected internal void InitializeAbonementsServiceDependencies()
        {
            AbonementEntityService = new GenericEntityService<Abonement>(AbonementRepositoryMock.Object);
            AbonementCardEntityService = new GenericEntityService<AbonementCard>(AbonementCardRepositoryMock.Object);
            CardEventItemEntityService = new GenericEntityService<CardEventItem>(CardEventItemRepositoryMock.Object);
        }

        protected internal void InitializeFitnessCatalogsServiceRepositories()
        {
            FitnessEventRepositoryMock = new Mock<IRepository<FitnessEvent>>();
            SpecializationRepositoryMock = new Mock<IRepository<Specialization>>();
            ServiceRepositoryMock = new Mock<IRepository<Service>>();
            TrainerRepositoryMock = new Mock<IRepository<Trainer>>();
            VenueRepositoryMock = new Mock<IRepository<Venue>>();
        }

        protected internal void InitializeFitnessCatalogsServiceDependencies()
        {
            VenueEntityService = new GenericEntityService<Venue>(VenueRepositoryMock.Object);
            TrainerEntityService = new GenericEntityService<Trainer>(TrainerRepositoryMock.Object);
            ServiceEntityService = new GenericEntityService<Service>(ServiceRepositoryMock.Object);
            SpecializationEntityService = new GenericEntityService<Specialization>(SpecializationRepositoryMock.Object);
            FitnessEventEntityService = new GenericEntityService<FitnessEvent>(FitnessEventRepositoryMock.Object);
        }

        protected internal void InitializeSchedulesServiceRepositories()
        {
            AbonementFitnessEventRepositoryMock = new Mock<IRepository<AbonementFitnessEvent>>();
            WeeklyEventRepositoryMock = new Mock<IRepository<WeeklyEvent>>();
            DateEventRepositoryMock = new Mock<IRepository<DateEvent>>();
            FitnessEventRepositoryMock = new Mock<IRepository<FitnessEvent>>();
        }

        protected internal void InitializeSchedulesServiceDependencies()
        {
            FitnessEventEntityService = new GenericEntityService<FitnessEvent>(FitnessEventRepositoryMock.Object);
            WeeklyEventEntityService = new GenericEntityService<WeeklyEvent>(WeeklyEventRepositoryMock.Object);
            AbonementFitnessEventEntityService = new GenericEntityService<AbonementFitnessEvent>(AbonementFitnessEventRepositoryMock.Object);
            DateEventEntityService = new GenericEntityService<DateEvent>(DateEventRepositoryMock.Object);
        }

        protected internal void InitializeUsersServiceRepositories()
        {
            CustomerRepositoryMock = new Mock<IRepository<Customer>>();
            CustomerCategoryRepositoryMock = new Mock<IRepository<CustomerCategory>>();
            ReviewRepositoryMock = new Mock<IRepository<Review>>();
        }

        protected internal void InitializeUsersServiceDependencies()
        {
            ReviewEntityService = new GenericEntityService<Review>(ReviewRepositoryMock.Object);
            CustomerCategoryEntityService = new GenericEntityService<CustomerCategory>(CustomerCategoryRepositoryMock.Object);
            CustomerEntityService = new GenericEntityService<Customer>(CustomerRepositoryMock.Object);
        }

        protected internal void InitializeAbonementsService()
        {
            InitializeAbonementsServiceDependencies();
            SetupMapper();

            AbonementsService = new AbonementsService(
                AbonementEntityService,
                AbonementCardEntityService,
                CardEventItemEntityService,
                Mapper);
        }

        protected internal void InitializeFitnessCatalogsService()
        {
            InitializeFitnessCatalogsServiceDependencies();
            SetupMapper();

            FitnessCatalogsService = new FitnessCatalogsService(
                FitnessEventEntityService,
                SpecializationEntityService,
                ServiceEntityService,
                TrainerEntityService,
                VenueEntityService, Mapper);
        }

        protected internal void InitializeSchedulesService()
        {
            InitializeSchedulesServiceDependencies();
            SetupMapper();

            SchedulesService = new SchedulesService(
                AbonementFitnessEventEntityService,
                DateEventEntityService,
                WeeklyEventEntityService,
                FitnessEventEntityService,
                Mapper);
        }

        protected internal void InitializeUsersService()
        {
            InitializeUsersServiceRepositories();
            SetupMapper();

            UsersService = new UsersService(
                CustomerCategoryEntityService,
                CustomerEntityService,
                ReviewEntityService,
                Mapper);
        }

        protected internal void SetupMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BusinessLogic.Mapper.BusinessLogicMapperFirstProfile>();
                cfg.AddProfile<BusinessLogic.Mapper.BusinessLogicMapperSecondProfile>();
            });
            Mapper = new Mapper(config);
        }
    }
}
