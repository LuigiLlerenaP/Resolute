namespace Resolute.Common.ProblemsConst
{
    public static class CodesErrors
    {
        // 2xx Success (Éxito)
        public const int Ok = 200;
        public const int Created = 201;
        public const int Accepted = 202;
        public const int NoContent = 204;

        // 3xx Redirection (Redirección)
        public const int MovedPermanently = 301;
        public const int Found = 302;
        public const int NotModified = 304;

        // 4xx Client Errors (Errores del Cliente)
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int Forbidden = 403;
        public const int NotFound = 404;
        public const int MethodNotAllowed = 405;
        public const int Conflict = 409;
        public const int UnprocessableEntity = 422; // Muy usado para errores de validación

        // 5xx Server Errors (Errores del Servidor)
        public const int InternalServerError = 500;
        public const int NotImplemented = 501;
        public const int BadGateway = 502;
        public const int ServiceUnavailable = 503;
    }
}
