using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Checkk.Exceptions;

namespace Checkk
{
    /// <summary>
    /// Methods for checking a string
    /// </summary>
    public class CheckStringInvariant : CheckGenericInvariant<string>
    {
        public CheckStringInvariant(Expression<Func<string>> target)
            : base(target)
        {
        }

        /// <summary>
        /// Check that the target value is not null or empty.
        /// Throws an InvariantShouldNotBeNullOrEmptyException if the target is null or empty.
        /// </summary>
        /// <param name="message">An optional message that overrides the automatically generated one if the check fails</param>
        public void IsNotNullOrEmpty(string message = null)
        {
            if (string.IsNullOrEmpty(TargetValue))
            {
                throw new InvariantShouldNotBeNullOrEmptyException(FieldName, message);
            }
        }

        /// <summary>
        /// Check that the target value matches the regex.
        /// Throws an InvariantShouldMatchRegexException if the target does not match the regex.
        /// </summary>
        /// <param name="regex">The regex to test against the target value</param>
        /// <param name="message">An optional message that overrides the automatically generated one if the check fails</param>
        public void IsMatchForRegex(string regex, string message = null)
        {
            IsMatchForRegex(new Regex(regex), message);
        }

        /// <summary>
        /// Check that the target value matches the regex.
        /// Throws an InvariantShouldMatchRegexException if the target does not match the regex.
        /// </summary>
        /// <param name="regex">The regex to test against the target value</param>
        /// <param name="message">An optional message that overrides the automatically generated one if the check fails</param>
        public void IsMatchForRegex(Regex regex, string message = null)
        {
            if (!regex.IsMatch(TargetValue))
            {
                throw new InvariantShouldMatchRegexException(FieldName, TargetValue, regex, message);
            }
        }
    }
}