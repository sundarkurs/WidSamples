using Microsoft.EntityFrameworkCore;
using WSA.Microservice.AuthSample.Application.Common.Interfaces.Repositories;
using WSA.Microservice.AuthSample.Domain.Entities;
using WSA.Microservice.AuthSample.Infrastructure.Persistence.Contexts;

namespace WSA.Microservice.AuthSample.Infrastructure.Persistence.Repositories
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        private readonly DbSet<Todo> _todos;

        public TodoRepository(TemplateContext dbContext) : base(dbContext)
        {
            _todos = dbContext.Set<Todo>();
        }

        public Task<bool> IsTitleUniqueAsync(string title)
        {
            return _todos.AllAsync(p => p.Title != title);
        }
    }
}
