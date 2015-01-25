namespace Check.Exceptions
{
    public class InvariantShouldNotBeNullException : InvariantFieldException
    {
        public InvariantShouldNotBeNullException(string fieldName)
            : base(fieldName)
        {
        }

        public override string Message
        {
            get { return string.Format("{0} should not be null", FieldName); }
        }
    }
}