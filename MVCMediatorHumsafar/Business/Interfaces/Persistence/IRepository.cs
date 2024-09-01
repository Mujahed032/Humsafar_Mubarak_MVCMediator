using CommonX;
using Entity.Concrete.Models.Candidates;
using System.Linq.Expressions;

namespace Application.Interfaces.Persistence
{
    public interface IRepository<T>
    {
        Task<IResponseDataModel<IEnumerable<T>>> GetAllAsync();
        IEnumerable<T> GetAll();
        Task<T> GetAsync(Guid Id);
        Task<IResponseDataModel<T>> GetAsync(Expression<Func<T, bool>> filter);
        Task<IResponseDataModel<T>> AddAsync(T entity);

        Task<IResponseModel> RemoveAsync(T entity);
        Task<IResponseModel> RemoveAsync(Guid Id);
        Task<IResponseDataModel<IEnumerable<T>>> GetAsyncWithSpecifiedFilter(Expression<Func<T, bool>> filter);
        Task<IResponseModel> UpdateAsync(Guid Id, T entity);
    }
}
