using Trecom.Api.Identity.Application.Models.Dtos;
using Trecom.Api.Identity.Application.Models.Entities;

namespace Trecom.Api.Identity.Application.Features.Profile
{
    public class MappingProfiles:AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserQueryDto>().ReverseMap();
        }
    }
}
