using System.ComponentModel.DataAnnotations;

namespace SigmaAssignment.Data.Entities
{
    public class Candidate
    {
        [Key]
        [Required]
        public string Email { get; set; }  // Email is the primary key in the database

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string LinkedInProfileUrl { get; set; }

        public string GitHubProfileUrl { get; set; }

        [Required]
        public string FreeTextComment { get; set; }
    }

}
