using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.RequestFeatures;

namespace Yatt.Repo.Interfaces
{
    public interface ICompanyRepository
    {
        Task<ResponseDto<CompanyDto>> GetById(string id);
        Task<PagedList<CompanyDto>> GetPagedList(PageParameter pageParameter);
        Task<ResponseDto<CompanyDto>> Create(CompanyDto dto);
        Task<ResponseDto<CompanyDto>> Update(CompanyDto dto);
        Task<ResponseDto<CompanyDto>> Delete(string id);
    }
}
