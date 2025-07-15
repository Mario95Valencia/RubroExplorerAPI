using System.Linq.Expressions;

namespace MicroservicesTemplate.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(long id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync<TKey>(TKey id, TEntity entity);
        Task DeleteAsync(long id);
        Task<IEnumerable<TEntity>> GetByStatusAsync(bool status);
        Task SoftDeleteAsync(long id);

        //Queryable para aplicar filtros personalizados
        IQueryable<TEntity> AsQueryable();

        // Método para buscar por expresión
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<List<TEntity>> GetListByConditionAsync(Expression<Func<TEntity, bool>> predicate);


    }
}
