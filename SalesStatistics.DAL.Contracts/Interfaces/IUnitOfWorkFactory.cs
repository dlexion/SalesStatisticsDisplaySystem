namespace SalesStatistics.DAL.Contracts.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetInstance();
    }
}