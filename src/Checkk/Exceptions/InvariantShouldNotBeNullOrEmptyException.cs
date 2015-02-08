namespace Checkk.Exceptions
{
    public class InvariantShouldNotBeNullOrEmptyException : InvariantFieldException
    {
        public InvariantShouldNotBeNullOrEmptyException(string fieldName, string message)
            : base(fieldName, message)
        {
        }

        protected override string AutoMessage
        {
            get { return string.Format("{0} should not be null or empty", FieldName); }
        }
    }
}