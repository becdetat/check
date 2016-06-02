# Check [![Build status](https://ci.appveyor.com/api/projects/status/3vi28q9scgpjgr4j/branch/master?svg=true)](https://ci.appveyor.com/project/bendetat/check/branch/master)


A class invariants helper library.

Note that this isn't a test assertion library. This is for enforcing class invariants via assertions, which is generally going to be a much smaller set of comparison types that you would need in a test assertions library such as [Shouldly]().


## Installation

Use [NuGet](https://www.nuget.org/packages/check):

    PM> Install-Package check


## Usage

    Check.That(() => firstName).IsNotNull();
    Check.That(() => lastName).IsNotNullOrEmpty();
    Check.That(() => dateOfBirth.Year < 1989).IsTrue();

Failing checks throw an exception inheriting from `InvariantException`:

    var i1 = 10;
    Check.That(() => i1 > 20);  // throws InvariantShouldBeTrueException: "(i1 > 20) should be true"

Check can be extended with custom invariant checkers. Create a checker class that inherits from `CheckGenericInvariant<T>` and pass it to `Check.That` as a generic parameter for instantiation:

    Check.That<IValidatableViewModel, CheckValidatableViewModel>(() => loginViewModel);


## About this thing

The easiest way to demonstrate why class invariants is something we want to use is the creation of a domain model - an instance of a `Person` for example. In our domain, when a person is created they may require a first name, a last name and an email address. We won't do any validation of the email address at this level, instead we'll assume that the address has already been validated. These three requirements will be our [business rules](http://en.wikipedia.org/wiki/Business_rule) for the use case of creating a person within the domain.

The requirements should be enforced within the piece of code that constructs the person - in this case the `Person` class's constructor. If the requirements were only enforced outside of the constructor, then for a start you're violating [Single Responsibility Principle](http://en.wikipedia.org/wiki/Single_responsibility_principle) as every piece of code that can construct a person needs to implement these requirements. Most of the person creation might be via a single form for someone doing data entry, so it appears to be fine to do this in the form handler. What happens when a data import tool gets added? If those requirements aren't implemented correctly in the import tool it could be a vector for bad (corrupt) data entering the system, which could have unpredicable effects.

So the requirements should really be implemented in the `Person` class constructor. This is pretty easy to implement:

    class Person
    {
        public Person(
            string firstName,
            string lastName,
            string emailAddress)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("First name is required", "firstName");
            }
            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Last name is required", "lastName");
            }
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException("Email address is required", "emailAddress");
            }

            // continue creating the person
        }
    }

The selling point of Check (and other assertion helper libraries) is that this can be expressed a lot more concisely:

    class Person
    {
        public Person(
            string firstName,
            string lastName,
            string emailAddress)
        {
            Check.That(() => firstName).IsNotNullOrEmpty("First name is required");
            Check.That(() => lastName).IsNotNullOrEmpty("Last name is required");
            Check.That(() => emailAddress).IsNotNullOrEmpty("Email address is required");

            // continue creating the person
        }
    }

This habit of asserting preconditions can be used outside of a domain model. Consider a simple function that, given natural numbers `a0` and `ak` calculates `a0 / a1 / a2 ... / ak`:

    double f(int a0, int ak)
    {
        var range = a0 < ak 
            ? Enumerable.Range(a0, ak - a0 + 1)
            : Enumerable.Range(ak, a0 - ak + 1).Reverse();
        
        return range.Skip(1).Aggregate(
            (double)range.First(),
            (r, an) => r / an);     
    }

Ok so it sounded simple at first. This will work for any input (until it overflows `double`'s precision which happens pretty quickly) but if the range crosses zero (eg `f(-2, 4)` or f(3, -3)`) the result is negative infinity. This is technically correct but may result in other errors, so we have a business rule where all inputs have to be greater than zero.

    double f(int a0, int ak)
    {
        Check.That(() => a0 > 0).IsTrue();
        Check.That(() => ak > 0).IsTrue();
        
        // ...

In practice, the domain requirements will probably end up getting copied over several layers instead of staying isolated in the `Person` constructor. This is because, pragmatically, a larger system will be spread over several layers and to actually ship quality, responsive code, you end up with domain validation in the UI. There's nothing inherently wrong with this as long as the system with the greatest responsibity is performing all of the checks required to enforce the business rules. In other words, the `Person` constructor is the last line of defence when the system is constructing a model of a person, and it should be responsible for implementing the requirements by executing the set of class invariants.

Further reading:

- [Invariant (Computer science)](http://en.wikipedia.org/wiki/Invariant_(computer_science))
- [Class invariant](http://en.wikipedia.org/wiki/Class_invariant)
- [Design Smell: Default Constructor](http://blog.ploeh.dk/2011/05/30/DesignSmellDefaultConstructor/)
- [Design by contract](http://en.wikipedia.org/wiki/Design_by_contract)
- [Assertion (software development)](http://en.wikipedia.org/wiki/Assertion_(software_development))



## API

Each `.Is` method takes an optional `message` parameter which overrides the automatically generated message.

	Check.That(() => true).IsTrue("Bending the laws of space and time, are we?");


### Generic
The generic invariant checker is the base type of all invariant checkers and contains the shared logic. If the type of the expression passed into `Check.That` isn't handled by a checker, a generic invariant checker will be returned.

#### `IsNotNull`
Checks that the target expression result is not null.

#### `IsEqualTo`
Checks that the target expression result is equal to the provided value (using `.Equals()` to perform the comparison).

#### `IsNotEqualTo`
Checks that the target expression result is not equal to the provided value (using `.Equals()` to perform the comparison).

### `IsOneOf` and `IsNotOneOf`
Checks that the target expression result is or is not one of the provided values (using `.Contains()` to perform the comparison).


### Boolean
The boolean invariant checker checks that an expression that results in a boolean is true or false. The resulting exception message displays the entire compiled expression for easier debugging:

	var i1 = 10;
	Check.That(() => i1 > 20).IsTrue();	// throws InvariantShouldBeTrueException: "(i1 > 20) should be true"

	Check.That(() => 20 > i1).IsFalse() // throws InvariantShouldBeFalseException


### String
The string invariant checker contains check methods for strings.

#### `IsNotNullOrEmpty`
Checks that the target expression result is not null or empty.

	var occupation = "Unicorn Wrangler";

	Check.That(() => occupation).IsNotNullOrEmpty();

#### `IsMatchForRegex`
Checks that the target expression matches the supplied regular expression.

	var phone = "(07) 1234 5678";

	Check.That(() => email).IsMatchForRegex(@"\([0]\d\) \d\d\d\d \d\d\d\d");


### Type
The type invariant checker contains check methods for `System.Type`.

#### `HasPublicConstructorWithParameters`
Checks that the type contains a public constructor that takes the provided parameter types (in order).

	Check.That(typeof(TTaxCalulator)).HasPublicConstructorWithParameters(
		typeof(ITaxConfiguration),
		typeof(TaxStatus));


### Guid
The `Guid` invariant checker contains check methods for `Guid`s.

#### `IsNotNullOrEmpty`
Checks that the target is not null or equal to `Guid.Empty`.

	public Entity(Guid id)
	{
		Check.That(() => id).IsNotNullOrEmpty();
	}
	

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

[Apache License Version 2.0](http://www.apache.org/licenses/LICENSE-2.0)

