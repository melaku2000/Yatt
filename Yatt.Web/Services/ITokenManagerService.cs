using Yatt.Models.Dtos;

namespace Yatt.Web.Services
{
    public interface ITokenManagerService
    {
        Task SetAuth(AuthDto authDto);
        Task<AuthDto> GetUserData();
        Task<string> GetToken();
        Task<string> GetRefreshToken();
        Task RemoveAuth();
    }
}
