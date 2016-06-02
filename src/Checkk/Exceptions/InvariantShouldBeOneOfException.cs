using System.Linq;

namespace Checkk.Exceptions
{
    public class InvariantShouldBeOneOfException<T> : InvariantFieldException
    {
        private readonly T[] _expected;
        private readonly T _targetValue;

        public InvariantShouldBeOneOfException(string fieldName, T targetValue, T[] expected)
            : base(fieldName, string.Empty)
        {
            _targetValue = targetValue;
            _expected = expected;
        }

        protected override string AutoMessage
        {
            get
            {
                var expectedValues = string.Join(
                    ",",
                    _expected.Select(x => $@"""{x}"""));

                return $@"""{_targetValue}"" should be one of [{expectedValues}]";
            }
        }
    }
}