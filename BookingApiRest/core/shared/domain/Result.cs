namespace BookingApiRest;
public class Result<T> where T : class {
    public T Value { get; }
    public bool IsSuccess { get; }
    public Exception Exception { get; }

    public Result(T value, bool isSuccess, Exception exception = null)  {
        Value = value;
        IsSuccess = isSuccess;
        Exception = exception;
    }

    public static Result<T> Success(T value) {
        return new Result<T>(value, true, null);
    }

    public static Result<T> Fail(Exception exception) {
        return new Result<T>(null, false, exception);
    }

    public Exception GetError() {
        return Exception;
    }
    public T GetValue() {
        return Value;
    }
    public bool HasSucced() {
        return this.IsSuccess;
    }

    public string GetErrorMessage() {
        return Exception != null ? this.Exception.Message : "";
    }

}

