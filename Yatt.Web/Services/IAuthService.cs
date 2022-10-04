
using Yatt.Models.Dtos;

namespace Yatt.Web.Services
{
    public interface IAuthService
    {
        Task<ResponseDto<AuthDto>> Login(LoginDto dto);
        Task<ResponseDto<AuthDto>> Register(RegisterDto dto);
        Task<ResponseDto<AuthDto>> RegisterCompany(RegisterCompanyDto dto);
        Task<ResponseDto<AuthDto>> CreateInstructor(RoleDto dto);
        Task<ResponseDto<AuthDto>> ConfirmEmail(ConfirmDto dto);
        Task<ResponseDto<ConfirmDto>> ConfirmPhone(ConfirmDto dto);
        Task<ResponseDto<ConfirmDto>> SendPhoneConfirmation(long userId);
        Task Logout();
    }
}
