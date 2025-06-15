using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Exceptions;
using FluentAssertions;

namespace OrderManagement.Tests.Domain
{
    public class CustomerTests
    {
        [Fact]
        public void WithValidParameters_ShouldCreateCustomerSuccessfully()
        {
            // Arrange
            var name = "Pedro";
            var email = "pedro.alves@email.com";

            // Act
            var customer = Customer.Create(name, email);

            // Assert
            customer.Should().NotBeNull();
            customer.Name.Should().Be("Pedro");
            customer.Email.Should().Be("pedro.alves@email.com");
            customer.IsDeleted.Should().BeFalse();
        }

        [Fact]
        public void WithNullName_ShouldThrowDomainException()
        {
            // Arrange
            string name = null;
            var email = "pedro.alves@email.com";

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => Customer.Create(name, email));
            exception.Message.Should().Be("Customer name cannot be null or empty.");
        }

        [Fact]
        public void WithInvalidEmail_ShouldThrowDomainException()
        {
            // Arrange
            var name = "Pedro";
            var email = "invalid-email";

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => Customer.Create(name, email));
            exception.Message.Should().Be("Customer email format is invalid.");
        }

        [Fact]
        public void AfterDelete_ShouldThrowDomainException()
        {
            // Arrange
            var customer = Customer.Create("Pedro", "pedro.alves@email.com");
            customer.Delete();

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                customer.Update("Cristiano Ronaldo", "cristianoronaldo@email.com"));

            exception.Message.Should().Be("Cannot update a deleted customer.");
        }

        [Fact]
        public void WithValidParameters_ShouldUpdateCustomerSuccessfully()
        {
            // Arrange
            var customer = Customer.Create("Pedro", "pedro.alves@email.com");

            // Act
            customer.Update("Cristiano Ronaldo", "cristianoronaldo@email.com");

            // Assert
            customer.Name.Should().Be("Cristiano Ronaldo");
            customer.Email.Should().Be("cristianoronaldo@email.com");
            customer.IsDeleted.Should().BeFalse();
        }

        [Fact]
        public void WhenCalled_ShouldMarkCustomerAsDeleted()
        {
            // Arrange
            var customer = Customer.Create("Pedro", "pedro.alves@email.com");

            // Act
            customer.Delete();

            // Assert
            customer.IsDeleted.Should().BeTrue();
        }
    }
}