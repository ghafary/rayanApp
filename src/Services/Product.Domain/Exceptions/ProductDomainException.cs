﻿namespace Product.Domain.Exceptions;

/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class ProductDomainException : Exception
{
    public ProductDomainException()
    { }

    public ProductDomainException(string message)
        : base(message)
    { }

    public ProductDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
