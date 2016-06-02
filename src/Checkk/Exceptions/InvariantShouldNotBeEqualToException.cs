namespace Checkk.Exceptions
{
    public class InvariantShouldNotBeEqualToException<T> : InvariantFieldException
    {
        private readonly T _received;
        private readonly T _expected;

        public InvariantShouldNotBeEqualToException(string fieldName, string message, T received, T expected)
            : base(fieldName, message)
        {
            _received = received;
            _expected = expected;
        }

        protected override string AutoMessage => $"\"{_received}\" should not be equal to \"{_expected}\"";
    }
}