namespace Bookshelf.Web.Extensions;

public static class HttpContextExtension
{
    public static string? GetValueFromSession(this HttpContext httpContext, string key) =>
        httpContext.Session.GetString(key);

    public static void SetValueInSession(this HttpContext httpContext, string key, string value) =>
        httpContext.Session.SetString(key, value);

    public static void ClearSession(this HttpContext httpContext) => httpContext.Session.Clear(); 
}
