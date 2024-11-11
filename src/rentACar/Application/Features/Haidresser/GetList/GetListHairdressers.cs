using System;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Haidresser.GetList;

public class GetListHairdressers : IRequest<GetListResponse<GetListHaidressersResponse>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => $"GetListHairdressers({PageRequest.Page},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetHairdressers";

    public TimeSpan? SlidingExpiration { get; }

    public class GetListHairdressersHandler : IRequestHandler<GetListHairdressers, GetListResponse<GetListHaidressersResponse>>
    {
        private readonly IHairdresserRepository _hairdresserRepository;
        private readonly IMapper _mapper;

        public GetListHairdressersHandler(IMapper mapper, IHairdresserRepository hairdresserRepository)
        {
            _mapper = mapper; 
            _hairdresserRepository = hairdresserRepository;
        }

        public async Task<GetListResponse<GetListHaidressersResponse>> Handle(
            GetListHairdressers request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Hairdresser> res = await _hairdresserRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );

            return _mapper.Map<GetListResponse<GetListHaidressersResponse>>(res);
        }
    }
}


