using Microsoft.EntityFrameworkCore;
using UniversityDto;

namespace UniversityRestApi.Data;


public class PaginatedData<T>
{
    public int CurrentIndex { get; set; } = 1;

    public int TotalItems { get; set; }
    
    public List<T> Items { get; set; } = new List<T>();
}


public interface IRepository<T>
{
    Task Create(T entity);

    Task Delete(T entity);

    Task<PaginatedData<T>> GetAll(PaginationFilter dto, Func<IQueryable<T>, IQueryable<T>>? exp = null);

    Task<T?> FilterForFirst(Func<IQueryable<T>, IQueryable<T>> exp);
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly UniversityContext database;

    public Repository(UniversityContext database)
    {
        this.database = database;
    }

    public async Task Create(T entity)
    {
        database.Set<T>().Add(entity);
        await database.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        database.Set<T>().Remove(entity);
        await database.SaveChangesAsync();
    }

    public async Task<PaginatedData<T>> GetAll(PaginationFilter filter,
        Func<IQueryable<T>, IQueryable<T>>? exp = null)
    {
        var count = await database.Set<T>().CountAsync();
        var query = database.Set<T>().AsQueryable();
        query = exp?.Invoke(query) ?? query;

        var data = await query
            .Skip((filter.CurrentIndex - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PaginatedData<T>()
        {
            CurrentIndex = filter.CurrentIndex,
            Items = data,
            TotalItems = count
        };
    }

    public async Task<T?> FilterForFirst(Func<IQueryable<T>, IQueryable<T>> exp)
    {
        var query = database.Set<T>().AsQueryable();
        query = exp.Invoke(query);

        return await query
            .FirstOrDefaultAsync();
    }

    public async Task Save()
    {
        await database.SaveChangesAsync();
    }
}
