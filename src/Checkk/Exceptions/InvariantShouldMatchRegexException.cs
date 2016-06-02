using System.Text.RegularExpressions;

namespace Checkk.Exceptions
{
    public class InvariantShouldMatchRegexException : InvariantFieldException
    {
        private readonly string _received;
        private readonly string _regex;

        public InvariantShouldMatchRegexException(string fieldName, string recieved, Regex regex, string message)
            : base(fieldName, message)
        {
            _received = recieved;
            _regex = regex.ToString();
        }

        protected override string AutoMessage => $"\"{_received}\" should match regular expression \"{_regex}\"";
    }
}