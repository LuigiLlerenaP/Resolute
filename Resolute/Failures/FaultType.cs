namespace Resolute.Failures;

/// <summary>
/// Categoría semántica del fallo para facilitar el mapeo a capas de presentación (HTTP, gRPC, CLI).
/// </summary>
public enum FaultType
{
    /// <summary>Fallo de validación o regla de negocio (400 Bad Request / 422).</summary>
    Validation = 0,

    /// <summary>El recurso o entidad solicitada no existe (404 Not Found).</summary>
    NotFound = 1,

    /// <summary>Conflicto de estado o duplicidad de clave única (409 Conflict).</summary>
    Conflict = 2,

    /// <summary>Falta de autenticación válida (401 Unauthorized).</summary>
    Unauthorized = 3,

    /// <summary>Autenticado pero sin permisos suficientes (403 Forbidden).</summary>
    Forbidden = 4,

    /// <summary>Múltiples errores acumulados (400 ValidationProblem).</summary>
    Aggregate = 5,

    /// <summary>Fallo interno o inesperado del sistema (500 Internal Server Error).</summary>
    Unexpected = 6
}