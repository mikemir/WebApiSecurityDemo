using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiSecurityDemo.Model;
using WebApiSecurityDemo.Model.Dtos;
using WebApiSecurityDemo.Utils;

namespace WebApiSecurityDemo.Services
{
    public class DemoUser
    {
        public int BadId { get; set; }
        public string GoodId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
    }

    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;

        private List<DemoUser> Users = new List<DemoUser>
            {
                new DemoUser{
                    BadId = 1,
                    GoodId = Guid.NewGuid().ToString("N"),
                    Nombres = "Sebastian",
                    Apellidos = "Romero"
                },
                new DemoUser{
                    BadId = 2,
                    GoodId = Guid.NewGuid().ToString("N"),
                    Nombres = "Orlando",
                    Apellidos = "Peña"
                },
                new DemoUser{
                    BadId = 3,
                    GoodId = Guid.NewGuid().ToString("N"),
                    Nombres = "Rocio",
                    Apellidos = "Escobar"
                },
            };

        public AccountService(IMapper mapper)
        {
            _mapper = mapper;
        }

        private static User SimulationRepositoryGet(User user)
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

        public List<DemoUser> GetUsers()
        {
            return Users;
        }

        public DemoUser GetById(int id)
        {
            return Users.SingleOrDefault(item => item.BadId == id);
        }

        public DemoUser GetByIdV2(string id)
        {
            return Users.SingleOrDefault(item => item.GoodId == id); ;
        }
    }
}