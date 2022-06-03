using AutoMapper;
using System.Linq;
using EventBusiness.IntegrationEvents.Events;
using Trading.Services.Dto.Users;
using Trading.Repository.Entity;

namespace Trading.Authen.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RequestNotificationEvent,ConfirmNotificationEvent>().ReverseMap();
            CreateMap<RegisterModel,Users>().ReverseMap();
            
        }
    }
}