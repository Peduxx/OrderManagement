using OrderManagement.Application.Errors;
using OrderManagement.Application.Errors.Contexts;

namespace OrderManagement.Application.Commands.Products.Validations
{
    public static class ProductValidation
    {
        public static Result Validate(IProductCommand product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return Result.Failure(GenericsErrors.NullOrEmptyProperty(nameof(product.Name)));

            if (product.Name.Length < 2)
                return Result.Failure(CustomersErrors.NameTooShort);

            if (product.Name.Length > 100)
                return Result.Failure(CustomersErrors.NameTooLong);

            if (product.Stock < 0)
                return Result.Failure(GenericsErrors.InvalidProperty(nameof(product.Stock)));

            if (product.Price <= 0)
                return Result.Failure(GenericsErrors.InvalidProperty(nameof(product.Price)));

            if (product.Code <= 0)
                return Result.Failure(GenericsErrors.InvalidProperty(nameof(product.Code)));


            return Result.Success();
        }
    }
}
