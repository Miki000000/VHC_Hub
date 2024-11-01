
using Mapster;

namespace VHC_Erp.Shared.SharedLogic;

public abstract record Option<T>{};

public sealed record Some<T>(T Value, int StatusCode) : Option<T>;
public sealed record None<T>(string Error, int ErrorCode) : Option<T>;

public static class OptionExtensions
{
    public static Some<T> Some<T>(this T data) => new Some<T>(data, 200);
    public static Some<T> Some<T>(this object data) => new Some<T>(data.Adapt<T>(), 200);
    public static None<T> None<T>(this T _, string error) => new None<T>(error, 500);
    public static None<T> None<T>(this T _, string error, int errorCode) => new None<T>(error, errorCode);
    public static None<T> None<T>(this object _, string error) => new None<T>(error, 500);
    public static None<T> None<T>(this  object _, string error, int errorCode) => new None<T>(error, errorCode);
    
}
