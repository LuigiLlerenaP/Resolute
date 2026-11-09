using Microsoft.AspNetCore.Http;
using Resolute.Contracts;
using Resolute.Failures;

namespace Resolute.Results;


/// <summary>
/// Provides HTTP-oriented extension methods to map <see cref="Result{T}"/> into Minimal API <see cref="IResult"/> responses.
/// </summary>
public static class ResultHttpExtensions
{
    /// <summary>
    /// Convierte un <see cref="Result{T}"/> en un <see cref="IResult"/> de Minimal APIs,
    /// ejecutando <paramref name="onSuccess"/> cuando es exitoso o generando un ProblemDetails RFC 9457 cuando falla.
    /// </summary>
    public static IResult ToIResult<T>(
        this Result<T> result,
        Func<T, IResult> onSuccess)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(onSuccess);

        return result.Match(
            onSuccess: onSuccess,
            onFailure: MapFaultToProblem
        );
    }

    /// <summary>
    /// Atajo para devolver 200 OK con el valor del Result si es exitoso, o ProblemDetails si falla.
    /// </summary>
    public static IResult ToOkResult<T>(this Result<T> result)
        => result.ToIResult(static value => TypedResults.Ok(value));

    /// <summary>
    /// Atajo para devolver 201 Created con la URI generada si es exitoso, o ProblemDetails si falla.
    /// </summary>
    public static IResult ToCreatedResult<T>(this Result<T> result, Func<T, string> uriFactory)
        => result.ToIResult(value => TypedResults.Created(uriFactory(value), value));

    /// <summary>
    /// Atajo para devolver 204 NoContent en comandos de borrado/actualización, o ProblemDetails si falla.
    /// </summary>
    public static IResult ToNoContentResult<T>(this Result<T> result)
        => result.ToIResult(static _ => TypedResults.NoContent());

    private static IResult MapFaultToProblem(IFault fault)
    {

        if (fault is AggregateFault aggregateFault)
        {
            var errorsDictionary = aggregateFault.Faults
                .GroupBy(static f => f.Code)
                .ToDictionary(
                    static g => g.Key,
                    static g => g.Select(static f => f.Message).ToArray()
                );

            return TypedResults.ValidationProblem(
                errors: errorsDictionary,
                detail: fault.Message,
                title: "Se encontraron múltiples errores de validación.",
                type: fault.Code
            );
        }

        var (statusCode, title) = ResolveStatusAndTitle(fault);

        return TypedResults.Problem(
            detail: fault.Message,
            statusCode: statusCode,
            title: title,
            type: $"https://httpstatuses.io/{statusCode}",
            extensions: new Dictionary<string, object?>
            {
                ["errorCode"] = fault.Code
            }
        );
    }

    private static (int StatusCode, string Title) ResolveStatusAndTitle(IFault fault)
    {
        if (fault is HttpFault httpFault)
        {
            return (httpFault.StatusCode, GetDefaultTitleForStatus(httpFault.StatusCode));
        }

        return fault.Type switch
        {
            FaultType.Validation => (StatusCodes.Status400BadRequest, "Error de validación de negocio"),
            FaultType.NotFound => (StatusCodes.Status404NotFound, "Recurso no encontrado"),
            FaultType.Conflict => (StatusCodes.Status409Conflict, "Conflicto con el estado actual"),
            FaultType.Unauthorized => (StatusCodes.Status401Unauthorized, "No autorizado"),
            FaultType.Forbidden => (StatusCodes.Status403Forbidden, "Acceso denegado"),
            FaultType.Unexpected => (StatusCodes.Status500InternalServerError, "Error interno del servidor"),
            _ => (StatusCodes.Status400BadRequest, "Error en la solicitud")
        };
    }

    private static string GetDefaultTitleForStatus(int statusCode) => statusCode switch
    {
        StatusCodes.Status400BadRequest => "Solicitud inválida",
        StatusCodes.Status401Unauthorized => "No autorizado",
        StatusCodes.Status403Forbidden => "Acceso prohibido",
        StatusCodes.Status404NotFound => "Recurso no encontrado",
        StatusCodes.Status405MethodNotAllowed => "Método no permitido",
        StatusCodes.Status409Conflict => "Conflicto de estado",
        StatusCodes.Status422UnprocessableEntity => "Error de validación semántica",
        StatusCodes.Status500InternalServerError => "Error interno del servidor",
        StatusCodes.Status501NotImplemented => "Funcionalidad no implementada",
        StatusCodes.Status502BadGateway => "Error de puerta de enlace",
        StatusCodes.Status503ServiceUnavailable => "Servicio no disponible",
        _ => "Error al procesar la solicitud"
    };
}