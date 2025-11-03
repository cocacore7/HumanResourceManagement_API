using HRM_API.Application.Helpers;

namespace HRM_API.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, JwtService jwtService)
        {
            var path = context.Request.Path.Value?.ToLower();

            if (path != null &&
                (path.Contains("/auth/login") ||
                 path.Contains("/auth/refreshtoken") ||
                 path.Contains("/auth/sendrecoverycode") ||
                 path.Contains("/auth/generatenewpassword") ||
                 path.Contains("/catalog/getjobcatalogpublic") ||
                 path.Contains("/catalog/gettowncatalogpublic") ||
                 path.Contains("/catalog/getrolecatalopublic") ||
                 path.Contains("/catalog/getactivejobcatalogpublic") ||
                 path.Contains("/form/setformanswerspublic") ||
                 path.Contains("/preapplication/preappvalidatepublic") ||
                 path.Contains("/preapplication/setpreapplicationspublic")))
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                var principal = jwtService.ValidateToken(token);

                if (principal != null)
                {
                    context.User = principal;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        success = false,
                        message = "Token inválido o expirado",
                        code = "401",
                        needsRefresh = true
                    });
                    return;
                }
            }

            await _next(context);
        }
    }

    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtMiddleware>();
        }
    }
}
