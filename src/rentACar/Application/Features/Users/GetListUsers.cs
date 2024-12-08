using Application.Features.Users; 
using Application.Services.User; 
using Core.Application.Pipelines.Caching; 
using MediatR; 

namespace Application.Features.Haidresser.GetList;

public class GetListUsers : IRequest<List<GetListUsersResponse>>, ICachableRequest
{
    public List<string> UserIds { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => $"GetListUsers({string.Join(",",UserIds)})";
    public string CacheGroupKey => "GetListUsers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListUsersHandler : IRequestHandler<GetListUsers, List<GetListUsersResponse>>
    {  
        private readonly IUserService _userService;

        public GetListUsersHandler(IUserService userService)
        {  
            _userService = userService;
        }

        public async Task<List<GetListUsersResponse>> Handle(GetListUsers request,CancellationToken cancellationToken)
        {
            return await _userService.GetListUser(request.UserIds);
        } 
    }
}


