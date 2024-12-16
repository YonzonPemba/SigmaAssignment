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

        public CandidateController(ICandidateService inMemoryCandidateService) {
            this._inMemoryCandidateService = inMemoryCandidateService;
        }


        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDTO candidate)
        {
            await _inMemoryCandidateService.AddOrUpdateCandidateAsync(candidate);
            return Ok(new BaseResponse<CandidateDTO>(null, 200, "Candidate added successfully."));
        }
    }
}
