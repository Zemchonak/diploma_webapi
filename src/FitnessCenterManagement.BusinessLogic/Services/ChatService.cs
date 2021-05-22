using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FitnessCenterManagement.BusinessLogic.Dtos;
using FitnessCenterManagement.BusinessLogic.Exceptions;
using FitnessCenterManagement.DataAccess.SignalR;

namespace FitnessCenterManagement.BusinessLogic.Services
{
    public interface IChatService
    {
        Task Send(string senderId, string receiverId, string message);

        Task<IReadOnlyCollection<MessageDto>> GetLastMessagesWithUser(string senderId, string receiverId, int amount = 10);

        Task<IReadOnlyCollection<string>> GetReceivers(string senderId);
    }

    public class ChatService : IChatService
    {
        private readonly IMessageRepository _messageRepository;

        private readonly IMapper _mapper;

        public ChatService(IMessageRepository repository, IMapper mapper)
        {
            _messageRepository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<MessageDto>> GetLastMessagesWithUser(string senderId, string receiverId, int amount = 10)
        {
            return _mapper.Map<IReadOnlyCollection<MessageDto>>(await _messageRepository.GetLastMessagesWithUser(senderId, receiverId, amount));
        }

        public async Task<IReadOnlyCollection<string>> GetReceivers(string senderId)
        {
            return await _messageRepository.GetReceivers(senderId);
        }

        public async Task Send(string senderId, string receiverId, string message)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
            {
                throw new BusinessLogicException();
            }

            await _messageRepository.Send(senderId, receiverId, message);
        }
    }
}
