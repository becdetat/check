using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Check.Exceptions
{
    public class InvariantException : Exception
    {
        protected static string Niceify(Expression body)
        {
            var regex = new Regex("value\\(.+?\\)\\.");
            return regex.Replace(body.ToString(), "");
        }
    }
}