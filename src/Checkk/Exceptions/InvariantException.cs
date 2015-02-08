using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Checkk.Exceptions
{
    public class InvariantException : Exception
    {
        private readonly string _message;
        protected InvariantException(string message)
        {
            _message = message;
        }

        protected static string Niceify(Expression body)
        {
            var regex = new Regex("value\\(.+?\\)\\.");
            return regex.Replace(body.ToString(), "");
        }

        protected virtual string AutoMessage
        {
            get { return null; }
        }

        public override string Message
        {
            get
            {
                return string.IsNullOrEmpty(_message) 
                    ? AutoMessage : 
                    string.Format(
                        "{0}{1}{2}", 
                        _message, 
                        Environment.NewLine, 
                        AutoMessage);
            }
        }
    }
}