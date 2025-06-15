using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;
        public Order Order { get; private set; }
        public Product Product { get; private set; }

        private OrderItem(Guid orderId, Guid productId, int quantity, decimal unitPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public static OrderItem Create(Guid orderId, Product product, int quantity)
        {
            ValidateQuantity(quantity);
            ValidateUnitPrice(product.Price);

            if (quantity > product.Stock)
                throw new DomainException($"Insufficient stock for product {product.Id}. Requested: {quantity}, Available: {product.Stock}");

            return new OrderItem(orderId, product.Id, quantity, product.Price);
        }

        private static void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Order item quantity must be greater than zero.");
        }

        private static void ValidateUnitPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
                throw new DomainException("Order item unit price must be greater than zero.");
        }
    }
}
