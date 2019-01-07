using SalesStatistics.DAL.Contracts.Interfaces;

namespace SalesStatistics.DAL.Models
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork GetInstance()
        {
            return new UnitOfWork();
        }
    }
}