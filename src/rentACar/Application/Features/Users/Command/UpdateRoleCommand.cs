using Application.Services.User;
using MediatR;

namespace Application.Features.Users.Commands;

public class UpdateRoleCommand : IRequest<bool>
{
    public string UserId { get; set; }
    public string Role { get; set; }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand,bool>
    {
        private readonly IUserService _userService;

        public UpdateRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            await _userService.SetUserRole(request.UserId, request.Role);
            return true;
        }
    }
}
