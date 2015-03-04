using System;
using System.Linq.Expressions;
using Checkk.Exceptions;
using Shouldly;
using Xunit;

namespace Checkk.Tests
{
    public class ExtensibilityTests
    {
        interface IValidatableViewModel
        {
            bool IsValid { get; }
        }

        class LoginViewModel : IValidatableViewModel
        {
            public string Name { get; set; }
            public string Password { get; set; }

            public bool IsValid
            {
                get { return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password); }
            }
        }

        class CheckValidatableViewModel : CheckGenericInvariant<IValidatableViewModel>
        {
            public CheckValidatableViewModel(Expression<Func<IValidatableViewModel>> target) 
                : base(target)
            {
            }

            public void IsValid()
            {
                if (!TargetValue.IsValid)
                {
                    throw new ViewModelMustBeValidException(FieldName);
                }
            }
        }

        class ViewModelMustBeValidException : InvariantFieldException
        {
            public ViewModelMustBeValidException(
                string fieldName,
                string message = null)
               : base(fieldName, message)
            {
            }

            public override string Message
            {
                get { return string.Format("{0} view model is invalid", FieldName); }
            }
        }

        [Fact]
        public void OnValidPasses()
        {
            var test = new LoginViewModel
            {
                Name = "name",
                Password = "password",
            };

            Should.NotThrow(() =>
                Check
                    .Yourself<IValidatableViewModel, CheckValidatableViewModel>(() => test)
                    .IsValid());
        }

        [Fact]
        public void OnNotValidFails()
        {
            var test = new LoginViewModel
            {
                Name = "name",
                Password = "",
            };

            Should.Throw<ViewModelMustBeValidException>(() =>
                Check
                    .Yourself<IValidatableViewModel, CheckValidatableViewModel>(() => test)
                    .IsValid());
        }

        [Fact]
        public void FailureMessageIsValid()
        {
            var test = new LoginViewModel
            {
                Name = "name",
                Password = "",
            };

            var exception = Should.Throw<ViewModelMustBeValidException>(() =>
                Check
                    .Yourself<IValidatableViewModel, CheckValidatableViewModel>(() => test)
                    .IsValid());

            exception.Message.ShouldBe("test view model is invalid");
        }
    }
}
