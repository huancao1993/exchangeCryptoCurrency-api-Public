using Trading.Authen.Api.Interfaces;
using EventBus.Base.Standard;
using EventBusiness.IntegrationEvents.Events;
using EventBusiness.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Authen.Api.IntegrationEvents.Handlers
{
        public class RequestNotificationEventHandler : IIntegrationEventHandler<RequestNotificationEvent>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly INotificationEventService _notificationEventService;
        private ILogger<RequestNotificationEventHandler> _logger;
        public RequestNotificationEventHandler(
            IMapper mapper, 
            IUserService userService, 
            INotificationEventService notificationEventService, 
            ILogger<RequestNotificationEventHandler> logger)
        {
            _mapper = mapper;
            _userService = userService;
            _notificationEventService = notificationEventService;
            _logger = logger;
        }
        public async Task Handle(RequestNotificationEvent @event)
         {
            var data = _mapper.Map<ConfirmNotificationEvent>(@event);
            var userNames =  await _userService.GetUserNamesByRoleAction(@event.Role);
            if (userNames==null)
            {
                return;
            }
            data.UserNames = userNames;
            try
            {
                await _notificationEventService.Publish(data);
            }
            catch(Exception ex)
            {
                _logger.LogError("RequestNotificationEvent error", ex);
            }
           
        }
        }
    
}
