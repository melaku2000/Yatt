using Blazored.LocalStorage;
using Yatt.Models.Dtos;
using Yatt.Models.Enums;
using Yatt.Web.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Yatt.Web.Services
{
    public class AuthService: IAuthService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ITokenManagerService _tokenManager;
        public AuthService(HttpClient client, AuthenticationStateProvider authStateProvider, ITokenManagerService tokenService)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider;
            _tokenManager = tokenService;
        }

        public async Task<ResponseDto<AuthDto>> ConfirmEmail(ConfirmDto dto)
        {
            var content = JsonSerializer.Serialize(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync($"account/ConfirmEmail/{dto.UserId}", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            ResponseDto<AuthDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<AuthDto>>(postContent, _options);

            return authResponse!;
        }
        public async Task<ResponseDto<AuthDto>> Login(LoginDto dto)
        {
            var content = JsonSerializer.Serialize(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync("account/login", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            ResponseDto<AuthDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<AuthDto>>(postContent, _options);
           
            if (authResponse.Status!=ResponseStatus.Success)
            {
                Console.WriteLine(postContent);
                return authResponse;
            }
            var auth = authResponse.Model;
            var token = auth!.Token;
            await _tokenManager.SetAuth(authResponse.Model!);
            ((AuthStateProvider)_authStateProvider).Notify();
            return new ResponseDto<AuthDto> {Model=auth, Status=ResponseStatus.Success};
        }
        public async Task Logout()
        {
            await _tokenManager.RemoveAuth();
            ((AuthStateProvider)_authStateProvider).Notify();
            //_client.DefaultRequestHeaders.Authorization = null;
        }
        public async Task<ResponseDto<AuthDto>> Register(RegisterDto dto)
        {
            var content = JsonSerializer.Serialize(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync("account/Register", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
           
            ResponseDto<AuthDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<AuthDto>>(postContent, _options);

            return authResponse!;
        }
        public async Task<ResponseDto<AuthDto>> RegisterCompany(RegisterCompanyDto dto)
        {
            var content = JsonSerializer.Serialize(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync("account/RegisterCompany", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            ResponseDto<AuthDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<AuthDto>>(postContent, _options);

            return authResponse!;
        }
        public async Task<ResponseDto<ConfirmDto>> SendPhoneConfirmation(long userId)
        {
            var content = JsonSerializer.Serialize(userId);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync($"account/RegisterPhone/{userId}", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            ResponseDto<ConfirmDto>? response = JsonSerializer.Deserialize<ResponseDto<ConfirmDto>>(postContent, _options);

            return response;
        }
        public async Task<ResponseDto<ConfirmDto>> ConfirmPhone(ConfirmDto dto)
        {
            var content = JsonSerializer.Serialize(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync($"account/ConfirmPhone/{dto.UserId}", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            ResponseDto<ConfirmDto>? authResponse = 
                JsonSerializer.Deserialize<ResponseDto<ConfirmDto>>(postContent, _options);

            return authResponse!;
        }
        public async Task<ResponseDto<AuthDto>> CreateInstructor(RoleDto dto)
        {
            var content = JsonSerializer.Serialize(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync("account/addInstructorInRole", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            ResponseDto<AuthDto>? authResponse = JsonSerializer.Deserialize<ResponseDto<AuthDto>>(postContent, _options);

            return authResponse!;
        }
    }
}
