namespace OrderManagement.Application.Errors
{
    public record Error
    {
        public static readonly Error None = new(string.Empty, ErrorType.BadRequest);

        public string Description { get; }
        public ErrorType Type { get; }

        private Error(string description, ErrorType errorType)
        {
            Description = description;
            Type = errorType;
        }

        public static Error BadRequest(string description) => new(description, ErrorType.BadRequest);
        public static Error NotFound(string description) => new(description, ErrorType.NotFound);
        public static Error Conflict(string description) => new(description, ErrorType.Conflict);
        public static Error InternalServerError(string description) => new(description, ErrorType.InternalServerError);
    }
}
