using FirstCourseApp.Entities;

namespace FirstCourseApp.Repositories.Interface;

    public interface IRepository<T>    
    where T : class, IBaseEntity
    {
        void Add(T item) ;
        void Remove(T item);
        void Save();
        T GetById(int id);
        IEnumerable<T> GetAll();
    
    }

