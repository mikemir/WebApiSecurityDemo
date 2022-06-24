using System.ComponentModel.DataAnnotations;

namespace WebApiSecurityDemo.Model.Dtos
{
    public class FileUploadDto
    {
        //FileExtensions(Extensions = "jpg,jpeg,gif,png")
        [Required, FileExtensions(Extensions = "zip")]
        public string Name { get; set; }

        [Required, RegularExpression(@"^(?:[A-Za-z0-9+\/]{4})*(?:[A-Za-z0-9+\/]{2}==|[A-Za-z0-9+\/]{3}=|[A-Za-z0-9+\/]{4})$")]
        public string Content { get; set; }
    }
}