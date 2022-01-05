using System.Collections.Generic;
using AutoMapper;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;

namespace DodoApp.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<CreateGoodsTransactionHeaderDto, GoodsTransactionHeader>();
            CreateMap<GoodsTransactionHeader, ReadGoodsTransactionHeaderDto>();
            CreateMap<UpdateGoodsTransactionHeaderDto, GoodsTransactionHeader>();
            CreateMap<CreateGoodsTransactionDetailDto, GoodsTransactionDetail>();
            CreateMap<GoodsTransactionDetail, ReadGoodsTransactionDetailDto>();
            CreateMap<List<GoodsTransactionDetail>, List<ReadGoodsTransactionDetailDto>>();
        }
    }
}