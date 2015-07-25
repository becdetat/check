namespace Checkk.Exceptions
{
    public class InvariantFieldException : InvariantException
    {
        protected readonly string FieldName;

        public InvariantFieldException(string fieldName, string message)
            : base(message)
        {
            FieldName = fieldName;
        }

        protected override string AutoMessage => $"{FieldName} is invalid";
    }
}