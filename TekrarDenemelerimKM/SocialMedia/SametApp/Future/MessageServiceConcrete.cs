using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Core.Entities;
using SocialMediaApp.Data.UnitOfWork;
using SocialMediaApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaApp.Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<MessageDto>>> GetConversationAsync(string senderId, string receiverId)
        {
            var messages = await _unitOfWork.Messages.Where(m =>
                    (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                    (m.SenderId == receiverId && m.ReceiverId == senderId))
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();

            var messagesDto = _mapper.Map<List<MessageDto>>(messages);

            return new ResponseDto<List<MessageDto>> { Data = messagesDto };
        }

        public async Task<ResponseDto<List<UserDto>>> GetConversationsAsync(string userId)
        {
            // Kullanıcının mesajlaştığı kişilerin ID'lerini al
            var userIds = await _unitOfWork.Messages.Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .Select(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .Distinct()
                .ToListAsync();

            // Her bir kullanıcı için son mesajı al
            var users = new List<User>();

            foreach (var id in userIds)
            {
                var user = await _unitOfWork.Messages.Where(m =>
                        (m.SenderId == userId && m.ReceiverId == id) ||
                        (m.SenderId == id && m.ReceiverId == userId))
                    .OrderByDescending(m => m.CreatedAt)
                    ////////////////////////////////////////////////////////
                ///

                // burayı ayırıp baştan koyma sebebim yukarı çıktıyı bir yerden aldım ama bundan sonrasını mesaj uzunluğua ulaştığı için olabilidiğince anlatarak sonradan tamamlattım doğru olmayuabilir düzenleme gerekebilir o yüzden.
                    using AutoMapper;
                using Microsoft.EntityFrameworkCore;
                using SocialMediaApp.Core.Entities;
                using SocialMediaApp.Data.UnitOfWork;
                using SocialMediaApp.Shared.DTOs;
                using System;
                using System.Collections.Generic;
                using System.Linq;
                using System.Threading.Tasks;

namespace SocialMediaApp.Service.Services
    {
        public class MessageService : IMessageService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ResponseDto<List<MessageDto>>> GetConversationAsync(string senderId, string receiverId)
            {
                var messages = await _unitOfWork.Messages.Where(m =>
                        (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                        (m.SenderId == receiverId && m.ReceiverId == senderId))
                    .Include(m => m.Sender)
                    .Include(m => m.Receiver)
                    .OrderBy(m => m.CreatedAt)
                    .ToListAsync();

                var messagesDto = _mapper.Map<List<MessageDto>>(messages);

                return new ResponseDto<List<MessageDto>> { Data = messagesDto };
            }

            public async Task<ResponseDto<List<UserDto>>> GetConversationsAsync(string userId)
            {
                // Kullanıcının mesajlaştığı kişilerin ID'lerini al
                var userIds = await _unitOfWork.Messages.Where(m => m.SenderId == userId || m.ReceiverId == userId)
                    .Select(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                    .Distinct()
                    .ToListAsync();

                // Her bir kullanıcı için son mesajı al
                var users = new List<User>();

                foreach (var id in userIds)
                {
                    var user = await _unitOfWork.Users
                        .Include(u => u.ProfilePicture)
                        .FirstOrDefaultAsync(u => u.Id == id);

                    if (user != null)
                    {
                        // Son mesajı al
                        var lastMessage = await _unitOfWork.Messages
                            .Where(m => (m.SenderId == userId && m.ReceiverId == id) ||
                                        (m.SenderId == id && m.ReceiverId == userId))
                            .OrderByDescending(m => m.CreatedAt)
                            .FirstOrDefaultAsync();

                        if (lastMessage != null)
                        {
                            user.LastMessage = lastMessage.Content;
                            user.LastMessageDate = lastMessage.CreatedAt;
                        }

                        // Okunmamış mesaj sayısını hesapla
                        var unreadCount = await _unitOfWork.Messages
                            .CountAsync(m => m.SenderId == id && m.ReceiverId == userId && !m.IsRead);

                        user.UnreadMessageCount = unreadCount;
                        users.Add(user);
                    }
                }

                // Son mesaj tarihine göre sırala
                users = users.OrderByDescending(u => u.LastMessageDate ?? DateTime.MinValue).ToList();

                var usersDto = _mapper.Map<List<UserDto>>(users);

                return new ResponseDto<List<UserDto>> { Data = usersDto };
            }

            public async Task<ResponseDto<MessageDto>> SendMessageAsync(string senderId, string receiverId, string content)
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    return new ResponseDto<MessageDto>
                    {
                        Success = false,
                        Message = "Mesaj içeriği boş olamaz"
                    };
                }

                // Kullanıcıları kontrol et
                var sender = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Id == senderId);
                var receiver = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Id == receiverId);

                if (sender == null || receiver == null)
                {
                    return new ResponseDto<MessageDto>
                    {
                        Success = false,
                        Message = "Gönderici veya alıcı bulunamadı"
                    };
                }

                // Yeni mesajı oluştur
                var message = new Message
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Content = content,
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Messages.AddAsync(message);
                await _unitOfWork.SaveChangesAsync();

                // Mesaj gönderildikten sonra detaylı bilgileri tekrar al
                var createdMessage = await _unitOfWork.Messages
                    .Include(m => m.Sender)
                    .Include(m => m.Receiver)
                    .FirstOrDefaultAsync(m => m.Id == message.Id);

                var messageDto = _mapper.Map<MessageDto>(createdMessage);

                return new ResponseDto<MessageDto> { Data = messageDto };
            }

            public async Task<ResponseDto<NoDataDto>> MarkAsReadAsync(string userId, int messageId)
            {
                var message = await _unitOfWork.Messages.FirstOrDefaultAsync(m => m.Id == messageId);

                if (message == null)
                {
                    return new ResponseDto<NoDataDto>
                    {
                        Success = false,
                        Message = "Mesaj bulunamadı"
                    };
                }

                // Kullanıcı bu mesajın alıcısı mı kontrol et
                if (message.ReceiverId != userId)
                {
                    return new ResponseDto<NoDataDto>
                    {
                        Success = false,
                        Message = "Bu işlemi yapmaya yetkiniz yok"
                    };
                }

                message.IsRead = true;
                await _unitOfWork.SaveChangesAsync();

                return new ResponseDto<NoDataDto> { Data = new NoDataDto() };
            }

            public async Task<ResponseDto<int>> GetUnreadCountAsync(string userId)
            {
                var unreadCount = await _unitOfWork.Messages
                    .CountAsync(m => m.ReceiverId == userId && !m.IsRead);

                return new ResponseDto<int> { Data = unreadCount };
            }
        }
    }