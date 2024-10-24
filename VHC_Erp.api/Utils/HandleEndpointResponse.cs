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
        return r switch
        {
            Some<T> response => Results.Json(response.Value, statusCode: response.StatusCode),
            None<T> response => Results.Json(response.Error.Split(["\n"], StringSplitOptions.None), statusCode: response.ErrorCode)
        };
    }
}