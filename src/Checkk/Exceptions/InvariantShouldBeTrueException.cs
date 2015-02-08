using System;
using System.Linq.Expressions;

namespace Checkk.Exceptions
{
    public class InvariantShouldBeTrueException : InvariantException
    {
        private readonly Expression<Func<bool>> _target;

        public InvariantShouldBeTrueException(
            Expression<Func<bool>> target)
            : base(null)
        {
            _target = target;
        }

        protected override string AutoMessage
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