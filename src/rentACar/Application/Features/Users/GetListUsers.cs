using System;
using System.Net.Http.Headers;
using Application.Features.Users;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Features.Haidresser.GetList;

public class GetListUsers : IRequest<GetListResponse<GetListUsersResponse>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => $"GetListUsers({PageRequest.Page},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetListUsers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListUsersHandler : IRequestHandler<GetListUsers, GetListResponse<GetListUsersResponse>>
    {
        private readonly IHairdresserRepository _hairdresserRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;

        public GetListUsersHandler(IMapper mapper, IHairdresserRepository hairdresserRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            _hairdresserRepository = hairdresserRepository;
            this.configuration = configuration;
        }

        public async Task<GetListResponse<GetListUsersResponse>> Handle(GetListUsers request,CancellationToken cancellationToken)
        {
            var apiKey = configuration.GetSection("Clerk:ApiKey").Get<string>();
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var totalCount = await GetTotalUserCount(apiKey);
            var response = await client.GetAsync($"https://api.clerk.com/v1/users?limit={request.PageRequest.PageSize}&offset={(request.PageRequest.Page - 1) * request.PageRequest.PageSize}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<List<GetListUsersResponse>>(content);

                var totalPages = (int)Math.Ceiling((double)totalCount / request.PageRequest.PageSize);
                var hasNext = request.PageRequest.Page < totalPages;
                var hasPrevious = request.PageRequest.Page > 1;


                var res = new GetListResponse<GetListUsersResponse>()
                {
                    Count = totalCount, 
                    Items = userResponse,  
                    Pages = totalPages, 
                    Index = request.PageRequest.Page, 
                    Size = request.PageRequest.PageSize,  
                    HasNext = hasNext, 
                    HasPrevious = hasPrevious
                };
                return res;
            }
            else
            {
                return null;
            }

        }

        class UsersCount
        {
            public string @object { get; set; }
            public int total_count { get; set; }
        }


        private async Task<int> GetTotalUserCount(string apiKey)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await client.GetAsync($"https://api.clerk.com/v1/users/count");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<UsersCount>(content);
                return res.total_count;
            }
            return 0;
        }
    }
}


