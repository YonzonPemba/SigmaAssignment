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
        [Route("add-update")]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDTO candidate)
        {
            try
            {
                _logger.LogInformation("Starting candidate creation/updation process.");
                var validationResult = _validator.Validate(candidate);

                if (!validationResult.IsValid)
                {
                    _logger.LogInformation("Invalid model for candidate creation/updation process.");
                    var response = new BaseResponse<CandidateDTO>
                    {
                        Data = null,
                        Success = false,
                        Message = "Validation failed",
                        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                    };
                    return BadRequest(response);
                }

                var result = await _inMemoryCandidateService.AddOrUpdateCandidateAsync(candidate);
                _logger.LogInformation("Completed candidate creation/updation process.");
                return Ok(new BaseResponse<CandidateDTO>(result, true, result?.IsUpdated ?? false ? "Candidate updated successfully." : "Candidate added successfully."));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the candidate creation or update.");

                var errorResponse = new BaseResponse<CandidateDTO>
                {
                    Data = null,
                    Success = false,
                    Message = "Internal Server Error",
                    Errors = new List<string> { "An unexpected error occurred.Please try again later." } 
                };

                return StatusCode(500, errorResponse);
            }
        }
    }
}
