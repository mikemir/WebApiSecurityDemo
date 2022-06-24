using FluentValidation;
using System.Linq;
using WebApiSecurityDemo.Model.Dtos;

namespace WebApiSecurityDemo.Model.Validations
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        private string[] BLACK_LIST = new string[] { " OR ", " AND ", "--", "'" };

        public UserValidator()
        {
            //https://docs.fluentvalidation.net/en/latest/index.html
            //LISTA NEGRAS
            RuleFor(m => m.Email).EmailAddress().NotEmpty();
            RuleFor(m => m.Password).Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$");
            //SELECT * FROM USERS WHERE email = '' OR 1=1 -- AND PASSS = ''
        }
    }
}