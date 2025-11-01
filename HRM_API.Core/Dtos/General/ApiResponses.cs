namespace HRM_API.Core.Dtos.General
{
    public record ApiResponse<T>(
        bool Success,
        string? Message = null,
        string? Code = null,
        T? Data = default,
        object? Errors = null,
        bool NeedsRefresh = false
    );

    public static class ApiResponses
    {
        public static ApiResponse<T> Ok<T>(T data, string? message = null, string? code = null)
            => new(true, message, code, data);

        public static ApiResponse<object> Ok(string? message = null, string? code = null)
            => new(true, message, code, null);

        public static ApiResponse<object> Fail(string code, string message, object? errors = null, bool needsRefresh = false)
            => new(false, message, code, null, errors, needsRefresh);
        public static ApiResponse<object> TokenExpired()
        => new(false, "Token inválido o expirado", "401", null, null, true);
    }
}
