using Application.Features.Business.Commands.Create;
using Application.Features.Business.Commands.Update; 
using Application.Features.BusinessServices.Command.Create;
using Application.Features.BusinessServices.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
namespace Application.Features.BusinessServices.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<BusinessService, CreateServiceCommand>().ReverseMap();
        CreateMap<BusinessService, CreateServiceResponse>().ReverseMap();
        CreateMap<BusinessService, UpdateBusinessCommand>().ReverseMap();
        CreateMap<BusinessService, UpdateBusinessResponse>().ReverseMap(); 
        CreateMap<BusinessService, GetListServiceResponse>().ReverseMap();
        CreateMap<IPaginate<BusinessService>, GetListResponse<GetListServiceResponse>>().ReverseMap();
    }
}

