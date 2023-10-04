using System.Collections.Generic;
using LoxLiaison.Utils;

namespace LoxLiaison.Functions
{
    /// <summary>
    /// Represents a callable mechanism within Lox.
    /// </summary>
    public interface ILoxCallable
    {
        /// <summary>
        /// Returns the number of arguments taken by this <see cref="ILoxCallable"/>.
        /// </summary>
        /// <returns>The number of arguments taken by this <see cref="ILoxCallable"/>.</returns>
        public int Arity();

        /// <summary>
        /// Calls this <see cref="ILoxCallable"/>.
        /// </summary>
        /// <param name="interpreter">The current <see cref="Interpreter"/> used to call this <see cref="ILoxCallable"/>.</param>
        /// <param name="arguments">A <see cref="List{T}"/> of <see cref="object"/>s representing arguments.</param>
        /// <returns>The result of this call.</returns>
        public object Call(Interpreter interpreter, List<object> arguments);
    }
}
