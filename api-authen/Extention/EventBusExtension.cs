using EventBus.Base.Standard;
using EventBusiness.IntegrationEvents.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Authen.Api.Extention
{
    public static class EventBusExtension
    {
        public static IServiceCollection AddEventBusiness(this IServiceCollection serviceDescriptors)
        {
          //  serviceDescriptors.AddTransient(typeof(RequestNotificationEventHandler));
            return serviceDescriptors;
        }
        public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
           // eventBus.Subscribe<RequestNotificationEvent,RequestNotificationEventHandler>();
            return app;
        }
    }
}
