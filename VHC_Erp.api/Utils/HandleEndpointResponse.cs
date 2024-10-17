using VHC_Erp.Shared.SharedLogic;

namespace VHC_Erp.api.Utils;

public static class HandleEndpointResponse
{
    public static IResult HandleResponse<T>(this Maybe<T> r)
    {
        return r.Exists ? Results.Json(r.Value, statusCode: r.HttpCode) : Results.Json(r.Error, statusCode:r.HttpCode);
    }
    public static IResult HandleResponse<T>(this Option<T> r)
    {
        return r.Match(
            Some: value => Results.Json(value, statusCode: 200),
            None: (err, code) => Results.Json(err.Split(["\n"], StringSplitOptions.None), statusCode: code)
        );
    }
}