using OrderManagement.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace OrderManagement.Domain.Entities
{
    public class Customer : BaseEntity
    {
        private static readonly Regex EmailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Name { get; private set; }
        public string Email { get; private set; }

        private Customer(string name, string email)
        {
            Name = name.Trim();
            Email = email.Trim().ToLowerInvariant();
        }

        public static Customer Create(string name, string email)
        {
            ValidateName(name);
            ValidateEmail(email);

            return new Customer(name, email);
        }

        public void Update(string name, string email)
        {
            if(IsDeleted)
                throw new DomainException("Cannot update a deleted customer.");

            ValidateName(name);
            ValidateEmail(email);

            Name = name.Trim();
            Email = email.Trim().ToLowerInvariant();
            SetUpdatedAt();
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Customer name cannot be null or empty.");

            if (name.Trim().Length < 2)
                throw new DomainException("Customer name must have at least 2 characters.");

            if (name.Trim().Length > 100)
                throw new DomainException("Customer name cannot exceed 100 characters.");
        }

        private static void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Customer email cannot be null or empty.");

            if (email.Trim().Length > 255)
                throw new DomainException("Customer email cannot exceed 255 characters.");

            if (!EmailRegex.IsMatch(email.Trim()))
                throw new DomainException("Customer email format is invalid.");
        }
    }
}