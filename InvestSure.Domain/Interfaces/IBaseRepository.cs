
namespace InvestSure.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(T entity);
        Task Update(T entity);

    }
}
