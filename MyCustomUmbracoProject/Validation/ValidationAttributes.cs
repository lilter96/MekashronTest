using System.ComponentModel.DataAnnotations;

namespace MyCustomUmbracoProject.Validation;

[AttributeUsage(AttributeTargets.Property)]
public class MustBeTrue : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value != null && value is bool && (bool)value;
    }
}

/// <summary>
///     Checks if the value is equal to null or is an empty string
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class MustBeEmpty : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value == null || (value is string && (string)value == "");
    }
}

/// <summary>
///     Checks if the value is not null and is an integer which is greater than zero
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class GreaterThanZero : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value != null && value is int && (int)value > 0;
    }
}