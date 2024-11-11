using System;
using Application.Features.Haidresser.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<GetListUsers, GetListUsersResponse>().ReverseMap();
        CreateMap<IPaginate<GetListUsers>, GetListResponse<GetListUsersResponse>>().ReverseMap();
    }
}

