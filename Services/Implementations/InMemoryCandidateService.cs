using AutoMapper;
using SigmaAssignment.Data.DTO;
using SigmaAssignment.Data.Entities;
using SigmaAssignment.Repositories.Interfaces;
using SigmaAssignment.Services.Interfaces;

namespace SigmaAssignment.Services.Implementations
{
    public class InMemoryCandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public InMemoryCandidateService(ICandidateRepository candidateRepository, IMapper mapper)
        {
            this._candidateRepository = candidateRepository;
            this._mapper = mapper;
        }

        public async Task AddOrUpdateCandidateAsync(CandidateDTO candidate)
        {
            // Check if the candidate exists by email (unique identifier)
            var existingCandidate = await _candidateRepository.GetByEmailAsync(candidate.Email);

            if (existingCandidate == null)
            {
                var newCandidate = _mapper.Map<CandidateDTO, Candidate>(candidate);
                // Add a new candidate
                await _candidateRepository.AddAsync(newCandidate);
            }
        }
    }
}
