using Application.Features.Users;
using Application.Services.Repositories;
using Application.Services.User;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Employees.Queries.GetList;

public class GetBussinessEmployees : IRequest<GetListResponse<GetListBusinessEmployeesResponse>>, ICachableRequest
{
    public int BusinessId { get; set; }
    public bool BypassCache { get; }

    public string CacheKey => $"GetBusinessEmployees";
    public string CacheGroupKey => "GetEmployees";

    public TimeSpan? SlidingExpiration { get; }

    public class GetBussinessEmployeesHandler
        : IRequestHandler<GetBussinessEmployees, GetListResponse<GetListBusinessEmployeesResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetBussinessEmployeesHandler(IMapper mapper, IEmployeeRepository employeeRepository, IUserService userService)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _userService = userService;
        }

        public async Task<GetListResponse<GetListBusinessEmployeesResponse>> Handle(
            GetBussinessEmployees request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Employee> employees = await _employeeRepository.GetListAsync(
                index: 0,
                size: 100,
                predicate: x => x.IsActive && x.BusinessId == request.BusinessId
            );

            var businessEmployees = employees.Items.Select(x => x.UserId).ToList();
            var employeInfos = new List<GetListUsersResponse>();
            if (businessEmployees != null && businessEmployees.Count() > 0)
            {
                employeInfos = await _userService.GetListUser(businessEmployees);
            }

            var res = _mapper.Map<GetListResponse<GetListBusinessEmployeesResponse>>(employees);

            foreach (var item in res.Items)
            {
                item.UserInfo = employeInfos.First(x => x.id == item.UserId);
            }

            return res;
        }
    }
}
