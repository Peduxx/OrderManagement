using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount => _orderItems.Sum(item => item.TotalPrice);

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private static readonly Dictionary<OrderStatus, OrderStatus[]> ValidTransitions = new()
        {
            [OrderStatus.WaitingPayment] = [OrderStatus.Payd, OrderStatus.Canceled],
            [OrderStatus.Payd] = [OrderStatus.Shipped, OrderStatus.Canceled],
            [OrderStatus.Shipped] = [OrderStatus.Delivered, OrderStatus.Canceled],
            [OrderStatus.Delivered] = [],
            [OrderStatus.Canceled] = []
        };

        protected Order()
        {
            _orderItems = [];
        }

        private Order(Guid customerId, IEnumerable<OrderItem> items)
        {
            if (items == null || !items.Any())
                throw new DomainException("Order must contain at least one item.");

            CustomerId = customerId;
            Status = OrderStatus.WaitingPayment;
            _orderItems = new List<OrderItem>(items);
        }

        public static Order Create(Guid customerId, IEnumerable<OrderItem> items)
        {
            return new Order(customerId, items);
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            if (IsDeleted)
                throw new DomainException("Cannot update status of a deleted order.");

            if (!ValidTransitions.TryGetValue(Status, out var allowedStatuses) ||
                !allowedStatuses.Contains(newStatus))
            {
                throw new DomainException($"Cannot change the order status from {Status} to {newStatus}.");
            }

            Status = newStatus;
            SetUpdatedAt();
        }

        public void DeleteOrderAndItems()
        {
            if (IsDeleted ) return;

            if(Status != OrderStatus.Canceled)
                throw new DomainException("Cannot delete an active order.");

            foreach (var item in _orderItems)
            {
                item.Delete();
            }

            Delete();
        }
    }
}
