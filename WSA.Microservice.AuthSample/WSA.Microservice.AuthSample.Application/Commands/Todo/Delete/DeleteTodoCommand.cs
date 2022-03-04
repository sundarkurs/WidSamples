using MediatR;
using WSA.Microservice.AuthSample.Application.Common.Wrappers;

namespace WSA.Microservice.AuthSample.Application.Commands.Todo.Delete
{
    public class DeleteTodoCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}
