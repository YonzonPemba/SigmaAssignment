using Microsoft.EntityFrameworkCore;
using SigmaAssignment.Data.Entities;
using SigmaAssignment.Repositories.Interfaces;

namespace SigmaAssignment.Repositories.Implementations
{
    public class InMemoryCandidateRepository : ICandidateRepository
    {
        private readonly ApplicationContext _context;

        public InMemoryCandidateRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Candidate> GetByEmailAsync(string email)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
        }
    }
}
