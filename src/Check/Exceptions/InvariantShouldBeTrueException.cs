using System;
using System.Linq.Expressions;

namespace Check.Exceptions
{
    public class InvariantShouldBeTrueException : InvariantException
    {
        private readonly Expression<Func<bool>> _target;

        public InvariantShouldBeTrueException(Expression<Func<bool>> target)
        {
            _target = target;
        }

        public override string Message
        {
            get
            {
                return string.Format(
                    "{0} should be true",
                    Niceify(_target.Body));
            }
        }
    }
}