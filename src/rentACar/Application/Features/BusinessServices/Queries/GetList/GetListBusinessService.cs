using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching; 
using Core.Persistence.Paging;
using MediatR;
using Domain.Entities;
using Core.Application.Responses;

namespace Application.Features.BusinessServices.Queries.GetList;

public class GetListBusinessService : IRequest<GetListResponse<GetListServiceResponse>>, ICachableRequest
{
    public int BusinessId { get; set; }
    public bool BypassCache { get; }

    public string CacheKey => $"GetServices";
    public string CacheGroupKey => "GetListServices";

    public TimeSpan? SlidingExpiration { get; }

    public class GetListBusinessServiceHandler : IRequestHandler<GetListBusinessService, GetListResponse<GetListServiceResponse>>
    {
        private readonly IBusinessServiceRepository _businessServiceRepository;
        private readonly IMapper _mapper;

        public GetListBusinessServiceHandler(IMapper mapper, IBusinessServiceRepository businessServiceRepository)
        {
            _mapper = mapper; 
            _businessServiceRepository = businessServiceRepository;
        }

        public async Task<GetListResponse<GetListServiceResponse>> Handle(GetListBusinessService request, CancellationToken cancellationToken)
        {
            IPaginate<BusinessService> res = await _businessServiceRepository.GetListAsync(
                index: 0,
                size: 100,
                predicate: x => x.IsActive && x.BusinessId == request.BusinessId
            );

            return _mapper.Map<GetListResponse<GetListServiceResponse>>(res);
        }
    }
}