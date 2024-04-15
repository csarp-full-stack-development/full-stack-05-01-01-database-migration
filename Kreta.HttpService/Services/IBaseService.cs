using Kreta.Shared.Responses;

namespace Kreta.HttpService.Services
{
    public interface IBaseService<TEntity>
    {
        public Task<List<TEntity>> SelectAllAsync();
        public Task<ControllerResponse> UpdateAsync(TEntity entity);
        public Task<ControllerResponse> DeleteAsync(Guid id);
        public Task<ControllerResponse> InsertAsync(TEntity entity);
    }
}
