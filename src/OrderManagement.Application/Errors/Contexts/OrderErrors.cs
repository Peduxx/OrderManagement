namespace OrderManagement.Application.Errors.Contexts
{
    public static class OrderErrors
    {
        public static Error InvalidQuantity(Guid id) => Error.BadRequest($"Invalid quantity for product: {id}");
        public static Error InsufficientStock(Guid id) => Error.BadRequest($"Insufficient stock for product: {id}");

        public readonly static Error OrderNotFound = Error.BadRequest($"Order not found.");
    }
}
