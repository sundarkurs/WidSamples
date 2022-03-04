using MediatR;
using WSA.Microservice.AuthSample.Application.Common.DTO;
using WSA.Microservice.AuthSample.Application.Common.Wrappers;

namespace WSA.Microservice.AuthSample.Application.Commands.Todo.Update
{
    public class UpdateTodoCommand : IRequest<Response<TodoDto>>
    {
        public int Id { get; set; }
        public TodoRequest Todo { get; set; }
    }
}
