using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniversityRestApi.Dto;

namespace UniversityRestApi.Data;


public class PaginatedData<T> {
    public int CurrentIndex { get; set; } = 1;

    public int TotalItems { get; set; }

    public List<T> Items { get; set; } = new List<T>();
}


public interface IRepository<T, R>
{
    Task Create(T entity);

    Task Delete(T entity);

    Task<PaginatedData<T>> GetAll(PaginationFilter dto, Expression<Func<T, bool>>? filter);

    Task<T> GetByID(R id);
}

public class Repository<T, R> : IRepository<T, R> where T : class
{
    private readonly UniversityContext database;

    public Repository(UniversityContext database) {
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

    public async Task<PaginatedData<T>> GetAll(PaginationFilter dto, Expression<Func<T, bool>>? filter = null)
    {
       var count = await database.Set<T>().CountAsync();

        var query = database.Set<T>().AsQueryable();

        if(filter != null)
        {
            query = query.Where(filter);
        }

        var data = await query
            .Skip(dto.CurrentIndex - 1 * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync();


        return new PaginatedData<T>()
        {
            CurrentIndex = dto.CurrentIndex,
            Items = data,
            TotalItems = count
        };
    }

    public async Task<T> GetByID(R id)
    {
        return await database.Set<T>().Where((e) => e.Equals(id)).FirstAsync();
    }
}
