using Microsoft.EntityFrameworkCore;
using FlowersShop.DataAccess.Entities; 
namespace FlowersShop.DataAccess.Repository;

public class Repository<T> where T : BaseEntity
{
    private DbContext _context;
    public Repository(DbContext context)
    {
        _context = context;
    }
    
    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public T? GetById(int id)
    {
        return _context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

    public T Save(T entity)
    {
        if (entity.CreationTime == entity.ModificationTime)
        {
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = DateTime.UtcNow;
            var result = _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }
        else
        {
            entity.ModificationTime = DateTime.UtcNow;
            var result = _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return result.Entity;
        }
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Deleted;
        _context.SaveChanges();
    }
}