using System;
using Application.Features.Employees.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Business.Queries.GetList;


public class GetListBusiness : IRequest<GetListResponse<GetListBusinessResponse>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public bool BypassCache { get; }

    public string CacheKey => $"GetBusiness";
    public string CacheGroupKey => "GetListBusiness";

    public TimeSpan? SlidingExpiration { get; }

    public class GetListBusinessHandler : IRequestHandler<GetListBusiness, GetListResponse<GetListBusinessResponse>>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public GetListBusinessHandler(IMapper mapper, IBusinessRepository businessRepository)
        {
            _mapper = mapper; 
            _businessRepository = businessRepository;
        }

        public async Task<GetListResponse<GetListBusinessResponse>> Handle(GetListBusiness request,CancellationToken cancellationToken)
        {
            IPaginate<Domain.Entities.Business> res = await _businessRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize,
                predicate: x => x.IsActive,
                include: x=> x.Include(a=>a.BusinessImages).Include(a=>a.WorkingHours)
            );

            return _mapper.Map<GetListResponse<GetListBusinessResponse>>(res);
        }
    }
}