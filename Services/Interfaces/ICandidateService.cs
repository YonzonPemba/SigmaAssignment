using SigmaAssignment.Data.DTO;

namespace SigmaAssignment.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<CandidateDTO> AddOrUpdateCandidateAsync(CandidateDTO candidate);
    }
}
