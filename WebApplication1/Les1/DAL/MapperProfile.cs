using AutoMapper;
using DataLayer;

namespace Les1.DAL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DebetCard, DebetCardResponse>();
            CreateMap<DebetCardRequest, DebetCard>();
        }
    }
}
