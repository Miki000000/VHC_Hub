
using Mapster;

namespace VHC_Erp.Shared.SharedLogic;


public abstract record Option<T>{};

public sealed record Some<T>(bool Success, T Value, int StatusCode, Metadata Metadata) : Option<T>;
public sealed record None<T>(bool Sucess, string Error, int ErrorCode, Metadata Metadata) : Option<T>;
public sealed record Metadata(DateTime TimeStamp, string version);
public static class OptionExtensions
{
    public static Some<T> Some<T>(this T data) => new Some<T>(true, data, 200, new Metadata(DateTime.Now, "1.0"));
    public static Some<T> Some<T>(this object data) => new Some<T>(true, data.Adapt<T>(), 200, new Metadata(DateTime.Now, "1.0"));
    public static None<T> None<T>(this T _, string error) => new None<T>(false, error, 500, new Metadata(DateTime.Now, "1.0"));
    public static None<T> None<T>(this T _, string error, int errorCode) => new None<T>(false, error, errorCode, new Metadata(DateTime.Now, "1.0"));
    public static None<T> None<T>(this object _, string error) => new None<T>(false, error, 500, new Metadata(DateTime.Now, "1.0"));
    public static None<T> None<T>(this  object _, string error, int errorCode) => new None<T>(false, error, errorCode, new Metadata(DateTime.Now, "1.0"));
    
}
