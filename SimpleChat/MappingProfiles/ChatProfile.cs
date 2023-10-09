using AutoMapper;
using SimpleChat_Core.DTO;
using SimpleChat_Db.Entities;

namespace SimpleChat.MappingProfiles
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<Chat, ChatDTO>();

            CreateMap<ChatDTO, Chat>();
        }
    }
}
