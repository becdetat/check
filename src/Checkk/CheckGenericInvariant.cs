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
                var memberExpression = Target.Body as MemberExpression;
                if (memberExpression != null)
                {
                    return memberExpression.Member.Name;
                }

                var constantExpression = Target.Body as ConstantExpression;
                if (constantExpression != null)
                {
                    return $"Constant \"{constantExpression.Value}\"";
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

        /// <summary>
        /// Check that the target value is equal to the expected value (using .Equals())
        /// Throws an InvariantsShouldBeEqualToException if the target value does not equal the expected value.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="message">An optional message that overrides the automatically generated one if the check fails</param>
        public void IsEqualTo(T expected, string message = null)
        {
            if (!TargetValue.Equals(expected))
            {
                throw new InvariantShouldBeEqualToException<T>(FieldName, message, TargetValue, expected);
            }
        }

        /// <summary>
        /// Check that the target value is not equal to the expected value (using .Equals())
        /// Throws an InvariantsShouldBeEqualToException if the target value does equals the expected value.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="message">An optional message that overrides the automatically generated one if the check fails</param>
        public void IsNotEqualTo(T expected, string message = null)
        {
            if (TargetValue.Equals(expected))
            {
                throw new InvariantShouldNotBeEqualToException<T>(FieldName, message, TargetValue, expected);
            }
        }
    }
}