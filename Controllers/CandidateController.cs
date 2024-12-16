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

        public CandidateController(ICandidateService inMemoryCandidateService, IValidator<CandidateDTO> validator) 
        {
            this._inMemoryCandidateService = inMemoryCandidateService;
            this._validator = validator;
        }


        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDTO candidate)
        {
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
