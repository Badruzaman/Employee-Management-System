
using System.Diagnostics.Contracts;

namespace EmployeeManagementSystem.Common
{

    [Serializable]
    public class Result
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }
        public bool Failure => !Success;
        public short Code { get; private set; }

        protected Result(bool success, string error, short errorCode = -1)
        {
            Contract.Requires(success || (!string.IsNullOrEmpty(error) && errorCode == -1));
            Contract.Requires(!success || string.IsNullOrEmpty(error) || errorCode > 0);

            Success = success;
            Error = error;
            Code = errorCode;
        }

        public static Result Fail(string message) => new Result(false, message, 422);

        public static Result Fail(string message, short errorCode) => new Result(false, message, errorCode);

        public static Result<T> Fail<T>(string message) => new Result<T>(default(T), false, message, 422);

        public static Result<T> Fail<T>(string message, short errorCode) =>
            new Result<T>(default(T), false, message, errorCode);

        public static Result Ok() => new Result(true, string.Empty);

        public static Result<T> Ok<T>(T value) => new Result<T>(value, true, string.Empty);

        public static Result NotFound(string message = "") => new Result(false, message, 404);

        public static Result<T> NotFound<T>(string message = null) => new Result<T>(default(T), false, message, 404);

        public static Result Forbidden(string message = "") => new Result(false, message, 403);

        public static Result<T> Forbidden<T>(string message = "") => new Result<T>(default(T), false, message, 403);

        public static Result BadRequest(string message = "") => new Result(false, message, 400);

        public static Result<T> BadRequest<T>(string message = "") => new Result<T>(default(T), false, message, 400);

        public static Result Combine(params Result[] results)
        {
            foreach (Result r in results)
            {
                if (!r.Success)
                    return r;
            }
            return Ok();
        }
    }

    public class Result<T> : Result
    {
        private T _value;

        public T Value
        {
            get
            {
                Contract.Requires(Success);
                return _value;
            }

            private set => _value = value;
        }

        protected internal Result(T value, bool success, string error, short errorCode = -1) : base(success, error, errorCode)
        {
            Contract.Requires(value != null || !success);
            Value = value;
        }
    }
}
