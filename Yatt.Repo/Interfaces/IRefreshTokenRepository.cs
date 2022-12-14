using Yatt.Models.Dtos;
using Yatt.Models.Entities;

namespace Yatt.Repo.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetRefreshToken(string refreshToken, string userAgent);
        Task<RefreshToken> UpdateRefreshToken(RequestTokenDto dto);
    }
}
