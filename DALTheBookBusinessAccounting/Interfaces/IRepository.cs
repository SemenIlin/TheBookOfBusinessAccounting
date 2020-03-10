
namespace DALTheBookBusinessAccounting.Interfaces
{
    public interface IRepository<T> : IReadRepository<T> where T : class 
    {        
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
