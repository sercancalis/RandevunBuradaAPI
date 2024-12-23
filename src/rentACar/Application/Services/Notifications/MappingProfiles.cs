using System;
using Application.Features.Business.Commands.Update;
using Application.Features.BusinessServices.Command.Create;
using Application.Features.BusinessServices.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Services.Notifications;



public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IPaginate<Notification>, GetListResponse<Notification>>().ReverseMap();
    }
}