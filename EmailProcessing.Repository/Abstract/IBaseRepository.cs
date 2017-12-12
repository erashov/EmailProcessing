using System.Linq;

namespace EmailProcessing.Repository.Abstract
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> Find();
        IQueryable<T> Find(string filter);

        IQueryable<T> FindPage(int page, int pageSize, string sort, string order, string filter);

        T FindById(int id);

        T Add(T entity);

        T Update(T entity);

        T Remove(T entity);
    }
}
