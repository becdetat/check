using System;
using System.Linq.Expressions;
using Check;
using Checkk.Exceptions;

namespace Checkk
{
    /// <summary>
    /// Methods for checking a string
    /// </summary>
    public class CheckStringInvariant : CheckGenericInvariant<string>
    {
        public CheckStringInvariant(Expression<Func<string>> target)
            : base(target)
        {
        }

        /// <summary>
        /// Check that the target value is not null or empty.
        /// Throws an InvariantShouldNotBeNullOrEmptyException if the target is null or empty.
        /// </summary>
        /// <param name="message">An optional message that overrides the automatically generated one if the check fails</param>
        public void IsNotNullOrEmpty(string message = null)
        {
            if (string.IsNullOrEmpty(TargetValue))
            {
                throw new InvariantShouldNotBeNullOrEmptyException(FieldName, message);
            }
        }
    }
}