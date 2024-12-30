using System;
using Application.Features.Business.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching; 
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Business.Queries.Get;
 

public class GetBusinessByUserId : IRequest<GetBusinessResponse>, ICachableRequest
{
    public string UserId { get; set; }
    public bool BypassCache { get; } 
    public string CacheKey => $"GetBusiness(UserId={UserId})";
    public string CacheGroupKey => "GetListBusiness"; 
    public TimeSpan? SlidingExpiration { get; }

    public class GetBusinessByUserIdHandler : IRequestHandler<GetBusinessByUserId, GetBusinessResponse>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public GetBusinessByUserIdHandler(IMapper mapper, IBusinessRepository businessRepository)
        {
            _mapper = mapper;
            _businessRepository = businessRepository;
        }

        public async Task<GetBusinessResponse> Handle(GetBusinessByUserId request, CancellationToken cancellationToken)
        {
            Domain.Entities.Business? res = await _businessRepository.GetAsync(
                predicate: x => x.IsActive && x.UserId == request.UserId,
                include: x => x.Include(a => a.BusinessImages).Include(x=>x.WorkingHours)
            );

            return _mapper.Map<GetBusinessResponse>(res);
        }
    }
}