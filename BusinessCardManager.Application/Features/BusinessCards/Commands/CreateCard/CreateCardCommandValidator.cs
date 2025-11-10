using BusinessCardManager.Domain.ValueObjects;
using FluentValidation;

namespace BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard
{
    public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required."); // Basic check

            RuleFor(v => v.Phone)
                .NotEmpty().WithMessage("Phone is required.");

            RuleFor(v => v.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(BeAValidGender).WithMessage("Please specify a valid gender (Male, Female, Other).");

            RuleFor(v => v.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.")
                .LessThan(DateTime.Today).WithMessage("Date of Birth must be in the past.");

            RuleFor(v => v.PhotoBase64)
                .Must(BeAValidBase64AndSize).When(v => !string.IsNullOrEmpty(v.PhotoBase64))
                .WithMessage("Photo must be a valid Base64 string and under 1MB.");
        }

        private bool BeAValidGender(string gender)
        {
            try
            {
                Gender.FromString(gender);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        private bool BeAValidBase64AndSize(string? base64)
        {
            if (string.IsNullOrEmpty(base64)) return true;

            try
            {

                if (base64.Length > 1_400_000) return false;

                Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
                return Convert.TryFromBase64String(base64, buffer, out _);
            }
            catch
            {
                return false;
            }
        }
    }
}
