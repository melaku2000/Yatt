using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.RequestFeatures;

namespace Yatt.Repo.Interfaces
{
    public interface IJobDescriptionRepository
    {
        Task<ResponseDto<JobDescrioptionDto>> GetById(string id);
        Task<ResponseDto<List<JobDescrioptionDto>>> GetListByJobId(string jobId);
        Task<ResponseDto<JobDescrioptionDto>> Create(JobDescrioptionDto dto);
        Task<ResponseDto<JobDescrioptionDto>> Update(JobDescrioptionDto dto);
        Task<ResponseDto<JobDescrioptionDto>> Delete(string id);
    }
}
