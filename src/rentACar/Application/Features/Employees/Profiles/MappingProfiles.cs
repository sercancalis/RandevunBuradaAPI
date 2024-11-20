using System;
using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Employees.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Employee, CreateEmployeeCommand>().ReverseMap();
        CreateMap<Employee, CreateEmployeeResponse>().ReverseMap();
        CreateMap<Employee, UpdateEmployeeCommand>().ReverseMap();
        CreateMap<Employee, UpdateEmployeeResponse>().ReverseMap();
        CreateMap<Employee, GetListBusinessEmployeesResponse>().ReverseMap();
        CreateMap<IPaginate<Employee>, GetListResponse<GetListBusinessEmployeesResponse>>().ReverseMap();
    }
}
