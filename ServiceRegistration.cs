using SigmaAssignment.Mappings;
using SigmaAssignment.Repositories.Implementations;
using SigmaAssignment.Repositories.Interfaces;
using SigmaAssignment.Services.Implementations;
using SigmaAssignment.Services.Interfaces;

namespace SigmaAssignment
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Add scoped services
            services.AddScoped<ICandidateRepository, InMemoryCandidateRepository>();
            services.AddScoped<ICandidateService, InMemoryCandidateService>();

        }
    }
}
