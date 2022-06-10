using AutoMapper;
using System.Linq;
using EventBusiness.IntegrationEvents.Events;
using Trading.Authen.Services.Dto.Users;
using Trading.Authen.Repository.Entity;

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