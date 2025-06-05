namespace Application.IdentityProvider.WebApi;

public class Result<TSuccess, TFailure>
{
    private bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    private TSuccess Success { get; }
    private TFailure Failure { get; }

    private Result(TSuccess success)
    {
        IsSuccess = true;
        Success = success;
        Failure = default!;
    }

    private Result(TFailure failure)
    {
        IsSuccess = false;
        Failure = failure;
        Success = default!;
    }

    // Factory methods
    public static Result<TSuccess, TFailure> Ok(TSuccess success) => new(success);
    public static Result<TSuccess, TFailure> Fail(TFailure failure) => new(failure);

    // Implicit conversions
    public static implicit operator Result<TSuccess, TFailure>(TSuccess success) => Ok(success);
    public static implicit operator Result<TSuccess, TFailure>(TFailure failure) => Fail(failure);

    // Match
    public TReturn Match<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
    {
        return IsSuccess ? onSuccess(Success) : onFailure(Failure);
    }

    // Map: transform the success type
    public Result<TNewSuccess, TFailure> Map<TNewSuccess>(Func<TSuccess, TNewSuccess> mapper)
    {
        return IsSuccess ? Result<TNewSuccess, TFailure>.Ok(mapper(Success))
                         : Result<TNewSuccess, TFailure>.Fail(Failure);
    }

    // Bind: flatMap / chain
    public Result<TNewSuccess, TFailure> Bind<TNewSuccess>(Func<TSuccess, Result<TNewSuccess, TFailure>> binder)
    {
        return IsSuccess ? binder(Success) : Result<TNewSuccess, TFailure>.Fail(Failure);
    }

    // Static Try: wraps try/catch blocks into Result
    public static async Task<Result<TSuccess, TFailure>> TryCatchAsync(
        Func<Task<TSuccess>> action, 
        Func<Exception, TFailure> exceptionHandler)
    {
        try
        {
            var result = await action();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Fail(exceptionHandler(ex));
        }
    }
}
