using System.ComponentModel.DataAnnotations;

namespace SigmaAssignment.Data.DTO
{
    public class CandidateDTO
    {
        public string Email { get; set; }  

        public string FirstName { get; set; }  

        public string LastName { get; set; }  

        public string PhoneNumber { get; set; }  

        public string StartTime { get; set; } 
        public string EndTime { get; set; }  

        public string LinkedInProfileUrl { get; set; }  

        public string GitHubProfileUrl { get; set; }  

        public string FreeTextComment { get; set; }
    }
}
