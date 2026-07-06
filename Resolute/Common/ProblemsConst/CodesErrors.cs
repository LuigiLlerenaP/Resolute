namespace Resolute.Common.ProblemsConst
{
    /// <summary>
    /// Numeric HTTP status codes used throughout the library.
    /// </summary>
    public static class CodesErrors
    {
        // 2xx Success (Éxito)
        /// <summary>200 OK</summary>
        public const int Ok = 200;

        /// <summary>201 Created</summary>
        public const int Created = 201;

        /// <summary>202 Accepted</summary>
        public const int Accepted = 202;

        /// <summary>204 No Content</summary>
        public const int NoContent = 204;

        // 3xx Redirection (Redirección)
        /// <summary>301 Moved Permanently</summary>
        public const int MovedPermanently = 301;

        /// <summary>302 Found</summary>
        public const int Found = 302;

        /// <summary>304 Not Modified</summary>
        public const int NotModified = 304;

        // 4xx Client Errors (Errores del Cliente)
        /// <summary>400 Bad Request</summary>
        public const int BadRequest = 400;

        /// <summary>401 Unauthorized</summary>
        public const int Unauthorized = 401;

        /// <summary>403 Forbidden</summary>
        public const int Forbidden = 403;

        /// <summary>404 Not Found</summary>
        public const int NotFound = 404;

        /// <summary>405 Method Not Allowed</summary>
        public const int MethodNotAllowed = 405;

        /// <summary>409 Conflict</summary>
        public const int Conflict = 409;

        /// <summary>422 Unprocessable Entity (commonly used for validation errors)</summary>
        public const int UnprocessableEntity = 422; // Muy usado para errores de validación

        // 5xx Server Errors (Errores del Servidor)
        /// <summary>500 Internal Server Error</summary>
        public const int InternalServerError = 500;

        /// <summary>501 Not Implemented</summary>
        public const int NotImplemented = 501;

        /// <summary>502 Bad Gateway</summary>
        public const int BadGateway = 502;

        /// <summary>503 Service Unavailable</summary>
        public const int ServiceUnavailable = 503;
    }
}
