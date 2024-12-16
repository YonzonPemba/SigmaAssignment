using Microsoft.EntityFrameworkCore;
using SigmaAssignment.Data.Entities;

namespace SigmaAssignment
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }

        }
}
