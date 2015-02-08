using System;
using System.Linq;
using System.Linq.Expressions;
using Checkk.Exceptions;

namespace Checkk
{
    /// <summary>
    /// Methods for checking a System.Type
    /// </summary>
    public class CheckTypeInvariant : CheckGenericInvariant<Type>
    {
        public CheckTypeInvariant(Expression<Func<Type>> target) : base(target)
        {
        }

        /// <summary>
        /// Check that the target type has a public constructor that accepts the provided
        /// types, in the provided order.
        /// </summary>
        /// <param name="types">The types of the constructor parameters to search for</param>
        public void HasPublicConstructorWithParameters(
            params Type[] types)
        {
            if (!TargetValue.GetConstructors()
                .Any(x => types.SequenceEqual(x.GetParameters().Select(p => p.ParameterType))))
            {
                throw new InvariantShouldHavePublicConstructorWithParametersException(
                    Target,
                    types,
                    message: null);
            }
        }
    }
}