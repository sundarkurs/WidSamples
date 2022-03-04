using MediatR;
using WSA.Microservice.AuthSample.Application.Common.DTO;
using WSA.Microservice.AuthSample.Application.Common.Wrappers;

namespace WSA.Microservice.AuthSample.Application.Commands.Todo.Create
{
    public class CreateTodoCommand : IRequest<Response<TodoDto>>
    {
        public TodoRequest Todo { get; set; }
    }
}
