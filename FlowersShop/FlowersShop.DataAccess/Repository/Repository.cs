using Microsoft.EntityFrameworkCore;
using FlowersShop.DataAccess.Entities; 

namespace FlowersShop.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IDbContextFactory<FlowersShopDbContext> _contextFactory;
    public Repository(IDbContextFactory<FlowersShopDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public IQueryable<T> GetAll()
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>();
    }

    public T? GetById(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

    public T Save(T entity)
    {
        if (entity.CreationTime == entity.ModificationTime)
        {
            using var context = _contextFactory.CreateDbContext();
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = DateTime.UtcNow;
            var result = context.Set<T>().Add(entity);
            context.SaveChanges();
            return result.Entity;
        }
        else
        {
            using var context = _contextFactory.CreateDbContext();
            entity.ModificationTime = DateTime.UtcNow;
            var result = context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return result.Entity;
        }
    }

    public void Delete(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Deleted;
        context.SaveChanges();
    }
}