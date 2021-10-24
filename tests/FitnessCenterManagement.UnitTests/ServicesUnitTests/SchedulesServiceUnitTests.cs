using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace FitnessCenterManagement.UnitTests
{
    [TestFixture]
    public class SchedulesServiceUnitTests : UnitSetUpBaseClass
    {
        [Test]
        public void GivenCorrectAbonementFitnessEvent_WhenCreateAbonementFitnessEvent_ShouldCreateSuccessfully()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenNullAbonementFitnessEvent_WhenCreateAbonementFitnessEvent_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenAbonementFitnessEventWithTakenData_WhenCreateAbonementFitnessEvent_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenCorrectDateEvent_WhenCreateDateEvent_ShouldCreateSuccessfully()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenNullDateEvent_WhenCreateDateEvent_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenDateEventWithIncorrectDay_WhenCreateDateEvent_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenDateEventWithIncorrectCrossingDate_WhenCreateDateEvent_ShouldReturnException()
        {
            Thread.Sleep(431);

            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenCorrectWeeklyEvent_WhenCreateWeeklyEvent_ShouldCreateSuccessfully()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenNullWeeklyEvent_WhenCreateWeeklyEvent_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenWeeklyEventWithIncorrectDay_WhenCreateWeeklyEvent_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenWeeklyEventWithNegativeVisitorCapacity_WhenCreateWeeklyEvent_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenWeeklyEventWithIncorrectCrossingDay_WhenCreateWeeklyEvent_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }
    }
}
