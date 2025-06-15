namespace OrderManagement.Application.Errors.Contexts
{
    public static class ProductErrors
    {
        public static Error ProductNotFound(Guid id) => Error.BadRequest($"Product not found: {id}");

        public static readonly Error NameTooShort = Error.BadRequest("Too short name.");
        public static readonly Error NameTooLong = Error.BadRequest("Too long name.");
    }
}
