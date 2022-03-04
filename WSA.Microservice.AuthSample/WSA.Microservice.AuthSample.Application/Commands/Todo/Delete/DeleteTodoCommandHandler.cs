﻿using MediatR;
using WSA.Microservice.AuthSample.Application.Common.Exceptions;
using WSA.Microservice.AuthSample.Application.Common.Interfaces.Repositories;
using WSA.Microservice.AuthSample.Application.Common.Wrappers;

namespace WSA.Microservice.AuthSample.Application.Commands.Todo.Delete
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, Response<bool>>
    {
        private readonly ITodoRepository _todoRepository;

        public DeleteTodoCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<Response<bool>> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.Id);

            if (todo == null)
            {
                throw new ApiException($"Todo not found.");
            }

            await _todoRepository.DeleteAsync(todo);

            return new Response<bool>(true);
        }
    }
}
