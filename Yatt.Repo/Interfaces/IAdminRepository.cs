using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.RequestFeatures;

namespace Yatt.Repo.Interfaces
{
    public interface IAdminRepository
    {
        Task<ResponseDto<AdminDto>> GetById(string id);
        Task<PagedList<AdminDto>> GetPagedList(PageParameter pageParameter);
        Task<ResponseDto<AdminDto>> Create(AdminDto dto);
        Task<ResponseDto<AdminDto>> Update(AdminDto dto);
        Task<ResponseDto<AdminDto>> Delete(string id);
    }
}
