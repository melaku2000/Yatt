using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.RequestFeatures;

namespace Yatt.Repo.Interfaces
{
    public interface IJobDutyRepository
    {
        Task<ResponseDto<JobDutyDto>> GetById(string id);
        Task<ResponseDto<List<JobDutyDto>>> GetListByJobId(string jobId);
        Task<ResponseDto<JobDutyDto>> Create(JobDutyDto dto);
        Task<ResponseDto<JobDutyDto>> Update(JobDutyDto dto);
        Task<ResponseDto<JobDutyDto>> Delete(string id);
    }
}
