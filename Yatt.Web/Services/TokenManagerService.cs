using Blazored.LocalStorage;
using Yatt.Models.Constants;
using Yatt.Models.Dtos;
using Yatt.Web.Features;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Yatt.Web.Services
{
    public class TokenManagerService: ITokenManagerService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly JsonSerializerOptions _options;
        public TokenManagerService(HttpClient authService, ILocalStorageService localStorage)
        {
            this._httpClient = authService;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _localStorage = localStorage;
        }
        public async Task<string> GetRefreshToken()
        {
            return await _localStorage.GetItemAsStringAsync(AuthConstant.REFRESH_TOKEN);
        }

        public async Task<string> GetToken()
        {
            var token= await _localStorage.GetItemAsStringAsync(AuthConstant.TOKEN);

            if (string.IsNullOrEmpty(token)) return string.Empty;

            if(ValidateTokenExpiration(token))
                return token;

            string refreshToken =await GetRefreshToken();

            if(string.IsNullOrEmpty(refreshToken)) return string.Empty;

            var tokenDto = new RequestRefreshTokenDto { Token = token, RefreshToken = refreshToken };

            return await RefreshToken(tokenDto);

        }

        public async Task<AuthDto> GetUserData()
        {
            return await _localStorage.GetItemAsync<AuthDto>(AuthConstant.USER_DATA);
        }

        public async Task RemoveAuth()
        {
            await _localStorage.RemoveItemAsync(AuthConstant.USER_DATA);
            await _localStorage.RemoveItemAsync(AuthConstant.TOKEN);
            await _localStorage.RemoveItemAsync(AuthConstant.REFRESH_TOKEN);
        }

        public async Task SetAuth(AuthDto authDto)
        {
            var token =await GetToken();
            if(!string.IsNullOrEmpty(token)) 
                await RemoveAuth();
            await _localStorage.SetItemAsync<string>(AuthConstant.TOKEN, authDto.Token!);
            await _localStorage.SetItemAsync<string>(AuthConstant.REFRESH_TOKEN, authDto.RefreshToken!);
            authDto.Token = null;
            authDto.RefreshToken = null;
            await _localStorage.SetItemAsync<AuthDto>(AuthConstant.USER_DATA, authDto);
        }
        private async Task<string> RefreshToken(RequestRefreshTokenDto dto)
        {
            var tokenDto = JsonSerializer.Serialize(dto);
            var bodyContent = new StringContent(tokenDto, Encoding.UTF8, "application/json");
            
            var refreshResult = await _httpClient.PostAsync("account/refresh", bodyContent);
            var refreshContent = await refreshResult.Content.ReadAsStringAsync();
            ResponseDto<AuthDto>? result = JsonSerializer.Deserialize<ResponseDto<AuthDto>>(refreshContent, _options);
            if (!refreshResult.IsSuccessStatusCode && result.Model == null)
            {
                return String.Empty;
                //throw new ApplicationException("Something went wrong during the refresh token action");
            }
            var auth = result!.Model;
            await SetAuth(auth!);

            return await Task.FromResult(auth.Token!);
        }
        private bool ValidateTokenExpiration(string token)
        {
            List<Claim> claims = JwtParser.ParseClaimsFromJwt(token).ToList();
            if(claims.Count==0) return false;

            string expirationSeconds = claims
                .Where(a => a.Type.ToLower() == "exp")
                .Select(a => a.Value).FirstOrDefault();
            if(string.IsNullOrEmpty(expirationSeconds)) return false;

            var expirationDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expirationSeconds));

            if(expirationDate<DateTime.UtcNow) return false;

            return true;
        }
    }
}
