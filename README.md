Check
=====

A domain invariants helper library.

	Check.That(() => firstName).IsNotNull();
	Check.That(() => lastName).IsNotNullOrEmpty();
	Check.That(() => dateOfBirth.Year < 1989);

Failing checks throw an exception inheriting from `InvariantException`:

	var i1 = 10;
	Check.That(() => i1 > 20);	// throws InvariantShouldBeTrueException: "(i1 > 20) should be true"

Check can be extended with custom invariant checkers. Create a checker class that inherits from `CheckGenericInvariant<T>` and pass it to `Check.That` as a generic parameter for instantiation:

    Check.That<IValidatableViewModel, CheckValidatableViewModel>(() => loginViewModel);


## API

### Generic
The generic invariant checker is the base type of all invariant checkers and contains the shared logic. If the type of the expression passed into `Check.That` isn't handled by a checker, a generic invariant checker will be returned.

#### `IsNotNull`
Checks that the target expression result is not null.


### Boolean
The boolean invariant checker takes an expression that results in a boolean, and immediately checks for boolean true. The resulting exception message displays the entire compiled expression for easier debugging:

	var i1 = 10;
	Check.That(() => i1 > 20);	// throws InvariantShouldBeTrueException: "(i1 > 20) should be true"


### String
The string invariant checker contains check methods for strings.

#### `IsNotNullOrEmpty`
Checks that the target expression result is not null or empty.

	var occupation = "Unicorn Wrangler";

	Check.That(() => occupation).IsNotNullOrEmpty();


### Type
The type invariant checker contains check methods for `System.Type`.

#### HasPublicConstructorWithParameters
Checks that the type contains a public constructor that takes the provided parameter types (in order).

	Check.That(typeof(TTaxCalulator)).HasPublicConstructorWithParameters(
		typeof(ITaxConfiguration),
		typeof(TaxStatus));


### Extensibility

Extend Check with a custom invariant checker. Create a checker that implements `CheckGenericInvariant<T>`:

    public interface IValidatableViewModel
    {
        bool IsValid { get; }
    }

    public class CheckValidatableViewModel : CheckGenericInvariant<IValidatableViewModel>
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

The checker type and the target type both have to be passed to `Check.That` for the checker to resolve correctly.

	Check.That<IValidatableViewModel, CheckValidatableViewModel>(() => viewModel).IsValid()

> Note that this is a trivial example that could be implemented without a custom checker by simply executing `Check.That(() => viewModel.IsValid)`, but custom checkers with complex logic could be reused making the effort worthwhile.

 The checker type is instantiated using a public constructor that takes `Expression<Func<T>>` only, so make sure the constructor is correct for the checker.


## License



