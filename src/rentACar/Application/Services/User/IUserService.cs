using System;
using Application.Features.Employees.Commands.Create;
using Application.Features.Users;
using Domain.Entities;

namespace Application.Services.User
{
    public interface IUserService
    {
        public Task<List<GetListUsersResponse>> GetListUser(List<string> userIds);
        public Task<int> GetTotalUserCount();
        public Task SetUserRole(string userId,string role);
        public Task SetBusinessId(string userId,int businessId);
        public Task<CreateEmployeeResponse> AddUser(string userId, int businessId);
        public Task<GetListUsersResponse> GetUserByEmail(string email);
    }
}

