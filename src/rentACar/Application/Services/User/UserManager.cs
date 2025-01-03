using System;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Application.Features.Employees.Commands.Create;
using Application.Features.Users;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Services.User
{
    public class UserManager : IUserService
    {
        private readonly IConfiguration configuration;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        public UserManager(IConfiguration configuration, IEmployeeRepository employeeRepository, IMapper mapper, HttpClient httpClient)
        {
            this.configuration = configuration;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _httpClient = httpClient;
            _apiKey = configuration.GetValue<string>("Clerk:ApiKey");
        }

        public async Task<List<GetListUsersResponse>> GetListUser(List<string> userIds)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var url = "https://api.clerk.com/v1/users";
            if (userIds.Count() > 0)
            {  
                url = $"https://api.clerk.com/v1/users?user_id={string.Join("&user_id=", userIds)}";
                // url = $"https://api.clerk.com/v1/users?user_id=user_2n19IUVwKR5kfTBLUIoQEohzxIs";
            }
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var users = System.Text.Json.JsonSerializer.Deserialize<List<GetListUsersResponse>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return users;
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

        public async Task<int> GetTotalUserCount()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.GetAsync($"https://api.clerk.com/v1/users/count");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var res = System.Text.Json.JsonSerializer.Deserialize<UsersCount>(content);
                return res.total_count;
            }
            return 0; 
        } 

        public async Task SetBusinessIdAndRole(string userId, int businessId, string role)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var content = new StringContent(
                 JsonSerializer.Serialize(new
                 {
                     public_metadata = new
                     {
                         businessId = businessId,
                         role= role
                     }
                 }),
                 Encoding.UTF8,
                 "application/json"
             );

            var response = await _httpClient.PatchAsync($"https://api.clerk.com/v1/users/{userId}", content);
             
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to set user role. Status Code: {response.StatusCode}, Response: {responseContent}");
            }
        }

        public async Task<CreateEmployeeResponse> AddUser(string userId,int businessId)
        { 
            var res = await _employeeRepository.AddAsync(new Employee
            {
                BusinessId = businessId,
                UserId = userId,
                IsConfirmed = true
            });
            return _mapper.Map<CreateEmployeeResponse>(res);
        }

        public async Task<GetListUsersResponse> GetUserByEmail(string email)
        {
            try
            {  
                var url = $"https://api.clerk.com/v1/users?email_address={email}"; 
                // Add authorization header
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                // Send the request
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    // Log the error or handle it accordingly
                    return null;
                }

                // Parse the response content
                var responseContent = await response.Content.ReadAsStringAsync(); 
                var users = JsonSerializer.Deserialize<List<GetListUsersResponse>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return users.FirstOrDefault(); 
            }
            catch (Exception ex)
            { 
                return null;
            }
        }

        public async Task<string> GetAdminUserId()
        {
            try
            {
                var url = $"https://api.clerk.com/v1/users?email_address=sercancalis7@gmail.com";
                // Add authorization header
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                // Send the request
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    // Log the error or handle it accordingly
                    return null;
                }

                // Parse the response content
                var responseContent = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<List<GetListUsersResponse>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if(users != null && users.Any())
                {
                    return users.Select(x => x.id).First();
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

