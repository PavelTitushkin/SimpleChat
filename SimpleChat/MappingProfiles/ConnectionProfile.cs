using AutoMapper;
using SimpleChat_Core.DTO;
using SimpleChat_Db.Entities;

namespace SimpleChat.MappingProfiles
{
    public class ConnectionProfile : Profile
    {
        public ConnectionProfile() 
        {
            CreateMap<Connection, ConnectionDTO>();

            CreateMap<ConnectionDTO, Connection>();
        }
    }
}
