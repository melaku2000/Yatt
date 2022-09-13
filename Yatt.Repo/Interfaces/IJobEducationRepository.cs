using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.RequestFeatures;

namespace Yatt.Repo.Interfaces
{
    public interface IJobEducationRepository
    {
        Task<ResponseDto<JobEducationDto>> GetById(string id);
        Task<ResponseDto<List<JobEducationDto>>> GetListByJobId(string jobId);
        Task<ResponseDto<JobEducationDto>> Create(JobEducationDto dto);
        Task<ResponseDto<JobEducationDto>> Update(JobEducationDto dto);
        Task<ResponseDto<JobEducationDto>> Delete(string id);
    }
}
