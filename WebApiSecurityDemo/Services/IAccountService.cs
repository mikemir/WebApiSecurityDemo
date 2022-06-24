using System.Collections.Generic;
using WebApiSecurityDemo.Model;
using WebApiSecurityDemo.Model.Dtos;

namespace WebApiSecurityDemo.Services
{
    public interface IAccountService
    {
        UserDto Login(UserDto user);

        List<DemoUser> GetUsers();

        DemoUser GetById(int id);

        DemoUser GetByIdV2(string id);
    }
}