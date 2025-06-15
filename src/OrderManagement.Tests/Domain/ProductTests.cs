using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Exceptions;
using FluentAssertions;

namespace OrderManagement.Tests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void WithValidParameters_ShouldCreateProductSuccessfully()
        {
            // Arrange
            var name = "Notebook Dell";
            var stock = 10;
            var price = 250099;
            var code = 123456789;

            // Act
            var product = Product.Create(name, stock, price, code);

            // Assert
            product.Should().NotBeNull();
            product.Name.Should().Be("Notebook Dell");
            product.Stock.Should().Be(10);
            product.Price.Should().Be(250099);
            product.Code.Should().Be(123456789);
            product.IsDeleted.Should().BeFalse();
        }

        [Fact]
        public void WithNullName_ShouldThrowDomainException()
        {
            // Arrange
            string name = null;
            var stock = 10;
            var price = 250099;
            var code = 123456789;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => Product.Create(name, stock, price, code));
            exception.Message.Should().Be("Product name cannot be null or empty.");
        }

        [Fact]
        public void WithNegativeStock_ShouldThrowDomainException()
        {
            // Arrange
            var name = "Notebook Dell";
            var stock = -5;
            var price = 250099;
            var code = 123456789;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => Product.Create(name, stock, price, code));
            exception.Message.Should().Be("Cannot set a negativa stock for a product.");
        }

        [Fact]
        public void WithZeroPrice_ShouldThrowDomainException()
        {
            // Arrange
            var name = "Notebook Dell";
            var stock = 10;
            var price = 0;
            var code = 123456789;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => Product.Create(name, stock, price, code));
            exception.Message.Should().Be("Cannot set a null price for a product.");
        }

        [Fact]
        public void AfterDelete_ShouldThrowDomainException()
        {
            // Arrange
            var product = Product.Create("Notebook Dell", 10, 250099, 123456789);
            product.Delete();

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() =>
                product.Update("MacBook Pro", 5, 800000, 987654321L));
            exception.Message.Should().Be("Cannot update a deleted customer.");
        }

        [Fact]
        public void ToDeletedProduct_ShouldRestoreProductAndAddStock()
        {
            // Arrange
            var product = Product.Create("Notebook Dell", 10, 250099, 123456789);
            product.Delete();

            // Act
            product.AddStock(5);

            // Assert
            product.Stock.Should().Be(15);
            product.IsDeleted.Should().BeFalse();
        }
    }
}