using AutoMapper;
using FightCoachAI.Application.DTOs.Auth;
using FightCoachAI.Domain.Entities;

namespace FightCoachAI.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}
