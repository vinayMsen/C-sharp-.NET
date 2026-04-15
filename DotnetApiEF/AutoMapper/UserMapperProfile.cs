using AutoMapper;
using DotnetApiEF.Models;
using DotnetApiEF.Dto;

namespace DotnetApiEF.AutoMapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDto, User>();
        }
    }
}