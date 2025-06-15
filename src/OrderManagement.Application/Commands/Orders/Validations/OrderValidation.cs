using OrderManagement.Application.Errors.Contexts;

namespace OrderManagement.Application.Commands.Orders.Validations
{
    public static class OrderValidation
    {
        public static Result Validate(IOrderCommand order)
        {
            foreach(var item in order.Products)
            {
                if(item.Quantity <= 0)
                {
                    Result.Failure(OrderErrors.InvalidQuantity(item.ProductId));
                }
            }

            return Result.Success();
        }
    }
}
