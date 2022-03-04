using MediatR;
using Microsoft.Extensions.Logging;
using WSA.Microservice.AuthSample.Application.Queries.Todo;
using WSA.Microservice.AuthSample.WebJob.InboundProcess.Interfaces;

namespace WSA.Microservice.AuthSample.WebJob.InboundProcess.Services
{
    public class TodoImporter : ITodoImporter
    {
        private readonly ILogger<TodoImporter> _logger;
        private IMediator _mediator;

        public TodoImporter(ILogger<TodoImporter> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task ProcessAsync()
        {
            _logger.LogInformation("Process started");

            var response = await _mediator.Send(new GetAllTodos.Query());

            return;
        }
    }
}
