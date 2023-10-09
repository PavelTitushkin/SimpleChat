using AutoMapper;
using SimpleChat_Core.DTO;
using SimpleChat_Db.Entities;

namespace SimpleChat.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserDTO, User>();

            CreateMap<User, UserDTO>();
        }
    }
}
