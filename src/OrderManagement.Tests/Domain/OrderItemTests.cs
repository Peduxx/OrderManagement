using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Exceptions;
using FluentAssertions;

namespace OrderManagement.Tests.Domain
{
    public class OrderItemTests
    {
        [Fact]
        public void WithValidData_ShouldCreateOrderItemSuccessfully()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var product = CreateValidProduct();
            var quantity = 2;

            // Act
            var orderItem = OrderItem.Create(orderId, product, quantity);

            // Assert
            orderItem.Should().NotBeNull();
            orderItem.OrderId.Should().Be(orderId);
            orderItem.ProductId.Should().Be(product.Id);
            orderItem.Quantity.Should().Be(quantity);
            orderItem.UnitPrice.Should().Be(product.Price);
            orderItem.TotalPrice.Should().Be(quantity * product.Price);
        }

        [Fact]
        public void WithZeroQuantity_ShouldThrowDomainException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var product = CreateValidProduct();
            var quantity = 0;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(
                () => OrderItem.Create(orderId, product, quantity));

            exception.Message.Should().Be("Order item quantity must be greater than zero.");
        }

        [Fact]
        public void WithNegativeQuantity_ShouldThrowDomainException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var product = CreateValidProduct();
            var quantity = -1;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(
                () => OrderItem.Create(orderId, product, quantity));

            exception.Message.Should().Be("Order item quantity must be greater than zero.");
        }

        [Fact]
        public void WithInsufficientStock_ShouldThrowDomainException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var product = CreateValidProduct(stock: 5);
            var quantity = 10;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(
                () => OrderItem.Create(orderId, product, quantity));

            exception.Message.Should().Be($"Insufficient stock for product {product.Id}. Requested: {quantity}, Available: {product.Stock}");
        }

        [Fact]
        public void WhenCalculated_ShouldReturnCorrectTotalPrice()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var product = CreateValidProduct(price: 1050);
            var quantity = 3;
            var orderItem = OrderItem.Create(orderId, product, quantity);

            // Act
            var totalPrice = orderItem.TotalPrice;

            // Assert
            totalPrice.Should().Be(3150);
        }

        private Product CreateValidProduct(int stock = 10, decimal price = 100)
        {
            return Product.Create("Test Product", stock, price, 12345);
        }
    }
}