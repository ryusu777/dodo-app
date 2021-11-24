using System;
using System.Collections.Generic;
using AutoMapper;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;

namespace DodoApp.Profiles
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<CreateCurrencyDto, Currency>();
            CreateMap<PageWrapper<List<Currency>>, PageWrapper<List<ReadCurrencyDto>>>();
        }
    }
}