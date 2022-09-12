using Microsoft.AspNetCore.Http;
using Yatt.Models.Dtos;
using Yatt.Models.RequestFeatures;

namespace Yatt.Repo.Interfaces
{
    public interface IAccountRepository
    {
        Task<ResponseDto<AuthDto>> Register(RegisterDto dto);
        Task<ResponseDto<AuthDto>> RegisterCompany(RegisterCompanyDto dto);

        Task<ResponseDto<AuthDto>> VerifyUser(LoginDto dto);
        Task<ResponseDto<AuthDto>> GetUser(string id);
        Task<ResponseDto<AuthDto>> ConfirmEmail(ConfirmDto dto);
        Task<ResponseDto<ConfirmDto>> SendPhoneConfirmation(string userId);
        Task<ResponseDto<ConfirmDto>> ConfirmPhone(ConfirmDto dto);
        Task<AuthDto> GetRefreshToken(string refreshToken, HttpContext httpContext);

        Task<PagedList<UserDto>> GetUserPagedList(PageParameter pageParameter);
    }
}
