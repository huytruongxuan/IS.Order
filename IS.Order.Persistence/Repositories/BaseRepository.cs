using IS.Order.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IS.Order.Persistence.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    protected readonly ApplicationDbContext DbContext;

    protected BaseRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<T> GetByIdAsync(Guid id)
    {
        var t = await DbContext.Set<T>().FindAsync(id);
        return t;
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await DbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size)
    {
        return await DbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
    }
}