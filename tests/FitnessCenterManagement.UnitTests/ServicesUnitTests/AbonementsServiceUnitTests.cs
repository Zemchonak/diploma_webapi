using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessCenterManagement.BusinessLogic.Dtos;
using FitnessCenterManagement.BusinessLogic.Exceptions;
using FitnessCenterManagement.BusinessLogic.Resources;
using FitnessCenterManagement.DataAccess.Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace FitnessCenterManagement.UnitTests
{
    [TestFixture]
    public class AbonementsServiceUnitTests : UnitSetUpBaseClass
    {
        private const int CreatedItemId = 141;
        private const int CardCustomerId = 1;
        private const int AttendancesNumber = 1;
        private const decimal CoefficientNumber = 0.9m;
        private const string DefaultImageNameString = "testImage";
        private const string AbonementNameString = "testAbonement";

        private const string EmptyAbonementName = "";
        private const int NegativeAttendances = -1;
        private const int NegativeVisits = -1;
        private const decimal TooBigCoefficient = 3.01m;
        private const decimal TooLittleCoefficient = -0.01m;

        private static List<Abonement> PrepareAbonementsCollection(Abonement item = null)
        {
            var collection = new List<Abonement>
            {
                new Abonement { Id = 1, Attendances = 2, Coefficient = 1.1m, ImageName = "image1", Name = "name1", Status = DataAccess.Enums.AbonementStatus.Disabled, },
                new Abonement { Id = 2, Attendances = 3, Coefficient = 1.2m, ImageName = "image2", Name = "name2", Status = DataAccess.Enums.AbonementStatus.Disabled, },
                new Abonement { Id = 3, Attendances = 4, Coefficient = 0.8m, ImageName = "image3", Name = "name3", Status = DataAccess.Enums.AbonementStatus.Enabled, },
            };

            if (item != null)
            {
                collection.Add(item);
            }

            return collection;
        }

        private static List<AbonementCard> PrepareAbonementCardsCollection(AbonementCard item = null)
        {
            var dateTimeOffsetNow = DateTimeOffset.Now;
            var collection = new List<AbonementCard>
            {
                new AbonementCard { Id = 1, AbonementId = 1, CustomerId = 1, PurchaseDate = dateTimeOffsetNow.AddDays(-1), Status = DataAccess.Enums.AbonementCardStatus.Disabled, Visits = 1 },
                new AbonementCard { Id = 2, AbonementId = 2, CustomerId = 2, PurchaseDate = dateTimeOffsetNow.AddDays(-2), Status = DataAccess.Enums.AbonementCardStatus.Disabled, Visits = 2 },
                new AbonementCard { Id = 3, AbonementId = 3, CustomerId = 3, PurchaseDate = dateTimeOffsetNow.AddDays(-3), Status = DataAccess.Enums.AbonementCardStatus.Enabled,  Visits = 3 },
            };

            if (item != null)
            {
                collection.Add(item);
            }

            return collection;
        }

        private static List<CardEventItem> PrepareCardEventItemsCollection(CardEventItem item = null)
        {
            var collection = new List<CardEventItem>
            {
                new CardEventItem { Id = 1, AbonementCardId = 1, DateEventId = 1, IsUsed = false, },
                new CardEventItem { Id = 2, AbonementCardId = 1, DateEventId = 1, IsUsed = false, },
                new CardEventItem { Id = 3, AbonementCardId = 1, DateEventId = 1, IsUsed = false, },
            };

            if (item != null)
            {
                collection.Add(item);
            }

            return collection;
        }

        [Test]
        public async Task GivenCorrectAbonement_WhenCreateAbonement_ShouldCreateSuccessfully()
        {
            // Arrange
            var collection = PrepareAbonementsCollection();

            var abonement = new AbonementDto
            {
                Attendances = AttendancesNumber,
                Coefficient = CoefficientNumber,
                ImageName = DefaultImageNameString,
                Name = AbonementNameString,
                Status = BusinessLogic.Enums.AbonementStatus.Disabled,
            };

            InitializeAbonementsServiceRepositories();
            GetAllAbonementsMockSetup(collection.AsQueryable());
            CreateAbonementMockSetup(collection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            // Act
            var result = await AbonementsService.CreateAbonementAsync(abonement);

            // Assert
            result.Should().Be(CreatedItemId);
            collection.Should().BeEquivalentTo(PrepareAbonementsCollection(new Abonement
                    {
                        Id = CreatedItemId,
                        Attendances = AttendancesNumber,
                        Coefficient = CoefficientNumber,
                        ImageName = DefaultImageNameString,
                        Name = AbonementNameString,
                        Status = DataAccess.Enums.AbonementStatus.Disabled,
                    }));
        }

        [Test]
        public async Task GivenNullAbonement_WhenCreateAbonement_ShouldReturnException()
        {
            // Arrange
            var collection = PrepareAbonementsCollection();

            InitializeAbonementsServiceRepositories();
            GetAllAbonementsMockSetup(collection.AsQueryable());
            CreateAbonementMockSetup(collection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            AbonementDto item = null;

            // Act
            Func<Task> action = async () => await AbonementsService.CreateAbonementAsync(item);

            // Assert
            await action.Should().ThrowAsync<BusinessLogicException>()
                .WithMessage(StringRes.NullEntityMsg);
        }

        [Test]
        public async Task GivenAbonementWithTakenName_WhenCreateAbonement_ShouldReturnException()
        {
            // Arrange
            var collection = PrepareAbonementsCollection(
                new Abonement
                {
                    Id = 5,
                    Attendances = AttendancesNumber,
                    Coefficient = CoefficientNumber,
                    ImageName = DefaultImageNameString,
                    Name = AbonementNameString,
                    Status = DataAccess.Enums.AbonementStatus.Disabled,
                });

            InitializeAbonementsServiceRepositories();
            GetAllAbonementsMockSetup(collection.AsQueryable());
            CreateAbonementMockSetup(collection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            var item = new AbonementDto
            {
                Attendances = AttendancesNumber,
                Coefficient = CoefficientNumber,
                ImageName = DefaultImageNameString,
                Name = AbonementNameString,
                Status = BusinessLogic.Enums.AbonementStatus.Disabled,
            };

            // Act
            Func<Task> action = async () => await AbonementsService.CreateAbonementAsync(item);

            // Assert
            await action.Should().ThrowAsync<ValidationException>()
                .WithMessage(StringRes.NameShouldBeUniqueMsg);
        }

        [Test]
        public async Task GivenAbonementWithEmptyName_WhenCreateAbonement_ShouldReturnException()
        {
            // Arrange
            var collection = PrepareAbonementsCollection();

            InitializeAbonementsServiceRepositories();
            GetAllAbonementsMockSetup(collection.AsQueryable());
            CreateAbonementMockSetup(collection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            var item = new AbonementDto
            {
                Attendances = AttendancesNumber,
                Coefficient = CoefficientNumber,
                ImageName = DefaultImageNameString,
                Name = EmptyAbonementName,
                Status = BusinessLogic.Enums.AbonementStatus.Disabled,
            };

            // Act
            Func<Task> action = async () => await AbonementsService.CreateAbonementAsync(item);

            // Assert
            await action.Should().ThrowAsync<ValidationException>()
                .WithMessage(StringRes.EmptyNameMsg);
        }

        [Test]
        public async Task GivenAbonementWithNegativeAttendances_WhenCreateAbonement_ShouldReturnException()
        {
            // Arrange
            var collection = PrepareAbonementsCollection();

            InitializeAbonementsServiceRepositories();
            GetAllAbonementsMockSetup(collection.AsQueryable());
            CreateAbonementMockSetup(collection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            var item = new AbonementDto
            {
                Attendances = NegativeAttendances,
                Coefficient = CoefficientNumber,
                ImageName = DefaultImageNameString,
                Name = AbonementNameString,
                Status = BusinessLogic.Enums.AbonementStatus.Disabled,
            };

            // Act
            Func<Task> action = async () => await AbonementsService.CreateAbonementAsync(item);

            // Assert
            await action.Should().ThrowAsync<ValidationException>()
                .WithMessage(StringRes.NegativeAttendancesMsg);
        }

        [Test]
        public async Task GivenAbonementWithTooBigCoefficient_WhenCreateAbonement_ShouldReturnException()
        {
            // Arrange
            var collection = PrepareAbonementsCollection();

            InitializeAbonementsServiceRepositories();
            GetAllAbonementsMockSetup(collection.AsQueryable());
            CreateAbonementMockSetup(collection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            var item = new AbonementDto
            {
                Attendances = AttendancesNumber,
                Coefficient = TooBigCoefficient,
                ImageName = DefaultImageNameString,
                Name = AbonementNameString,
                Status = BusinessLogic.Enums.AbonementStatus.Disabled,
            };

            // Act
            Func<Task> action = async () => await AbonementsService.CreateAbonementAsync(item);

            // Assert
            await action.Should().ThrowAsync<ValidationException>()
                .WithMessage(StringRes.CoefficientBoundsMsg);
        }

        [Test]
        public async Task GivenAbonementWithTooLittleCoefficient_WhenCreateAbonement_ShouldReturnException()
        {
            // Arrange
            var collection = PrepareAbonementsCollection();

            InitializeAbonementsServiceRepositories();
            GetAllAbonementsMockSetup(collection.AsQueryable());
            CreateAbonementMockSetup(collection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            var item = new AbonementDto
            {
                Attendances = AttendancesNumber,
                Coefficient = TooLittleCoefficient,
                ImageName = DefaultImageNameString,
                Name = AbonementNameString,
                Status = BusinessLogic.Enums.AbonementStatus.Disabled,
            };

            // Act
            Func<Task> action = async () => await AbonementsService.CreateAbonementAsync(item);

            // Assert
            await action.Should().ThrowAsync<ValidationException>()
                .WithMessage(StringRes.CoefficientBoundsMsg);
        }

        [Test]
        public async Task GivenNullAbonementCard_WhenCreateAbonementCard_ShouldReturnException()
        {
            // Arrange
            var collection = PrepareAbonementCardsCollection();

            InitializeAbonementsServiceRepositories();
            GetAllAbonementCardsMockSetup(collection.AsQueryable());
            CreateAbonementCardMockSetup(collection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            AbonementCardDto item = null;

            // Act
            Func<Task> action = async () => await AbonementsService.CreateAbonementCardAsync(item);

            // Assert
            await action.Should().ThrowAsync<BusinessLogicException>()
                .WithMessage(StringRes.NullEntityMsg);
        }

        [Test]
        public async Task GivenAbonementCardWithNegativeVisits_WhenCreateAbonementCard_ShouldReturnException()
        {
            // Arrange
            var collection = PrepareAbonementCardsCollection();

            InitializeAbonementsServiceRepositories();
            GetAllAbonementCardsMockSetup(collection.AsQueryable());
            CreateAbonementCardMockSetup(collection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            var item = new AbonementCardDto
            {
                AbonementId = CreatedItemId,
                PurchaseDate = DateTimeOffset.Now.AddDays(-1),
                CustomerId = CardCustomerId,
                Status = BusinessLogic.Enums.AbonementCardStatus.Disabled,
                Visits = NegativeVisits,
            };

            // Act
            Func<Task> action = async () => await AbonementsService.CreateAbonementCardAsync(item);

            // Assert
            await action.Should().ThrowAsync<ValidationException>()
                .WithMessage(StringRes.NegativeVisitsCountMsg);
        }

        [Test]
        public async Task GivenAbonementCardWithFutureDate_WhenCreateAbonementCard_ShouldReturnException()
        {
            // Arrange
            var collection = PrepareAbonementCardsCollection();

            InitializeAbonementsServiceRepositories();
            GetAllAbonementCardsMockSetup(collection.AsQueryable());
            CreateAbonementCardMockSetup(collection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            var item = new AbonementCardDto
            {
                AbonementId = CreatedItemId,
                PurchaseDate = DateTimeOffset.Now.AddDays(10),
                CustomerId = CardCustomerId,
                Status = BusinessLogic.Enums.AbonementCardStatus.Disabled,
                Visits = 1,
            };

            // Act
            Func<Task> action = async () => await AbonementsService.CreateAbonementCardAsync(item);

            // Assert
            await action.Should().ThrowAsync<ValidationException>()
                .WithMessage(StringRes.DateInTheFutureMsg);
        }

        [Test]
        public async Task GivenAbonementCardWithVisitsGreaterThanAbonementAttendances_WhenCreateAbonementCard_ShouldReturnException()
        {
            // Arrange
            var cardsCollection = PrepareAbonementCardsCollection();
            var abonementCollection = PrepareAbonementsCollection();

            InitializeAbonementsServiceRepositories();
            GetAbonementByIdMockSetup(abonementCollection);
            GetAllAbonementCardsMockSetup(cardsCollection.AsQueryable());
            CreateAbonementCardMockSetup(cardsCollection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            var item = new AbonementCardDto
            {
                AbonementId = CreatedItemId,
                PurchaseDate = DateTimeOffset.Now.AddDays(10),
                CustomerId = CardCustomerId,
                Status = BusinessLogic.Enums.AbonementCardStatus.Disabled,
                Visits = 1,
            };

            // Act
            Func<Task> action = async () => await AbonementsService.CreateAbonementCardAsync(item);

            // Assert
            await action.Should().ThrowAsync<ValidationException>()
                .WithMessage(StringRes.DateInTheFutureMsg);
        }

        [Test]
        public async Task GivenNullCardEventItem_WhenCreateCardEventItem_ShouldReturnException()
        {
            // Arrange
            var collection = PrepareCardEventItemsCollection();

            InitializeAbonementsServiceRepositories();
            CreateCardEventItemMockSetup(collection, CreatedItemId);
            InitializeAbonementsServiceDependencies();
            InitializeAbonementsService();

            CardEventItemDto item = null;

            // Act
            Func<Task> action = async () => await AbonementsService.CreateCardEventItemAsync(item);

            // Assert
            await action.Should().ThrowAsync<BusinessLogicException>()
                .WithMessage(StringRes.NullEntityMsg);
        }

        private void GetAllAbonementsMockSetup(IQueryable<Abonement> collection) =>
            AbonementRepositoryMock.Setup(r => r.GetAll()).Returns(collection);

        private void CreateAbonementMockSetup(IList<Abonement> collection, int newAbonementIdValue)
            => AbonementRepositoryMock.Setup(r =>
                r.CreateAsync(It.IsAny<Abonement>()))
                    .Callback<Abonement>(area =>
                    {
                        area.Id = newAbonementIdValue;
                        collection.Add(area);
                    })
                    .ReturnsAsync(newAbonementIdValue);

        private void GetAllAbonementCardsMockSetup(IQueryable<AbonementCard> collection) =>
            AbonementCardRepositoryMock.Setup(r => r.GetAll()).Returns(collection);

        private void GetAbonementByIdMockSetup(IList<Abonement> collection) =>
            AbonementRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns<int>(id => Task.FromResult(collection.FirstOrDefault(e => e.Id == id)));

        private void CreateAbonementCardMockSetup(IList<AbonementCard> collection, int newAbonementCardIdValue)
            => AbonementCardRepositoryMock.Setup(r =>
                r.CreateAsync(It.IsAny<AbonementCard>()))
                    .Callback<AbonementCard>(area =>
                    {
                        area.Id = newAbonementCardIdValue;
                        collection.Add(area);
                    })
                    .ReturnsAsync(newAbonementCardIdValue);

        private void CreateCardEventItemMockSetup(IList<CardEventItem> collection, int newCardEventItemIdValue)
            => CardEventItemRepositoryMock.Setup(r =>
                r.CreateAsync(It.IsAny<CardEventItem>()))
                    .Callback<CardEventItem>(a =>
                    {
                        a.Id = newCardEventItemIdValue;
                        collection.Add(a);
                    })
                    .ReturnsAsync(newCardEventItemIdValue);
    }
}
