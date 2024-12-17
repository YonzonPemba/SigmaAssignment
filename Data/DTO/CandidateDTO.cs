using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SigmaAssignment.Data.DTO
{
    public class CandidateDTO
    {
        /// <summary>Gets or sets the Email.</summary>
        /// <value>Email of Candidate.</value>
        public string Email { get; set; }

        /// <summary>Gets or sets the FirstName.</summary>
        /// <value>FirstName of Candidate.</value>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the LastName.</summary>
        /// <value>LastName of Candidate.</value>
        public string LastName { get; set; }

        /// <summary>Gets or sets the PhoneNumber.</summary>
        /// <value>PhoneNumber of Candidate.</value>
        public string PhoneNumber { get; set; }

        /// <summary>Gets or sets the StartTime.</summary>
        /// <value>Candidate's Preferred StartTime for Interview.</value>
        public string StartTime { get; set; }

        /// <summary>Gets or sets the EndTime.</summary>
        /// <value>Candidate's Preferred EndTime for Interview.</value>
        public string EndTime { get; set; }

        /// <summary>Gets or sets the LinkedInProfileUrl.</summary>
        /// <value>LinkedInProfileUrl of Candidate.</value>
        public string LinkedInProfileUrl { get; set; }

        /// <summary>Gets or sets the GitHubProfileUrl.</summary>
        /// <value>GitHubProfileUrl of Candidate.</value>
        public string GitHubProfileUrl { get; set; }

        /// <summary>Gets or sets the FreeTextComment.</summary>
        /// <value>Comment on Candidate.</value>
        public string FreeTextComment { get; set; }

        /// <summary>Gets or sets the IsUpdated.</summary>
        /// <value>Denotes is Candidate Updated.</value>
        [JsonIgnore]
        public bool? IsUpdated { get; set; }
    }
}
