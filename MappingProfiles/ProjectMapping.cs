using AutoMapper;
using Domain;
using Dto.Project;

namespace MappingProfiles
{
    public class ProjectMapping : Profile
    {
        public ProjectMapping()
        {
            base.CreateMap<ProjectDto, Project>().ForMember(p => p.DateCreated, e => e.Ignore()).ReverseMap();
        }
    }
}