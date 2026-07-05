namespace Resolute.Common.ProblemsConst
{
    public static class MessagesErrors
    {
        // 4xx Client Errors
        public const string BadRequest = "The request is invalid or poorly formatted.";
        public const string Unauthorized = "Authentication is required to access this resource.";
        public const string Forbidden = "You do not have permission to access this resource.";
        public const string NotFound = "The requested resource could not be found.";
        public const string MethodNotAllowed = "The HTTP method used is not allowed for this resource.";
        public const string Conflict = "There is a conflict with the current state of the resource.";
        public const string UnprocessableEntity = "The request was well-formed but contains semantic errors (e.g., validation failed).";

        // 5xx Server Errors
        public const string InternalServerError = "An unexpected error occurred on the server.";
        public const string NotImplemented = "The server does not support the functionality required to fulfill the request.";
        public const string BadGateway = "The server received an invalid response from an upstream server.";
        public const string ServiceUnavailable = "The server is currently unable to handle the request due to temporary overloading or maintenance.";
    }
}
