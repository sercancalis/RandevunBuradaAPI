using System;
using Application.Features.Business.Commands.Update;
using Application.Features.BusinessServices.Command.Create;
using Application.Features.BusinessServices.Queries.GetList;
using Application.Features.Notifications.Command.Send;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Notifications.Profiles;


public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SendNotificationCommand, Notification>().ReverseMap(); 
    }
}

