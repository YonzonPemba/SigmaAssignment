using Microsoft.EntityFrameworkCore;
using SigmaAssignment.Data.Entities;

namespace SigmaAssignment.Repositories.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetByEmailAsync(string email);
        Task AddAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);
    }
}
