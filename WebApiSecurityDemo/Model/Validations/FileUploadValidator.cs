using FluentValidation;
using WebApiSecurityDemo.Model.Dtos;

namespace WebApiSecurityDemo.Model.Validations
{
    public class FileUploadValidator : AbstractValidator<FileUploadDto>
    {
        public FileUploadValidator()
        {
            var base64Pattern = @"^(?:[A-Za-z0-9+\/]{4})*(?:[A-Za-z0-9+\/]{2}==|[A-Za-z0-9+\/]{3}=|[A-Za-z0-9+\/]{4})$";

            RuleFor(prop => prop.Name).NotEmpty();
            RuleFor(m => m.Content).Matches(base64Pattern).NotEmpty();
        }
    }
}