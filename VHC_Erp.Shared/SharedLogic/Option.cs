using Mapster;

namespace VHC_Erp.Shared.SharedLogic;

public class Option<T>
{
    internal T Value { get; }
    internal string Error { get; }
    internal int ErrorCode { get; }

    internal Option(T value, string error, int errorCode)
    {
        Value = value;
        Error = error;
        ErrorCode = errorCode;
    }

    public static Option<T> None(string error)
        => new Option<T>(default!, error, 500);
}

public static class OptionExtensions
{
    public static Option<T> Some<T>(this T value)
        => new Option<T>(value, default!, 200);
    public static Option<U> Some<T, U>(this T value)
        => new Option<U>(value.Adapt<U>(), default!, 200);
    public static Option<T> None<T>(this T value, string error)
        => new Option<T>(default!, error, 500);
    public static Option<U> None<T,U>(this T value, string error)
        => new Option<U>(default!, error, 500);
    public static Option<T> None<T>(this T value, string error, int errorCode)
        => new Option<T>(default!, error, errorCode);
    public static Option<U> None<T,U>(this T value, string error, int errorCode)
        => new Option<U>(default!, error, errorCode);
    public static TResult Match<T, TResult>(this Option<T> option, Func<T, TResult> Some, Func<string, int, TResult> None)
    {
        return option.Value is not null ? Some(option.Value) : None(option.Error, option.ErrorCode);
    }
    public static TResult Match<T, TResult>(this Option<T> option, Func<T, TResult> Some, Func<string, TResult> None)
    {
        return option.Value is not null ? Some(option.Value) : None(option.Error);
    }
}