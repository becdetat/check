namespace Checkk.Exceptions
{
    public class InvariantShouldBeValidEmailAddressException : InvariantFieldException
    {
        private readonly string _received;

        public InvariantShouldBeValidEmailAddressException(string fieldName, string recieved, string message)
            : base(fieldName, message)
        {
            _received = recieved;
        }

        protected override string AutoMessage => $"\"{_received}\" should be a valid email address";
    }
}