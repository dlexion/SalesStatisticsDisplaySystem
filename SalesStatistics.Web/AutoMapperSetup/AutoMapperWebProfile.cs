using AutoMapper;
using SalesStatistics.DataTransferObjects;
using SalesStatistics.Web.Models.ViewModels;

namespace SalesStatistics.Web.AutoMapperSetup
{
    public class AutoMapperWebProfile : Profile
    {
        public AutoMapperWebProfile()
        {
            CreateMap<ManagerViewModel, ManagerDTO>();
            CreateMap<ManagerDTO, ManagerViewModel>();

            CreateMap<ItemViewModel, ItemDTO>();
            CreateMap<ItemDTO, ItemViewModel>();

            CreateMap<CustomerDTO, CustomerViewModel>();
            CreateMap<CustomerViewModel, CustomerDTO>();
        }
    }
}