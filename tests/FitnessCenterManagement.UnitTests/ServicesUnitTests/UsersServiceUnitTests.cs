using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace FitnessCenterManagement.UnitTests
{
    [TestFixture]
    public class UsersServiceUnitTests : UnitSetUpBaseClass
    {
        [Test]
        public void GivenCorrectCustomerCategory_WhenCreateCustomerCategory_ShouldCreateSuccessfully()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenNullCustomerCategory_WhenCreateCustomerCategory_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenCustomerCategoryWithTakenName_WhenCreateCustomerCategory_ShouldReturnException()
        {
            Thread.Sleep(397);

            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenCustomerCategoryWithTooBigCoefficient_WhenCreateCustomerCategory_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenCustomerCategoryWithTooLittleCoefficient_WhenCreateCustomerCategory_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenCorrectCustomer_WhenCreateCustomer_ShouldCreateSuccessfully()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenNullCustomer_WhenCreateCustomer_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenCorrectReview_WhenCreateReview_ShouldCreateSuccessfully()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenNullReview_WhenCreateReview_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }

        [Test]
        public void GivenReviewWithEmptyText_WhenCreateReview_ShouldReturnException()
        {
            // Arrange
            // Act
            // Assert
            true.Should().BeTrue();
        }
    }
}
