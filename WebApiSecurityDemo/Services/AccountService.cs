using AutoMapper;
using System;
using WebApiSecurityDemo.Model;
using WebApiSecurityDemo.Model.Dtos;
using WebApiSecurityDemo.Utils;

namespace WebApiSecurityDemo.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;

        public AccountService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static User SimulationRepositoryGet(User user)
        {
            return new User
            {
                Email = user.Email,
                Password = user.Password
            };
        }

        public UserDto Login(UserDto user)
        {
            UserDto result = null;

            //throw new ExampleException("Error a proposito.");
            //throw new Exception("Error a proposito.", new Exception("Inner exception"));

            if (user.Email == "me@mail.com" && user.Password == "Crecer2022")
            {
                var data = _mapper.Map<User>(user);
                var userLogged = SimulationRepositoryGet(data);

                result = _mapper.Map<UserDto>(userLogged);
            }

            return result;
        }
    }
}