using VHC_Erp.Shared.SharedLogic;

namespace VHC_Erp.api.Utils;

public static class HandleEndpointResponse
{
    public static IResult HandleResponse<T>(this Maybe<T> r)
    {
        return r.Exists ? Results.Json(r.Value, statusCode: r.HttpCode) : Results.Json(r.Error, statusCode:r.HttpCode);
    }
}