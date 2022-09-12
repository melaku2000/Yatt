using Yatt.Models.Dtos;
using Yatt.Models.Entities;

namespace Yatt.Repo.Interfaces
{
    public interface IMembershipRepository
    {
        Task<MembershipDto> GetDtoById(string id);
        Task<ResponseDto<List<MembershipDto>>> GetList();
        Task<ResponseDto<MembershipDto>> Create(MembershipDto dto);
        Task<ResponseDto<MembershipDto>> Update(MembershipDto dto);
        Task<ResponseDto<MembershipDto>> Delete(string id);

    }
}
