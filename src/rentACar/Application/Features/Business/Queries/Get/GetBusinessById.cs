using System;
using Application.Features.Business.Queries.Get;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Business.Queries.Get;

public class GetBusinessById : IRequest<GetBusinessResponse>, ICachableRequest
{
    public int Id { get; set; }
    public bool BypassCache { get; }
    public string CacheKey => $"GetBusiness(Id={Id})";
    public string CacheGroupKey => "GetListBusiness";
    public TimeSpan? SlidingExpiration { get; }

    public class GetBusinessByIdHandler : IRequestHandler<GetBusinessById, GetBusinessResponse>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public GetBusinessByIdHandler(IMapper mapper, IBusinessRepository businessRepository)
        {
            _mapper = mapper;
            _businessRepository = businessRepository;
        }

        public async Task<GetBusinessResponse> Handle(GetBusinessById request, CancellationToken cancellationToken)
        {
            Domain.Entities.Business? res = await _businessRepository.GetAsync(
                predicate: x => x.Id == request.Id,
                include: x => x.Include(a => a.BusinessImages).Include(x => x.WorkingHours).Include(x=>x.BusinessServices)
            );

            return _mapper.Map<GetBusinessResponse>(res);
        }
    }
}