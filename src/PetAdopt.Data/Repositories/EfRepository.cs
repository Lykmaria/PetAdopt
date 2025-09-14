using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Application.Repositories;

namespace PetAdopt.Data.Repositories;

public class EfRepository<T>(PetAdoptDbContext db) : IGenericRepository<T> where T : class
{
    public Task<T?> GetByIdAsync(int id) => db.Set<T>().FindAsync(id).AsTask();

    public Task<List<T>> ListAsync(Expression<Func<T, bool>>? predicate = null) =>
        (predicate is null ? db.Set<T>() : db.Set<T>().Where(predicate)).ToListAsync();

    public async Task AddAsync(T entity) { await db.Set<T>().AddAsync(entity); }
    public Task UpdateAsync(T entity) { db.Set<T>().Update(entity); return Task.CompletedTask; }
    public Task DeleteAsync(T entity) { db.Set<T>().Remove(entity); return Task.CompletedTask; }

    public Task<int> SaveChangesAsync() => db.SaveChangesAsync();
}
