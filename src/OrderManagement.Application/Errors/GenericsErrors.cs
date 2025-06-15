namespace OrderManagement.Application.Errors
{
    public static class GenericsErrors
    {
        public static Error DomainError(string message) => Error.InternalServerError($"Domain error: {message}");
        public static Error InvalidProperty(string property) => Error.BadRequest($"Invalid property: {property}.");
        public static Error NullOrEmptyProperty(string property) => Error.BadRequest($"Property {property} cannot be null or empty.");
    }
}
