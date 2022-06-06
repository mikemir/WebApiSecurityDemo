using FluentValidation;
using WebApiSecurityDemo.Model.Dtos;

namespace WebApiSecurityDemo.Model.Validations
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            //https://docs.fluentvalidation.net/en/latest/index.html

            RuleFor(m => m.Email).EmailAddress();
            RuleFor(m => m.Password).Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$");
        }
    }
}