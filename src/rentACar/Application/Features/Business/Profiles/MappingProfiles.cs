using System;
using Application.Features.Business.Commands.Create;
using Application.Features.Business.Commands.Update;
using Application.Features.Business.Queries.Get;
using Application.Features.Business.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using static Application.Features.Business.Queries.Get.GetBusinessResponse;

namespace Application.Features.Business.Profiles;


public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.Business, CreateBusinessCommand>().ReverseMap();
        CreateMap<Domain.Entities.Business, CreateBusinessResponse>().ReverseMap();
        CreateMap<Domain.Entities.Business, UpdateBusinessCommand>().ReverseMap();
        CreateMap<Domain.Entities.Business, UpdateBusinessResponse>().ReverseMap();
        CreateMap<Domain.Entities.Business, GetListBusinessResponse>()
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.BusinessImages.Select(x => x.ImageUrl).ToList()));
        CreateMap<IPaginate<Domain.Entities.Business>, GetListResponse<GetListBusinessResponse>>().ReverseMap();
        CreateMap<Domain.Entities.Business, GetBusinessResponse>()
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.BusinessImages.Select(x => x.ImageUrl)))
            .ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => src.WorkingHours.Select(x => new WorkingHoursDto
            {
                WorkingDay = x.WorkingDay,
                Value = x.Value
            })
            .ToList()));
    }
}

