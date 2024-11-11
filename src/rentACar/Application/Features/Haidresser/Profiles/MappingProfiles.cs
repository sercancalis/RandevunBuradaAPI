using System;
using Application.Features.Haidresser.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Haidresser.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Hairdresser, GetListHaidressersResponse>().ReverseMap();
        CreateMap<IPaginate<Hairdresser>, GetListResponse<GetListHaidressersResponse>>().ReverseMap();
    }
}

