using System;
using System.Linq.Expressions;

namespace Checkk
{
    public static class Check
    {
        /// <summary>
        /// Create a generic invariant check instance for a field
        /// </summary>
        /// <typeparam name="T">The type of the target field</typeparam>
        /// <param name="target">An expression whose body is a field</param>
        /// <returns>A generic invariant check instance for a field</returns>
        public static CheckGenericInvariant<T> That<T>(Expression<Func<T>> target)
        {
            return new CheckGenericInvariant<T>(target);
        }

        /// <summary>
        /// Create an invariant check instance for a string field
        /// </summary>
        /// <param name="target">An expression whose body is a string field</param>
        /// <returns>An invariant check instance for a string</returns>
        public static CheckStringInvariant That(Expression<Func<string>> target)
        {
            return new CheckStringInvariant(target);
        }

        /// <summary>
        /// An extensibility method to create a TCheck invariant for the specified target
        /// </summary>
        /// <typeparam name="T">The type of the target body</typeparam>
        /// <typeparam name="TCheck">The type of the invariant check instance for the body</typeparam>
        /// <param name="target">An expression whose body can be checked by the supplied TCheck type</param>
        /// <returns>An invariant check instance that can check the target</returns>
        public static TCheck That<T, TCheck>(Expression<Func<T>> target)
            where TCheck : CheckGenericInvariant<T>
        {
            Check.That(() => typeof(TCheck)).HasPublicConstructorWithParameters(typeof(Expression<Func<T>>));
            
            var constructor = typeof (TCheck).GetConstructor(new[] {typeof (Expression<Func<T>>)});

            Check.That(() => constructor).IsNotNull();

            return (TCheck) constructor.Invoke(new[] {target});
        }

        /// <summary>
        /// Create an invariant check instance for a Type:
        ///     Check.That(() => typeof(Foo))...
        /// </summary>
        /// <param name="target">An expression that returns a Type</param>
        /// <returns>A check invariant instance for a Type</returns>
        public static CheckTypeInvariant That(Expression<Func<Type>> target)
        {
            return new CheckTypeInvariant(target);
        }

        /// <summary>
        /// Create an invariant check instance for a boolean expression which executes
        /// immediately and checks for boolean true
        /// </summary>
        /// <param name="target">An expression that results in a boolean</param>
        /// <returns>A check invariant instance for a boolean</returns>
        public static CheckBooleanInvariant That(
            Expression<Func<bool>> target)
        {
            return new CheckBooleanInvariant(target);
        }
    }
}