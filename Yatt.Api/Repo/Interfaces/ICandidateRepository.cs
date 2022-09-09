using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.RequestFeatures;

namespace Yatt.Api.Repo.Interfaces
{
    public interface ICandidateRepository
    {
        Task<ResponseDto<CandidateDto>> GetById(string id);
        Task<PagedList<CandidateDto>> GetPagedList(PageParameter pageParameter);
        Task<ResponseDto<CandidateDto>> Create(CandidateDto dto);
        Task<ResponseDto<CandidateDto>> Update(CandidateDto dto);
        Task<ResponseDto<CandidateDto>> Delete(string id);
    }
}
