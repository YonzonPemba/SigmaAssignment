using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SigmaAssignment.Controllers;
using SigmaAssignment.Data.DTO;
using SigmaAssignment.Services.Interfaces;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace SigmaAssignment.Tests
{
    [TestFixture]
    public class CandidateControllerTest
    {
        private Mock<ICandidateService> _candidateServiceMock;
        private Mock<IValidator<CandidateDTO>> _validatorMock;
        private Mock<ILogger<CandidateController>> _loggerMock;
        private CandidateController _controller;

        [SetUp]
        public void Setup()
        {
            _candidateServiceMock = new Mock<ICandidateService>();
            _validatorMock = new Mock<IValidator<CandidateDTO>>();
            _loggerMock = new Mock<ILogger<CandidateController>>();
            _controller = new CandidateController(_candidateServiceMock.Object, _validatorMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task AddOrUpdateCandidate_ValidationFails_ReturnsBadRequest()
        {
            // Arrange
            var candidateDto = new CandidateDTO { FirstName = "Hari", LastName = "Mahat", Email = "invalid-email" };
            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("Email", "Email is invalid")
            };

            _validatorMock
                .Setup(v => v.Validate(candidateDto))
                .Returns(new ValidationResult(validationErrors));

            // Act
            var result = await _controller.AddOrUpdateCandidate(candidateDto) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(400));

            var response = result.Value as BaseResponse<CandidateDTO>;
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.False);
            Assert.That(response.Message, Is.EqualTo("Validation failed"));
            Assert.That(response.Errors, Is.EqualTo("Email is invalid"));
        }

        [Test]
        public async Task AddOrUpdateCandidate_CreatesCandidate_ReturnsOkResponse()
        {
            // Arrange
            var candidateDto = new CandidateDTO { FirstName = "Hari", LastName = "Mahat", Email = "hari.mahat@example.com", FreeTextComment = "An example of free text comment" };
            var serviceResponse = new CandidateDTO { FirstName = "Hari", LastName = "Mahat", Email = "hari.mahat@example.com", IsUpdated = false };

            _validatorMock
                .Setup(v => v.Validate(candidateDto))
                .Returns(new ValidationResult());

            _candidateServiceMock
                .Setup(s => s.AddOrUpdateCandidateAsync(candidateDto))
                .ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.AddOrUpdateCandidate(candidateDto) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var response = result.Value as BaseResponse<CandidateDTO>;
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.False);
            Assert.That(response.Message, Is.EqualTo("Candidate added successfully."));
        }

        [Test]
        public async Task AddOrUpdateCandidate_UpdatesCandidate_ReturnsOkResponse()
        {
            // Arrange
            var candidateDto = new CandidateDTO { FirstName = "Hari", LastName = "Mahat", Email = "hari.mahat@example.com", FreeTextComment = "An example of free text comment" };
            var serviceResponse = new CandidateDTO { FirstName = "Hari", LastName = "Mahat", Email = "hari.mahat@example.com", IsUpdated = true };

            _validatorMock
                .Setup(v => v.Validate(candidateDto))
                .Returns(new ValidationResult());

            _candidateServiceMock
                .Setup(s => s.AddOrUpdateCandidateAsync(candidateDto))
                .ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.AddOrUpdateCandidate(candidateDto) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var response = result.Value as BaseResponse<CandidateDTO>;
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.False);
            Assert.That(response.Message, Is.EqualTo("Candidate updated successfully."));
        }

        [Test]
        public async Task AddOrUpdateCandidate_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var candidateDto = new CandidateDTO { FirstName = "Hari", LastName = "Mahat", Email = "hari.mahat@example.com" };

            _validatorMock
                .Setup(v => v.Validate(candidateDto))
                .Returns(new ValidationResult());

            _candidateServiceMock
                .Setup(s => s.AddOrUpdateCandidateAsync(candidateDto))
                .ThrowsAsync(new Exception("Unable to Add Or Update the Candidate"));

            // Act
            var result = await _controller.AddOrUpdateCandidate(candidateDto) as ObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(500));

            var response = result.Value as BaseResponse<CandidateDTO>;
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.False);
            Assert.That(response.Message, Is.EqualTo("Internal Server Error"));
            Assert.That(response.Errors, Is.EqualTo("An unexpected error occurred.Please try again later."));
        }
    }
}
