using System.ComponentModel.DataAnnotations;

namespace WebApiSecurityDemo.Model.Dtos
{
    public class UserDto
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}