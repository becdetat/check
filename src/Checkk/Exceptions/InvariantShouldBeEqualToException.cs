namespace Checkk.Exceptions
{
    public class InvariantShouldBeEqualToException<T> : InvariantFieldException
    {
        private readonly T _received;
        private readonly T _expected;

        public InvariantShouldBeEqualToException(string fieldName, string message, T received, T expected) : base(fieldName, message)
        {
            _received = received;
            _expected = expected;
        }

        protected override string AutoMessage => $"\"{_received}\" should be equal to \"{_expected}\"";
    }
}