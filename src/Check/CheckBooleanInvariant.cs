using System;
using System.Linq.Expressions;
using Check.Exceptions;

namespace Check
{
    /// <summary>
    /// Methods for checking a boolean. The constructor performs an immediate check for boolean true.
    /// </summary>
    public class CheckBooleanInvariant : CheckGenericInvariant<bool>
    {
        public CheckBooleanInvariant(
            Expression<Func<bool>> target) 
            : base(target)
        {
            if (!TargetValue)
            {
                throw new InvariantShouldBeTrueException(target);
            }
        }
    }
}