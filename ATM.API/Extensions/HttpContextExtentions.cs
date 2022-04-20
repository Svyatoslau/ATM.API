namespace Microsoft.AspNetCore.Http;

public static class HttpContextExtentions
{
    /// <summary>
    /// Try get a required header
    /// </summary>
    /// <param name="context"></param>
    /// <param name="header"></param>
    /// <returns></returns>
    public static string GetRequestHeader(this HttpContext context, string header)
    => context.Request.Headers.TryGetValue(header, out var headers)
        ? headers[0]
        : string.Empty;

    /// <summary>
    /// Verify card number
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cardNumber"></param>
    /// <returns></returns>
    public static bool HasCard(this HttpContext context, string cardNumber)
        => context.Request.Path.Value.Split('/').Contains(cardNumber);

}
