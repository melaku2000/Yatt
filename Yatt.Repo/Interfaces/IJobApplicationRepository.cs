using Yatt.Models.Dtos;
using Yatt.Models.Entities;

namespace Yatt.Repo.Interfaces
{
    public interface IJobApplicationRepository
    {
        Task<ResponseDto<JobApplicationDto>> GetDtoById(string id);
        Task<ResponseDto<List<JobApplicationDto>>> GetListByCandidateId(string candidateId);
        Task<ResponseDto<List<JobApplicationDto>>> GetListByJobId(string jobId);
        Task<ResponseDto<JobApplicationDto>> Create(JobApplicationDto dto);
        Task<ResponseDto<JobApplicationDto>> Update(JobApplicationDto dto);
        Task<ResponseDto<JobApplicationDto>> Delete(string id);

    }
}
