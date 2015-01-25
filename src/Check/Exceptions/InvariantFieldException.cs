namespace Check.Exceptions
{
    public class InvariantFieldException : InvariantException
    {
        protected readonly string FieldName;

        public InvariantFieldException(string fieldName)
        {
            FieldName = fieldName;
        }

        public override string Message
        {
            get { return string.Format("{0} is invalid", FieldName); }
        }
    }
}