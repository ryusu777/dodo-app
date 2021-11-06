using AutoMapper;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;

namespace DodoApp.Profiles
{
    public class GoodsProfile : Profile
    {
        public GoodsProfile()
        {
            CreateMap<Goods, ReadGoodsDto>();
            CreateMap<CreateGoodsDto, Goods>();
            CreateMap<UpdateGoodsDto, Goods>();
        }
    }
}