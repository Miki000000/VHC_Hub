using VHC_Erp.Shared.SharedLogic;

namespace VHC_Erp.api.Utils;

public static class HandleEndpointResponse
{
    public static IResult HandleResponse<T>(this Maybe<T> r)
    {
        return r.Exists ? Results.Json(r.Value, statusCode: r.HttpCode) : Results.Json(r.Error, statusCode:r.HttpCode);
    }
    public static IResult HandleResponse<T>(this Option<T> res)
    {
        return res switch
        {
            Some<T> response => Results.Json(response.Value, statusCode: response.StatusCode),
            None<T> response => Results.Json(data: new 
            {
                success = response.Sucess,
                error = new
                {
                    errorCode = response.ErrorCode,
                    errorMessage = response.Error.Split(["\n"], StringSplitOptions.None)
                },
                metadata = response.Metadata 
            }, statusCode: response.ErrorCode),
            _ => Results.Problem("Unknown server problem.", statusCode: 500)
        };
    }
}