using AutoMapper;
using System;

namespace WebApiSecurityDemo.Model.Dtos.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}