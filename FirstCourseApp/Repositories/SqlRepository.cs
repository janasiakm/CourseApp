using Microsoft.EntityFrameworkCore;
using FirstCourseApp.Entities;
using FirstCourseApp.Repositories.Interface; 

namespace FirstCourseApp.Repositories;

public class SqlRepository<T> : IRepository<T>  
    where T : class, IBaseEntity, new()
    
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _dbContext;


    public SqlRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public T? GetById(int id)
    {
        
        return _dbSet.FirstOrDefault(item => item.Id == id);
        
    }

    public int GetCountClient()
    {
        int result = _dbSet.Max(item => item.Id);
        //int count = 
        //int lastid=0;

        //for (int i = 0; i <= count;i++)
        //{
        //    var item = _dbSet.Find(i);
        //    if (item != null)
        //        lastid= item.Id;
        //}

        return result;

    }
    public void Add(T item)
    { 
        _dbSet.Add(item);
    }

    public void Remove(T item)
    { 
        _dbSet.Remove(item);    
    }

    public void Save()
    { 
       _dbContext.SaveChanges();
    }
    
    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }
    

}

