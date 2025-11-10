using BusinessCardManager.Domain.Exceptions;
using System.Text.RegularExpressions;
namespace BusinessCardManager.Domain.ValueObjects
{
    public class Phone : ValueObject
    {
        private static readonly Regex NonNumericRegex = new Regex(@"[^\d]", RegexOptions.Compiled);

        public string Value { get; private set; }

        private Phone() { }

        public static Phone Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new InvalidPhoneException("Phone number cannot be empty.");
            }

            var cleanedNumber = NonNumericRegex.Replace(phoneNumber, "");

            if (cleanedNumber.Length < 7)
            {
                throw new InvalidPhoneException("Phone number is not valid.");
            }

            return new Phone { Value = cleanedNumber };
        }

        public static implicit operator string(Phone phone)
        {
            return phone.Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}