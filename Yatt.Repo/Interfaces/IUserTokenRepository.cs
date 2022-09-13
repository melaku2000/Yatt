using Yatt.Models.Dtos;

namespace Yatt.Repo.Interfaces
{
    public interface IUserTokenRepository
    {
        Task<ResponseDto<UserTokenDto>> GenerateEmailConfirmationToken(RequestTokenDto dto);
        Task<ResponseDto<RefreshTokenDto>> GetRefreshToken(RequestTokenDto dto);
        Task<ResponseDto<RefreshTokenDto>> GetRefreshTokenByUserId(RequestTokenDto dto);
        Task<ResponseDto<RefreshTokenDto>> UpdateRefreshToken(RequestTokenDto dto);
        Task<ResponseDto<RefreshTokenDto>> AddRefreshToken(RequestTokenDto dto);
    }
}
