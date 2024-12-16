using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigmaAssignment.Data.DTO;
using SigmaAssignment.Data.Entities;
using SigmaAssignment.Services.Implementations;
using SigmaAssignment.Services.Interfaces;

namespace SigmaAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _inMemoryCandidateService;
        private readonly IValidator<CandidateDTO> _validator;
        private readonly ILogger<CandidateController> _logger;

        public CandidateController(ICandidateService inMemoryCandidateService, IValidator<CandidateDTO> validator, ILogger<CandidateController> logger)
        {
            this._inMemoryCandidateService = inMemoryCandidateService;
            this._validator = validator;
            this._logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDTO candidate)
        {
            _logger.LogInformation("Starting candidate creation process.");
            var validationResult = _validator.Validate(candidate);

            if (!validationResult.IsValid)
            {
                var response = new BaseResponse<CandidateDTO>
                {
                    Data = null,
                    Success = false,
                    Message = "Validation failed",
                    Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
                return BadRequest(response);
            }

            await _inMemoryCandidateService.AddOrUpdateCandidateAsync(candidate);
            return Ok(new BaseResponse<CandidateDTO>(null, true, "Candidate added successfully."));
        }
    }
}
