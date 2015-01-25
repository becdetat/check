namespace Check.Exceptions
{
    public class InvariantShouldNotBeNullException : InvariantFieldException
    {
        public InvariantShouldNotBeNullException(string fieldName, string message)
            : base(fieldName, message)
        {
        }

        protected override string AutoMessage
        {
            get { return string.Format("{0} should not be null", FieldName); }
        }
    }
}