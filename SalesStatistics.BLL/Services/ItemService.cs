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
    public class ItemService : Service<ItemDTO>, IItemService
    {
        public ItemService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public override void Add(ItemDTO item)
        {
            Add(item);
        }

        public void TryAdd(ItemDTO item)
        {
            Add(item, true);
        }

        private void Add(ItemDTO item, bool safe = false)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                Expression<Func<ItemDTO, bool>> itemSearchCriteria = x => x.Name == item.Name;

                var managerFromDb = unitOfWork.GetItems(itemSearchCriteria).FirstOrDefault();

                if (managerFromDb == null)
                {
                    lock (_lockers[item.GetType()])
                    {
                        if (unitOfWork.GetItems(itemSearchCriteria).FirstOrDefault() == null)
                        {
                            unitOfWork.AddItem(item);
                        }
                    }
                }
                else if (!safe)
                {
                    throw new ArgumentException("Already exists");
                }
            }
        }

        public override ItemDTO GetById(int id)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetItems(x => x.Id == id).FirstOrDefault();
            }
        }

        public override void Remove(ItemDTO item)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.RemoveItem(item);
            }
        }

        public override void Update(ItemDTO item)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.UpdateItem(item);
            }
        }

        public IEnumerable<ItemDTO> GetItems(ItemsRequest request = null)
        {
            Expression<Func<ItemDTO, bool>> finalExpression;

            if (request == null || request.Name == null)
            {
                finalExpression = null;
            }
            else
            {
                finalExpression = x => x.Name == request.Name;
            }

            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetItems(finalExpression);
            }
        }
    }
}