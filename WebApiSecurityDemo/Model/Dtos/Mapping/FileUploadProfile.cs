using AutoMapper;
using System;
using WebApiSecurityDemo.Model.Db;

namespace WebApiSecurityDemo.Model.Dtos.Mapping
{
    public class FileUploadProfile : Profile
    {
        public FileUploadProfile()
        {
            CreateMap<FileUploadDto, FileUpload>()
                .ForMember(dest => dest.Content,
                            opt => opt.MapFrom(src => Convert.FromBase64String(src.Content)))
                .ReverseMap();
        }
    }
}