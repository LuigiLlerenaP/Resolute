using Resolute.Failure;
using Resolute.Faults;

namespace Resolute.Results
{
    /// <summary>
    /// Representa el resultado de una operacion para su caso  correcto o para su falla si existe
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Result<T>
    {
        /// <summary>Indica si la operación terminó en éxito.</summary>
        public bool IsSuccess { get; }

        /// <summary>Indica si la operación terminó en fallo. Es el inverso de <see cref="IsSuccess"/>.</summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Valor producido por la operación. Solo es no-nulo cuando <see cref="IsSuccess"/> es true.
        /// </summary>
        public T? Value { get; }

        /// <summary>
        /// Error producido por la operación. Solo es no-nulo cuando <see cref="IsFailure"/> es true.
        /// </summary>
        public Fault? Fault { get; }

        /// <summary>
        /// Constructor privado para crear una instancia de <see cref="Result{T}"/>. Se utiliza internamente por los métodos estáticos <see cref="Success(T)"/> y <see cref="Failure(Fault)"/>. 
        /// </summary>
        /// <param name="isSuccess">Indica si la operación fue exitosa.</param>
        /// <param name="value">El valor producido por la operación.</param>
        /// <param name="fault">El error producido por la operación.</param>
        private Result(bool isSuccess, T? value ,  Fault? fault = null) {
            IsSuccess = isSuccess;
            Value = value;
            Fault = fault;  
        }

        /// <summary>
        /// Crea un resultado exitoso con el valor proporcionado. Lanza una excepción si el valor es nulo.
        /// </summary>
        /// <param name="value">El valor producido por la operación.</param>
        /// <returns></returns>
        /// <exception cref="InvalidResultException">Lanza una excepción si el valor es nulo.</exception>
        public static Result<T> Success(T value)
        {
            if (value is null )
            {
                throw new InvalidResultException(
                    resultState: typeof(T).Name ,
                    operation: nameof(Success),
                    message: $"Cannot create a successful Result<{typeof(T).Name}> with a null value."
                );
            }
                return new Result<T>(true, value);
        }

        /// <summary>
        /// Crea un resultado fallido con el error proporcionado. Lanza una excepción si el error es nulo.
        /// </summary>
        /// <param name="fault">El error producido por la operación.</param>
        /// <returns></returns>
        /// <exception cref="InvalidResultException">Lanza una excepción si el error es nulo.</exception>
        public static Result<T> Failure(Fault fault)
        {
            if (fault is null)
            {
                throw new InvalidResultException(
                    resultState: typeof(T).Name,
                    operation: nameof(Failure),
                    message: $"Cannot create a failed Result<{typeof(T).Name}> with a null error."
                );
            }
            return new Result<T>(false, default, fault);
        }

        /// <summary>
        /// Conversión implícita: permite retornar un valor de tipo <typeparamref name="T"/>
        /// directamente donde se espera un <see cref="Result{T}"/>, envolviéndolo como éxito.
        /// </summary>
        public static implicit operator Result<T>(T value) => Success(value);

        /// <summary>
        /// Conversión implícita: permite retornar un <see cref="Error"/> directamente donde
        /// se espera un <see cref="Result{T}"/>, envolviéndolo como fallo.
        /// </summary>
        public static implicit operator Result<T>(Fault fault) => Failure(fault);

        /// <summary>
        ///  Permite unir el resultado de una operación con dos funciones: una para manejar el caso de éxito y otra para manejar el caso de fallo.
        /// </summary>
        /// <typeparam name="TResult">El tipo del resultado de las funciones.</typeparam>
        /// <param name="onSuccess">La función para manejar el caso de éxito.</param>
        /// <param name="onFailure">La función para manejar el caso de fallo.</param>
        /// <returns></returns>
        public TResult Match <TResult>(Func<T, TResult> onSuccess , Func<Fault, TResult> onFailure) 
        {
            if (IsFailure)
            {
                return onFailure(Fault!);
            }

            return onSuccess(Value!);
        }
    }
}
