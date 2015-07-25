namespace Checkk.Exceptions
{
    public class InvariantShouldNotBeNullException : InvariantFieldException
    {
        public InvariantShouldNotBeNullException(string fieldName, string message)
            : base(fieldName, message)
        {
        }

        protected override string AutoMessage => $"{FieldName} should not be null";
    }
}