using Yatt.Models.Dtos;
using System.Text;
using System.Text.Json;

namespace Yatt.Web.Services
{
    public class FileService : IFileService
    {
        private readonly HttpClient _client;
        private readonly ITokenManagerService tokenManager;
        private readonly JsonSerializerOptions _options;
        public FileService(HttpClient client, ITokenManagerService tokenManager)
        {
            _client = client;
            this.tokenManager = tokenManager;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<FileData> GetProfileImage(long id)
        {
            await SetClientToken();

            var response = await _client.GetAsync($"files/GetProfile/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var resp = JsonSerializer.Deserialize<FileData>(content, _options);
           
            return resp;
        }

        public async Task<bool> UploadProfileImage(FileData fileData)
        {
            await SetClientToken();
            var content = JsonSerializer.Serialize(fileData);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _client.PostAsync($"files/profile", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            
            return true;
        }
        private async Task SetClientToken()
        {
            string token = await tokenManager.GetToken();

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
        }
    }
}
