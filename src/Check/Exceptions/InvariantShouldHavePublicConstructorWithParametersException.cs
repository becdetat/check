using System;
using System.Linq;
using System.Linq.Expressions;

namespace Check.Exceptions
{
    public class InvariantShouldHavePublicConstructorWithParametersException
        : InvariantException
    {
        private readonly Expression<Func<Type>> _target;
        private readonly Type[] _types;

        public InvariantShouldHavePublicConstructorWithParametersException(
            Expression<Func<Type>> target,
            Type[] types )
        {
            _target = target;
            _types = types;
        }

        public override string Message
        {
            get { return string.Format(
                "{0} should have a public constructor accepting ({1})",
                _target.Body,
                string.Join(", ", _types.Select(x => x.Name))); }
        }
    }
}