using Microsoft.AspNetCore.Mvc;

namespace Retake.Atm.Api.Extensions;

public static class HttpResponseJsonExtensions
{
    public static HttpResponse WithStatusCode(this HttpResponse response, int statusCode)
    {
        response.StatusCode = statusCode;

        return response;
    }

    public static Task WithJsonContent(this HttpResponse response, object content)
    {
        return response.WriteAsJsonAsync(content);
    }
}