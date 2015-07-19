using System;
using System.Linq.Expressions;

namespace Checkk.Exceptions
{
    public class InvariantShouldBeFalseException : InvariantException
    {
        private readonly Expression<Func<bool>> _target;

        public InvariantShouldBeFalseException(
            Expression<Func<bool>> target,
            string message)
            : base(message)
        {
            _target = target;
        }

        protected override string AutoMessage
        {
            get
            {
                return string.Format(
                    "{0} should be false",
                    Niceify(_target.Body));
            }
        }
    }
}