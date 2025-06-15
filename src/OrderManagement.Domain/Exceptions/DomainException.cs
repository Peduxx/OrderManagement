﻿namespace OrderManagement.Domain.Exceptions
{
    public sealed class DomainException : Exception
    {
        public DomainException() { }

        public DomainException(string message) : base(message) { }
    }
}
