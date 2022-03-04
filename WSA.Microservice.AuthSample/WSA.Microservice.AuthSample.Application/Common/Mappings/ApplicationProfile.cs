using AutoMapper;
using WSA.Microservice.AuthSample.Application.Common.DTO;
using WSA.Microservice.AuthSample.Domain.Entities;

namespace WSA.Microservice.AuthSample.Application.Common.Mappings
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Todo, TodoDto>().ReverseMap();
            CreateMap<TodoRequest, Todo>();
        }
    }
}
