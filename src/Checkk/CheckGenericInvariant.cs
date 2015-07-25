using System;
using System.Linq.Expressions;
using Checkk.Exceptions;

namespace Checkk
{
    /// <summary>
    ///     Methods for checking a generic instance of T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CheckGenericInvariant<T>
    {
        protected readonly Expression<Func<T>> Target;

        public CheckGenericInvariant(Expression<Func<T>> target)
        {
            Target = target;
        }

        protected T TargetValue => Target.Compile()();

        protected string FieldName
        {
            get
            {
                var body = Target.Body as MemberExpression;
                if (body != null)
                {
                    return body.Member.Name;
                }

                throw new ArgumentException($"Cannot get field name for expression '{Target}', the target body is {Target.Body.GetType()}");
            }
        }

        /// <summary>
        ///     Check that the target value is not null.
        ///     Throws an InvariantShouldNotBeNullException if the target value is null.
        /// </summary>
        /// <param name="message">An optional message that overrides the automatically generated one if the check fails</param>
        public void IsNotNull(string message = null)
        {
            if (object.ReferenceEquals(TargetValue, null))
            {
                throw new InvariantShouldNotBeNullException(FieldName, message);
            }
        }
    }
}