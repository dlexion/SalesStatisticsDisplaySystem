namespace SalesStatistics.BLL.Contracts.Interfaces
{
    public interface IService<T> where T : class
    {
        void Add(T model);

        T GetById(int id);

        void Remove(T model);

        void Update(T model);
    }
}