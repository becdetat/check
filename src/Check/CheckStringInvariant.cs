using System;
using System.Linq.Expressions;
using Check.Exceptions;

namespace Check
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
        public void IsNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(TargetValue))
            {
                throw new InvariantShouldNotBeNullOrEmptyException(FieldName);
            }
        }
    }
}