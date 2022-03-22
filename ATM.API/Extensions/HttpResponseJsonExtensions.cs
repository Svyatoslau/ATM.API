using ATM.API.Middlewares;

namespace Microsoft.AspNetCore.Http;

public static class HttpResponseJsonExtensions
{
    public static Task WriteJson(this HttpResponse response, ExceptionObjectResult result)
    {
        response.StatusCode = result.StatusCode;

        return response.WriteAsJsonAsync(result);
    }
}