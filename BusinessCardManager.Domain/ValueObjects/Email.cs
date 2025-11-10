using BusinessCardManager.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace BusinessCardManager.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled);

        public string Value { get; private set; }

        private Email() { }

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidEmailException("Email cannot be empty.");
            }

            email = email.Trim().ToLower();

            if (!EmailRegex.IsMatch(email))
            {
                throw new InvalidEmailException($"'{email}' is not a valid email address.");
            }

            return new Email { Value = email };
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}