namespace OrderManagement.Application.Errors.Contexts
{
    public static class CustomersErrors
    {
        public static readonly Error NameTooShort = Error.BadRequest("Too short name.");
        public static readonly Error NameTooLong = Error.BadRequest("Too long name.");
        public static readonly Error EmailAlreadyExists = Error.Conflict("Email already exists.");
        public static readonly Error CustomerNotFound = Error.NotFound("Customer not found.");
    }
}
