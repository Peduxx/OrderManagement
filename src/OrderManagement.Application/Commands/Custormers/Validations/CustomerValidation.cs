using OrderManagement.Application.Errors;
using OrderManagement.Application.Errors.Contexts;
using System.Net.Mail;

namespace OrderManagement.Application.Commands.Custormers.Validations
{
    public static class CustomerValidation
    {
        public static Result Validate(ICustomerCommand customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
                return Result.Failure(GenericsErrors.NullOrEmptyProperty(nameof(customer.Name)));

            if (string.IsNullOrWhiteSpace(customer.Email))
                return Result.Failure(GenericsErrors.NullOrEmptyProperty(nameof(customer.Email)));

            if (customer.Name.Length < 2)
                return Result.Failure(CustomersErrors.NameTooShort);

            if (customer.Name.Length > 100)
                return Result.Failure(CustomersErrors.NameTooLong);

            if (!IsValidEmail(customer.Email))
                return Result.Failure(GenericsErrors.InvalidProperty(nameof(customer.Email)));

            return Result.Success();
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}