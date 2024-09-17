namespace VHC_Erp.Shared.SharedLogic;

public class Maybe<T>
{
    public T Value { get; private set; }
    public List<string>? Error { get; private set; }
    public bool Exists { get; private set; }
    public int HttpCode { get; private set; }

    private Maybe(T value, bool exists, string? error, int httpCode)
    {
        Value = value;
        Exists = exists;
        HttpCode = httpCode;
        if(!Exists)
        {
            Error = error?.Split(["\n"], StringSplitOptions.None).ToList();
        }
    }
    public static Maybe<T> None(string error, int errorCode) => new Maybe<T>(default!, false, error, errorCode);
    public static Maybe<T> Some(T value) => new Maybe<T>(value, true, default!, 200);
}