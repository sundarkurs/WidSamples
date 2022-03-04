using WSA.Microservice.AuthSample.Domain.Entities;

namespace WSA.Microservice.AuthSample.Application.Common.Interfaces.Repositories
{
    public interface ITodoRepository : IBaseRepository<Todo>
    {
        Task<bool> IsTitleUniqueAsync(string code);
    }
}
