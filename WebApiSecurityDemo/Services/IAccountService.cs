using WebApiSecurityDemo.Model;
using WebApiSecurityDemo.Model.Dtos;

namespace WebApiSecurityDemo.Services
{
    public interface IAccountService
    {
        UserDto Login(UserDto user);
    }
}