using FluentValidation;
using System.Globalization;

namespace SigmaAssignment.Data.DTO.Validators
{
    public class CandidateValidator : AbstractValidator<CandidateDTO>
    {
        public CandidateValidator()
        {
            RuleFor(candidate => candidate.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(candidate => candidate.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters.");

            RuleFor(candidate => candidate.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(50).WithMessage("Last Name cannot exceed 50 characters.");

            RuleFor(candidate => candidate.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.")
                .Matches(@"^\+?\d{10}$").WithMessage("Phone Number must be exactly 10 digits.");

            RuleFor(candidate => candidate.StartTime)
            .NotEmpty().WithMessage("Start Time is required.")
            .Must(BeAValidTimeSpan).WithMessage("Start Time must be in a valid format (hh:mm:ss).");

            RuleFor(candidate => candidate.EndTime)
                .NotEmpty().WithMessage("End Time is required.")
                .Must(BeAValidTimeSpan).WithMessage("End Time must be in a valid format (hh:mm:ss).");

            RuleFor(candidate => candidate)
                .Must(candidate => IsStartTimeEarlierThanEndTime(candidate.StartTime, candidate.EndTime))
                .WithMessage("Start Time must be earlier than End Time.");

            RuleFor(candidate => candidate.LinkedInProfileUrl)
                .NotEmpty().WithMessage("LinkedIn Profile URL is required.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var result) &&
                             (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
                .WithMessage("Invalid LinkedIn Profile URL format.");

            RuleFor(candidate => candidate.GitHubProfileUrl)
                .NotEmpty().WithMessage("GitHub Profile URL is required.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var result) &&
                             (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
                .WithMessage("Invalid GitHub Profile URL format.");

            RuleFor(candidate => candidate.FreeTextComment)
                .NotEmpty().WithMessage("FreeTextComment cannot be empty.");
        }

        private bool BeAValidTimeSpan(string time)
        {
            return TimeSpan.TryParseExact(time, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out _);
        }

        private bool IsStartTimeEarlierThanEndTime(string startTime, string endTime)
        {
            if (TimeSpan.TryParseExact(startTime, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out var start) &&
                TimeSpan.TryParseExact(endTime, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out var end))
            {
                return start < end;
            }

            return false;
        }
    }
}
