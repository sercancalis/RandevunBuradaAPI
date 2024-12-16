using Application.Features.Users; 
using Application.Services.User; 
using Core.Application.Pipelines.Caching; 
using MediatR;

namespace Application.Features.Employees.Queries.Get;

public class GetEmployeeByEmail : IRequest<GetListUsersResponse>, ICachableRequest
{
    public string Email { get; set; }
    public bool BypassCache { get; }

    public string CacheKey => $"GetEmployee({Email})";
    public string CacheGroupKey => "GetEmployees";

    public TimeSpan? SlidingExpiration { get; }

    public class GetEmployeeByEmailHandler : IRequestHandler<GetEmployeeByEmail, GetListUsersResponse>
    { 
        private readonly IUserService _userService;

        public GetEmployeeByEmailHandler(IUserService userService)
        { 
            _userService = userService;
        }

        public async Task<GetListUsersResponse> Handle(GetEmployeeByEmail request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserByEmail(request.Email);
        }
    }
}
