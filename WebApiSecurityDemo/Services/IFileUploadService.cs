using WebApiSecurityDemo.Model.Db;

namespace WebApiSecurityDemo.Services
{
    public interface IFileUploadService
    {
        void UploadFile(FileUpload fileUpload);
    }
}