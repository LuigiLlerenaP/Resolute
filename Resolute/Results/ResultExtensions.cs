using Resolute.Failures;

namespace Resolute.Results
{
    /// <summary>
    /// Métodos de extensión para encadenar operaciones sobre <see cref="Result{T}"/>
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Verifica que el valor de un resultado exitoso cumpla una condición.
        /// </summary>
        /// <remarks>
        /// Tres casos posibles:
        /// <list type="bullet">
        ///   <item>Si el resultado ya era fallo → se propaga la misma referencia sin crear nada nuevo.</item>
        ///   <item>Si el predicado pasa → se devuelve el resultado original sin modificarlo.</item>
        ///   <item>Si el predicado falla → se devuelve un nuevo resultado fallido con el <paramref name="fault"/> indicado.</item>
        /// </list>
        /// </remarks>
        /// <typeparam name="T">Tipo del valor contenido en el resultado.</typeparam>
        /// <param name="result">Resultado sobre el cual validar.</param>
        /// <param name="predicate">Condición que debe cumplir el valor para que el resultado siga siendo exitoso.</param>
        /// <param name="fault">Falla a retornar si el predicado no se cumple.</param>
        /// <returns>
        /// El mismo <paramref name="result"/> si ya era fallo o si el predicado se cumple;
        /// un nuevo <see cref="Result{T}"/> fallido con <paramref name="fault"/> si el predicado falla.
        /// </returns>
        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Fault fault)
        {
            if (result.IsFailure)
            {
                return result;  // result.Fault!;  => Se crea un nuevo objeto Fault ,  y result solo seria la misma referncia con todo , asi podemos concaten vairo errres 
            }

            if (!predicate(result.Value!))
            {
                return fault;
            }

            return result;
        }

        /// <summary>
        /// Verifica que el valor de un resultado exitoso cumpla una condición.
        /// </summary>
        /// <typeparam name="T">Tipo del valor contenido en el resultado.</typeparam>
        /// <param name="result">Resultado sobre el cual validar.</param>
        /// <param name="predicate">Condición que debe cumplir el valor para que el resultado siga siendo exitoso.</param>
        /// <param name="faultFactory">Fábrica de fallos a retornar si el predicado no se cumple.</param>
        /// <returns>
        /// El mismo <paramref name="result"/> si ya era fallo o si el predicado se cumple;
        /// un nuevo <see cref="Result{T}"/> fallido con <paramref name="faultFactory"/> si el predicado falla.
        /// </returns>
        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Func<T, Fault> faultFactory)
        {
            if (result.IsFailure)
            {
                return result;
            }

            if (!predicate(result.Value!))
            {
                return faultFactory(result.Value!);
            }

            return result;
        }


        /// <summary>
        /// Transforma el valor de un resultado exitoso a otro tipo, manteniendo la falla si el resultado original era fallido.
        /// </summary>
        /// <typeparam name="TIn">Valor a transformar</typeparam>
        /// <typeparam name="TOut">Tipo al que se transformará el valor</typeparam>
        /// <param name="result">Resultado sobre el cual operar</param>
        /// <param name="map">Función de transformación</param>
        /// <returns>Un nuevo resultado con el valor transformado</returns>
        public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> map)
        {
            if (result.IsFailure)
            {
                return result.Fault!;
            }

            return map(result.Value!);
        }

        /// <summary>
        /// Encadena una operación que también puede fallar.
        /// Si el resultado actual es fallo, la función nunca se ejecuta.
        /// </summary>
        /// <typeparam name="TIn">Tipo del valor de entrada.</typeparam>
        /// <typeparam name="TOut">Tipo del valor producido por <paramref name="bind"/>.</typeparam>
        /// <param name="result">Resultado sobre el cual encadenar.</param>
        /// <param name="bind">
        /// Función que recibe el valor exitoso y devuelve un nuevo <see cref="Result{TOut}"/>.
        /// A diferencia de <see cref="Map{TIn,TOut}"/>, esta función puede fallar.
        /// </param>
        /// <returns>
        /// El <see cref="Result{TOut}"/> producido por <paramref name="bind"/> si el resultado era exitoso;
        /// el mismo fault propagado si era fallo.
        /// </returns>
        public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> bind)
        {
            if (result.IsFailure)
            {
                return result.Fault!;
            }

            return bind(result.Value!);
        }

        /// <summary>
        /// Combina una colección de <see cref="Result{T}"/> en un único resultado agregado.
        /// Recorre todos los elementos sin detenerse en el primer fallo,
        /// acumulando todos los errores encontrados.
        /// </summary>
        /// <remarks>
        /// Dos casos posibles:
        /// <list type="bullet">
        ///   <item>
        ///     Todos exitosos → devuelve un <see cref="Result{T}"/> exitoso
        ///     con la lista de valores en el mismo orden de la colección.
        ///   </item>
        ///   <item>
        ///     Alguno falla → devuelve un <see cref="Result{T}"/> fallido con un
        ///     <see cref="AggregateFault"/> que agrupa todos los faults encontrados.
        ///     Los valores de los elementos exitosos se descartan.
        ///   </item>
        /// </list>
        /// Las listas internas se crean con <c>capacity</c> conocida para evitar
        /// re-allocations durante el recorrido.
        /// </remarks>
        /// <typeparam name="T">Tipo del valor contenido en cada <see cref="Result{T}"/>.</typeparam>
        /// <param name="resultSource">
        /// Colección de resultados a combinar. Si no es un array, se materializa
        /// una sola vez para evitar iterar un <see cref="IEnumerable{T}"/> perezoso dos veces.
        /// </param>
        /// <returns>
        /// Un <see cref="Result{T}"/> exitoso con <see cref="IReadOnlyList{T}"/> si todos fueron exitosos;
        /// un <see cref="Result{T}"/> fallido con <see cref="AggregateFault"/> si alguno falló.
        /// </returns>
        public static Result<IReadOnlyList<T>> Combine<T>(this IEnumerable<Result<T>> resultSource)
        {
            Result<T>[] results = resultSource as Result<T>[] ?? [.. resultSource];
            List<Fault>? faults = null;
            List<T>? values = null;

            foreach (Result<T> result in results)
            {
                if (result.IsFailure)
                {
                    faults ??= new List<Fault>(results.Length);
                    faults.Add(result.Fault!);
                    continue;
                }

                if (faults is null)
                {
                    values ??= new List<T>(results.Length);
                    values.Add(result.Value!);
                }
            }

            if (faults is not null)
            {
                return AggregateFault.From(faults);
            }

            return (values ?? []).AsReadOnly();
        }
    }
}