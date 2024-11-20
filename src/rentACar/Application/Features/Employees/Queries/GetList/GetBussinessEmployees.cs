using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Employees.Queries.GetList;

public class GetBussinessEmployees : IRequest<GetListResponse<GetListBusinessEmployeesResponse>>, ICachableRequest
{
    public bool BypassCache { get; }

    public string CacheKey => $"GetBusinessEmployees";
    public string CacheGroupKey => "GetEmployees";

    public TimeSpan? SlidingExpiration { get; }

    public class GetBussinessEmployeesHandler
        : IRequestHandler<GetBussinessEmployees, GetListResponse<GetListBusinessEmployeesResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetBussinessEmployeesHandler(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper; 
            _employeeRepository = employeeRepository;
        }

        public async Task<GetListResponse<GetListBusinessEmployeesResponse>> Handle(
            GetBussinessEmployees request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Employee> animals = await _employeeRepository.GetListAsync(
                index: 0,
                size: 100,
                predicate: x => x.IsActive
            );

            return _mapper.Map<GetListResponse<GetListBusinessEmployeesResponse>>(animals);
        }
    }
}
