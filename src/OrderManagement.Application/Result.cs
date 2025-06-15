using System.Text.Json.Serialization;
using OrderManagement.Application.Errors;

namespace OrderManagement.Application
{
    public class Result
    {
        private readonly List<object> _data = [];

        public bool IsSuccess { get; }
        public IReadOnlyList<object> Data => _data.AsReadOnly();

        [JsonIgnore]
        public Error Error { get; }

        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Error? ErrorForJson => Error == Error.None ? null : Error;

        private Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);

        public Result AddData(object item)
        {
            _data.Add(item);
            return this;
        }
    }
}