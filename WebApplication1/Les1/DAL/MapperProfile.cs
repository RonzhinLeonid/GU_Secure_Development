using AutoMapper;
using DataLayer;

namespace Les1.DAL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<DebetCardRequest, DebetCard>();
            CreateMap<DebetCard, DebetCardRequest>();
            CreateMap<DebetCardResponse, DebetCard>();
            //CreateMap<DebetCard, DebetCardResponse>();
        }
    }
}
