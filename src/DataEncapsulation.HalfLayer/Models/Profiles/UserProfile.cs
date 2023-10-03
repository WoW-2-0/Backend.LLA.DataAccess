using AutoMapper;
using DataEncapsulation.HalfLayer.Models.Dtos;
using DataEncapsulation.HalfLayer.Models.Entities;

namespace DataEncapsulation.HalfLayer.Models.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();
    }
}