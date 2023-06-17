using FluentAssertions;
using FluentValidation.Results;
using ProductApp.Application.Common.Exceptions;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {

        var actual = new ValidationException().Errors;

        actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Fact]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        var failures = new List<ValidationFailure>
            {
                new ValidationFailure("ManufactureEmail", "must be demo@gmail.com"),
            };

        var actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] { "ManufactureEmail" });
        actual["ManufactureEmail"].Should().BeEquivalentTo(new string[] { "ManufactureEmail." });
    }

    [Fact]
    public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        var failures = new List<ValidationFailure>
            {
                new ValidationFailure("ManufactureEmail", "must be demo@gmail.com"),
                new ValidationFailure("ManufactureEmail", "must not empty"),
            };

        var actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] { "Password", "Age" });

        actual["ManufactureEmail"].Should().BeEquivalentTo(new string[]
        {
                "must be demo@gmail.com",
                "must not empty",
        });
    }
}
