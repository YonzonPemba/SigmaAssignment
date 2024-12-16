using AutoMapper;
using SigmaAssignment.Data.DTO;
using SigmaAssignment.Data.Entities;

namespace SigmaAssignment.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CandidateDTO, Candidate>().ReverseMap(); ;
        }
    }
}
