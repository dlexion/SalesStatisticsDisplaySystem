using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SalesStatistics.BLL.Contracts.Interfaces;
using SalesStatistics.BLL.Contracts.Requests;
using SalesStatistics.DataTransferObjects;
using SalesStatistics.DAL.Contracts.Interfaces;

namespace SalesStatistics.BLL.Services
{
    public class ManagerService : Service<ManagerDTO>, IManagerService
    {
        public ManagerService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public override void Add(ManagerDTO manager)
        {
            Add(manager);
        }

        public void TryAdd(ManagerDTO manager)
        {
            Add(manager, true);
        }

        private void Add(ManagerDTO manager, bool safe = false)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                Expression<Func<ManagerDTO, bool>> managerSearchCriteria = x => x.LastName == manager.LastName;

                var managerFromDb = unitOfWork.GetManagers(managerSearchCriteria).FirstOrDefault();

                if (managerFromDb == null)
                {
                    lock (_lockers[manager.GetType()])
                    {
                        if (unitOfWork.GetManagers(managerSearchCriteria).FirstOrDefault() == null)
                        {
                            unitOfWork.AddManager(manager);
                        }
                    }
                }
                else if(!safe)
                {
                    throw new ArgumentException("Already exists");
                }
            }
        }

        public override ManagerDTO GetById(int id)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetManagers(x => x.Id == id).FirstOrDefault();
            }
        }

        public override void Remove(ManagerDTO manager)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.RemoveManager(manager);
            }
        }

        public override void Update(ManagerDTO manager)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.UpdateManager(manager);
            }
        }

        public IEnumerable<ManagerDTO> GetManagers(ManagersRequest request = null)
        {
            Expression<Func<ManagerDTO, bool>> finalExpression;

            if (request == null || request.LastName == null)
            {
                finalExpression = null;
            }
            else
            {
                finalExpression = x => x.LastName == request.LastName;
            }

            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetManagers(finalExpression);
            }
        }
    }
}