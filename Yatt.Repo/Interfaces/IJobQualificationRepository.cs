using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.RequestFeatures;

namespace Yatt.Repo.Interfaces
{
    public interface IJobQualificationRepository
    {
        Task<ResponseDto<JobQualificationDto>> GetById(string id);
        Task<ResponseDto<List<JobQualificationDto>>> GetListByJobId(string jobId);
        Task<ResponseDto<JobQualificationDto>> Create(JobQualificationDto dto);
        Task<ResponseDto<JobQualificationDto>> Update(JobQualificationDto dto);
        Task<ResponseDto<JobQualificationDto>> Delete(string id);
    }
}
