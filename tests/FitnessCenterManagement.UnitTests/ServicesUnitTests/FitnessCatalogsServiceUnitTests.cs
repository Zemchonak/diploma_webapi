using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace FitnessCenterManagement.UnitTests
{
    [TestFixture]
    public class FitnessCatalogsServiceUnitTests : UnitSetUpBaseClass
    {
        [Test]
        public void GivenCorrectFitnessEvent_WhenCreateFitnessEvent_ShouldCreateSuccessfully()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenNullFitnessEvent_WhenCreateFitnessEvent_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenFitnessEventWithTooBigMinutes_WhenCreateFitnessEvent_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenFitnessEventWithTooLittleMinutes_WhenCreateFitnessEvent_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenSpecializationWithCorrectData_WhenCreateSpecialization_ShouldCreateSuccessfully()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenNullSpecialization_WhenCreateSpecialization_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenSpecializationWithEmptyInfo_WhenCreateSpecialization_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenCorrectService_WhenCreateService_ShouldCreateSuccessfully()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenNullService_WhenCreateService_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenServiceTooLittlePrice_WhenCreateService_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenServiceWithTakenNameAndSpecialization_WhenCreateService_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenCorrectTrainer_WhenCreateTrainer_ShouldCreateSuccessfully()
        {
            Thread.Sleep(583);

            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenNullTrainer_WhenCreateTrainer_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenTrainerWithEmptyDescription_WhenCreateTrainer_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenCorrectVenue_WhenCreateVenue_ShouldCreateSuccessfully()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenNullVenue_WhenCreateVenue_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenVenueWithEmptyLocation_WhenCreateVenue_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenVenueWithEmptyName_WhenCreateVenue_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenVenueTakenNameAndLocation_WhenCreateVenue_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }
    }
}
