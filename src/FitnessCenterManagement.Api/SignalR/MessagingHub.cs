using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FitnessCenterManagement.Api.SignalR
{
    public class MessagingHub : Hub
    {
        public Task SendMessage(string message, string userId)
        {
            return Clients.User(userId).SendAsync("Receive", message + userId);
        }
    }
}
