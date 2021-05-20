using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessCenterManagement.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterManagement.DataAccess.SignalR
{
    public interface IMessageRepository
    {
        Task Send(string senderId, string receiverId, string message);

        Task<IReadOnlyCollection<Message>> GetLastMessagesWithUser(string senderId, string receiverId, int amount = 10);

        Task<IReadOnlyCollection<string>> GetReceivers(string senderId);
    }

    public class MessageRepository : IMessageRepository
    {
        private readonly MainDbContext _context;

        public MessageRepository(MainDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IReadOnlyCollection<Message>> GetLastMessagesWithUser(string senderId, string receiverId, int amount = 10)
        {
            var messages = (await _context.Set<Message>().AsNoTracking().ToListAsync()).Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                              (m.SenderId == receiverId && m.ReceiverId == senderId)).ToList();

            return messages.OrderByDescending(m => m.Date).Take(amount).ToList().AsReadOnly();
        }

        public async Task<IReadOnlyCollection<string>> GetReceivers(string senderId)
        {
            return (await _context.Set<Message>().AsNoTracking().ToListAsync())
                        .Where(m => m.SenderId == senderId)
                        .Select(m => m.ReceiverId).ToList().AsReadOnly();
        }

        public async Task Send(string senderId, string receiverId, string message)
        {
            _context.Set<Message>().Add(new Message
            {
                Date = DateTimeOffset.Now,
                SenderId = senderId,
                ReceiverId = receiverId,
                Text = message,
            });
            await _context.SaveChangesAsync();
        }
    }
}
