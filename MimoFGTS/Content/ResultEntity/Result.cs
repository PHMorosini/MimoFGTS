namespace MimoFGTS.Content.ResultEntity
{
    public class Result
    {
        public bool Success { get; }
        public string Error { get; private set; }
        public bool IsFailure => !Success;

        protected Result(bool success, string error)
        {
            if (success && !string.IsNullOrEmpty(error))
            {
                throw new InvalidOperationException("A successful result cannot have an error message.");
            }

            if (!success && string.IsNullOrEmpty(error))
            {
                throw new InvalidOperationException("A failed result must have an error message.");
            }

            Success = success;
            Error = error;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static new Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }


        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }
        public static new Result<T> Fail(string message)
        {
            return new Result<T>(default(T), false, message);
        }
    }

}
