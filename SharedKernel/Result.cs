namespace SharedKernel
{
    public class Result
    {
        protected internal Result(bool isSuccess, string statusMessage, Error? error)
        {
            if(isSuccess && error != null ||
                !isSuccess && error == null)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSuccess = isSuccess;
            StatusMessage = statusMessage;
            Error = error;
        }

        public bool IsSuccess { get; }
        public string StatusMessage { get; }
        public Error? Error { get; }

        public static Result Success(string message) => new(true, message, null);
        public static Result Failure(Error? error , string message) => new(false, message, error);
        public static Result<TValue> Success<TValue>(TValue data, string? message = "Get data successfully") => new(data, true, message!, null);
        public static Result<TValue> Failure<TValue>(Error error, string? message = "Data retrieval failure") => new(default, false, message!, error);
    }
}
