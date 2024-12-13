using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Application.Features.Users;
using Microsoft.Extensions.Configuration;

namespace Application.Services.User
{
    public class UserManager : IUserService
    {
        private readonly IConfiguration configuration;
        public UserManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<GetListUsersResponse>> GetListUser(List<string> userIds)
        {
            var apiKey = configuration.GetSection("Clerk:ApiKey").Get<string>();
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var url = "https://api.clerk.com/v1/users";
            if (userIds.Count() > 0)
            {
                var joinedUserIds = string.Join(",", userIds);
                url = $"https://api.clerk.com/v1/users?user_id={joinedUserIds}";
                // url = $"https://api.clerk.com/v1/users?user_id=user_2n19IUVwKR5kfTBLUIoQEohzxIs";
            }
            var response = await client.GetAsync(url);
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

            var apiKey = configuration.GetSection("Clerk:ApiKey").Get<string>();
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await client.GetAsync($"https://api.clerk.com/v1/users/count");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var res = System.Text.Json.JsonSerializer.Deserialize<UsersCount>(content);
                return res.total_count;
            }
            return 0; 
        }

        public async Task SetUserRole(string userId, string role)
        {
            var apiKey = configuration.GetSection("Clerk:ApiKey").Get<string>();
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var content = new StringContent(
                 JsonSerializer.Serialize(new
                 {
                     public_metadata = new
                     {
                         role = role
                     }
                 }),
                 Encoding.UTF8,
                 "application/json"
             );

            var response = await client.PatchAsync($"https://api.clerk.com/v1/users/{userId}", content);

            // Başarı kontrolü
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to set user role. Status Code: {response.StatusCode}, Response: {responseContent}");
            }
        }
    }
}

