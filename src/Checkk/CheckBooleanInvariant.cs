using System;
using System.Linq.Expressions;
using Checkk.Exceptions;

namespace Checkk
{
    /// <summary>
    /// Methods for checking a boolean.
    /// </summary>
    public class CheckBooleanInvariant : CheckGenericInvariant<bool>
    {
        public CheckBooleanInvariant(
            Expression<Func<bool>> target) 
            : base(target)
        {
        }

        /// <summary>
        /// Check that the target value is true.
        /// Throws an InvariantShouldBeTrueException if the target is false.
        /// </summary>
        /// <param name="message">An optional message that overrides the automatically generated one if the check fails</param>
        public void IsTrue(string message = null)
        {
            if (!TargetValue)
            {
                throw new InvariantShouldBeTrueException(Target, message);
            }
        }

        /// <summary>
        /// Check that the target value is false.
        /// Throws an InvariantShouldBeFalseException if the target is true.
        /// </summary>
        /// <param name="message">An optional message that overrides the automatically generated one if the check fails</param>
        public void IsFalse(string message = null)
        {
            if (TargetValue)
            {
                throw new InvariantShouldBeFalseException(Target, message);
            }
        }
    }
}