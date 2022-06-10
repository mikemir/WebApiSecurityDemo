using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiSecurityDemo.Model.Db;
using WebApiSecurityDemo.Model.Dtos;
using WebApiSecurityDemo.Services;

namespace WebApiSecurityDemo.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;

        public FileUploadController(IMapper mapper, IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(FileUploadDto fileUpload)
        {
            var file = _mapper.Map<FileUpload>(fileUpload);

            _fileUploadService.UploadFile(file);

            return Ok();
        }
    }
}