using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.RequestFeatures;

namespace Yatt.Repo.Interfaces
{
    public interface IJobRepository
    {
        Task<ResponseDto<JobDto>> GetById(string id);
        Task<ResponseDto<List<JobDto>>> GetListByVacancyId(string vacancyId);
        Task<ResponseDto<JobDto>> Create(JobDto dto);
        Task<ResponseDto<JobDto>> Update(JobDto dto);
        Task<ResponseDto<JobDto>> Delete(string id);
    }
}
