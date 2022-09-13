using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.RequestFeatures;

namespace Yatt.Repo.Interfaces
{
    public interface IExperianceRepository 
    {
        Task<ResponseDto<ExperianceDto>> GetById(string id);
        Task<ResponseDto<List<ExperianceDto>>> GetListByCandidateId(string candidateId);
        Task<ResponseDto<ExperianceDto>> Create(ExperianceDto dto);
        Task<ResponseDto<ExperianceDto>> Update(ExperianceDto dto);
        Task<ResponseDto<ExperianceDto>> Delete(string id);
    }
}
