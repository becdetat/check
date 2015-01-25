namespace Check.Exceptions
{
    public class InvariantShouldNotBeNullOrEmptyException : InvariantFieldException
    {
        public InvariantShouldNotBeNullOrEmptyException(string fieldName)
            : base(fieldName)
        {
        }

        public override string Message
        {
            get { return string.Format("{0} should not be null or empty", FieldName); }
        }
    }
}