using Microsoft.AspNetCore.SignalR;

namespace FitnessCenterManagement.Api.SignalR
{
    public class MessagingUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection?.User?.FindFirst("userId").Value;
        }
    }
}
