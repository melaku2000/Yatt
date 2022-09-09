using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.RequestFeatures;

namespace Yatt.Api.Repo.Interfaces
{
    public interface IEducationRepository
    {
        Task<ResponseDto<EducationDto>> GetById(string id);
        Task<ResponseDto<List<EducationDto>>> GetListByCandidateId(string candidateId);
        Task<ResponseDto<EducationDto>> Create(EducationDto dto);
        Task<ResponseDto<EducationDto>> Update(EducationDto dto);
        Task<ResponseDto<EducationDto>> Delete(string id);
    }
}
