using FluentAssertions;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Tests.Domain
{
    public class OrderTests
    {
        [Fact]
        public void WithValidItems_ShouldCreateOrderSuccessfully()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var orderItems = CreateValidOrderItems();

            // Act
            var order = Order.Create(customerId, orderItems);

            // Assert
            order.Should().NotBeNull();
            order.CustomerId.Should().Be(customerId);
            order.Status.Should().Be(OrderStatus.WaitingPayment);
            order.OrderItems.Should().HaveCount(2);
            order.IsDeleted.Should().BeFalse();
        }

        [Fact]
        public void WithEmptyItems_ShouldThrowDomainException()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var emptyItems = new List<OrderItem>();

            // Act & Assert
            var exception = Assert.Throws<DomainException>(
                () => Order.Create(customerId, emptyItems));

            exception.Message.Should().Be("Order must contain at least one item.");
        }

        [Fact]
        public void WithNullItems_ShouldThrowDomainException()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            IEnumerable<OrderItem> nullItems = null;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(
                () => Order.Create(customerId, nullItems));

            exception.Message.Should().Be("Order must contain at least one item.");
        }

        [Fact]
        public void WithValidTransition_ShouldUpdateStatusSuccessfully()
        {
            // Arrange
            var order = CreateValidOrder();
            var newStatus = OrderStatus.Payd;

            // Act
            order.UpdateStatus(newStatus);

            // Assert
            order.Status.Should().Be(newStatus);
        }

        [Fact]
        public void WithInvalidTransition_ShouldThrowDomainException()
        {
            // Arrange
            var order = CreateValidOrder();
            var invalidStatus = OrderStatus.Delivered;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(
                () => order.UpdateStatus(invalidStatus));

            exception.Message.Should().Be($"Cannot change the order status from {order.Status} to {invalidStatus}.");
        }

        [Fact]
        public void AfterDelete_ShouldThrowDomainException()
        {
            // Arrange
            var order = CreateValidOrder();
            order.Delete();

            // Act & Assert
            var exception = Assert.Throws<DomainException>(
                () => order.UpdateStatus(OrderStatus.Payd));

            exception.Message.Should().Be("Cannot update status of a deleted order.");
        }

        [Fact]
        public void WhenCalculated_ShouldReturnCorrectTotalAmount()
        {
            // Arrange
            var orderItems = new List<OrderItem>
            {
                CreateOrderItem(quantity: 2, unitPrice: 1000),
                CreateOrderItem(quantity: 3, unitPrice: 1500)
            };
            var order = Order.Create(Guid.NewGuid(), orderItems);

            // Act
            var totalAmount = order.TotalAmount;

            // Assert
            totalAmount.Should().Be(6500);
        }

        [Fact]
        public void WithValidStatusFlow_ShouldAllowCorrectTransitions()
        {
            // Arrange
            var order = CreateValidOrder();

            // Act & Assert
            order.UpdateStatus(OrderStatus.Payd);
            order.Status.Should().Be(OrderStatus.Payd);

            order.UpdateStatus(OrderStatus.Shipped);
            order.Status.Should().Be(OrderStatus.Shipped);

            order.UpdateStatus(OrderStatus.Delivered);
            order.Status.Should().Be(OrderStatus.Delivered);
        }

        private Order CreateValidOrder()
        {
            var customerId = Guid.NewGuid();
            var orderItems = CreateValidOrderItems();
            return Order.Create(customerId, orderItems);
        }

        private List<OrderItem> CreateValidOrderItems()
        {
            return new List<OrderItem>
            {
                CreateOrderItem(),
                CreateOrderItem()
            };
        }

        private OrderItem CreateOrderItem(int quantity = 1, decimal unitPrice = 1000)
        {
            var orderId = Guid.NewGuid();
            var product = CreateValidProduct(unitPrice);
            return OrderItem.Create(orderId, product, quantity);
        }

        private Product CreateValidProduct(decimal price = 100, int stock = 10)
        {
            return Product.Create("Test Product", stock, price, 12345);
        }
    }
}
