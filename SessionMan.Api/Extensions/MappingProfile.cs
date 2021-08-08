using AutoMapper;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.DataTransfer.Session;
using SessionMan.DataAccess.Models;

namespace SessionMan.Api.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientCreateInput, Client>().ReverseMap();
            CreateMap<ClientUpsertOutput, Client>().ReverseMap();
            CreateMap<ClientUpdateInput, Client>().ReverseMap();
            CreateMap<ClientRecord, Client>().ReverseMap();
            
            CreateMap<SessionCreateInput, Session>().ReverseMap();
            CreateMap<SessionUpsertOutput, Session>().ReverseMap();
            CreateMap<SessionUpdateInput, Session>().ReverseMap();
            CreateMap<SessionRecord, Session>().ReverseMap();
        }
    }
}