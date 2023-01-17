using FirstCourseApp.Entities;
using FirstCourseApp.Repositories.Interface;

namespace FirstCourseApp.Repositories
{
    public class ListRepository<T> :IRepository<T>
        where T : BaseEntity
    {
        protected readonly List<T> _items = new();
        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
        }

        public void Remove(T item)
        {
          
            _items.Remove(item);
        }

        public void Save()
        {
           
        }

        //public List<TEntity> GetAll()
        //{
        //    //int x = _items.Count();
        //    //private int i;
        //    //private string result;
        //    //for ( i = 0, i <= x, i++)
        //    //{ 
        //    return  _items.ToList();
        //    //Console.WrieLine(result);
        //    //}
        //}
         

        public T GetById(int id)
        {
            return _items.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        

    }
}
