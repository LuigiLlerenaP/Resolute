using Resolute.Common.ProblemsConst;
using Resolute.Faults;

namespace Resolute.Common.Problems
{
    /// <summary>
    /// Factory de <see cref="HttpFault"/> predefinidos para los errores HTTP más comunes.
    /// Úsalos cuando la falla de dominio mapea directamente a un código HTTP estándar
    /// y no necesitas un mensaje personalizado.
    /// </summary>
    public static class FaultsHttp
    {
        /// <summary>400 — datos de entrada inválidos o mal formateados.</summary>
        public static HttpFault BadRequest(string code)
            => new(CodesErrors.BadRequest, code, MessagesErrors.BadRequest);

        /// <summary>401 — el cliente no está autenticado.</summary>
        public static HttpFault Unauthorized(string code)
            => new(CodesErrors.Unauthorized, code, MessagesErrors.Unauthorized);

        /// <summary>403 — el cliente no tiene permiso para esta operación.</summary>
        public static HttpFault Forbidden(string code)
            => new(CodesErrors.Forbidden, code, MessagesErrors.Forbidden);

        /// <summary>404 — el recurso solicitado no existe.</summary>
        public static HttpFault NotFound(string code)
            => new(CodesErrors.NotFound, code, MessagesErrors.NotFound);

        /// <summary>405 — el método HTTP usado no está permitido.</summary>
        public static HttpFault MethodNotAllowed(string code)
            => new(CodesErrors.MethodNotAllowed, code, MessagesErrors.MethodNotAllowed);

        /// <summary>409 — conflicto con el estado actual del recurso.</summary>
        public static HttpFault Conflict(string code)
            => new(CodesErrors.Conflict, code, MessagesErrors.Conflict);

        /// <summary>422 — errores de validación semántica.</summary>
        public static HttpFault UnprocessableEntity(string code)
            => new(CodesErrors.UnprocessableEntity, code, MessagesErrors.UnprocessableEntity);

        // ── 5xx Server Errors ─────────────────────────────────────────────────────

        /// <summary>500 — error inesperado del servidor.</summary>
        public static HttpFault InternalServerError(string code)
            => new(CodesErrors.InternalServerError, code, MessagesErrors.InternalServerError);

        /// <summary>501 — funcionalidad no implementada.</summary>
        public static HttpFault NotImplemented(string code)
            => new(CodesErrors.NotImplemented, code, MessagesErrors.NotImplemented);

        /// <summary>502 — respuesta inválida de un servidor upstream.</summary>
        public static HttpFault BadGateway(string code)
            => new(CodesErrors.BadGateway, code, MessagesErrors.BadGateway);

        /// <summary>503 — servidor temporalmente no disponible.</summary>
        public static HttpFault ServiceUnavailable(string code)
            => new(CodesErrors.ServiceUnavailable, code, MessagesErrors.ServiceUnavailable);


    }
}
